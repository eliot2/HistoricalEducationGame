﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Hand : MonoBehaviour {

    private LinkedList<GameObject> Cards;
    public Deck deck;
    public Transform[] HandCardPositions;

    private int[] clearTypes;

    // Use this for initialization
    void Start () {
        clearTypes = new int[] {0,0,0,1,0,0,0,1,0,1,
                                1,0,1,1,0,0,0,0,1,0,0};
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void NewGame(){
        //from previous game.
        if (Cards != null) { EmptyHand(); } 
        Cards = new LinkedList<GameObject>();
    }

    public void DrawCard(){
        while (Cards.Count < 5) {
            Cards.AddLast(deck.Top());
            Cards.Last.Value.transform.SetParent(HandCardPositions[
                Cards.Count - 1], false);
            Cards.Last.Value.transform.localPosition = new Vector3(0, 0, 0);
            Cards.Last.Value.transform.localScale = new Vector3(1f, 1f, 1f);
            Cards.Last.Value.GetComponent<Card>().SetNumPos(Cards.Count - 1);
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(14);
        }
    }

    public void RemoveCard(int CardNumber)
    {
        if(CardNumber != -1)
            RemoveRcsv(0, CardNumber, Cards.First);
    }

    /// <summary>
    /// Removes card and restructures hand and list.
    /// </summary>
    /// <param name="test"></param>
    /// <param name="target"></param>
    /// <param name="node"></param>
    private void RemoveRcsv(int test, int target, LinkedListNode<GameObject> node)
    {
        if (test != target)
        {
            RemoveRcsv(test + 1, target, node.Next);
        }
        else
        {
            node.Value.SetActive(false); //hide the card till new loc. chosen.
            node.Value.transform.SetParent(null, false);
            Cards.Remove(node);
        }
        if (test == 0)
        {
            FixHand(0, Cards.First);
        }
    }

    /// <summary>
    /// FOR AI USE ONLY
    /// </summary>
    /// <returns>First card in hand.</returns>
    public Transform GetRandomCard() 
    {   
        bool playerWinning =
            GameObject.FindGameObjectWithTag("InfluenceManager").
                GetComponent<InfluenceManager>().IsPlayerWinning();
        //AI won't use clears if the AI is winning. Unless of course the
        //second card is also a clear . . .
        LinkedListNode<GameObject> tempObj = Cards.First;
        if (!playerWinning)
        {
            for (int x = 0; x < Cards.Count; x++)
            {
                if (x == 4)
                {
                    //if we go through the whole hand looking for a non-clear
                    //then just return the last
                    return tempObj.Value.transform;
                }
                //if the card is marked as 1 in the list of card clear types,
                //don't choose it when the AI is winning.
                if (clearTypes[tempObj.Value.transform.GetComponent<Card>().
                    GetCardType()] == 1)
                {
                    tempObj = tempObj.Next;
                }
                else
                {
                    return tempObj.Value.transform;
                }
            }
        }
        //since decks are shuffled, this is technically a random card.
        return Cards.First.Value.transform;
    }

    private void FixHand(int start, LinkedListNode<GameObject> node)
    {
        if (node.Value.transform.parent == null)
        {
            Debug.Log("Sa");
        }
        else if (HandCardPositions[start] == null)
        {
            Debug.Log("sass");
        }
        node.Value.transform.SetParent(HandCardPositions[start], false);
        node.Value.transform.localPosition = new Vector3(0, 0, 0);
        node.Value.transform.localScale = new Vector3(1f, 1f, 1f);
        node.Value.GetComponent<Card>().SetNumPos(start);
        if (node.Next != null)
        {
            FixHand(start + 1, node.Next);
        }    
    }

    private void EmptyHand()
    {
        //for (card in CARDS.count)
        GameObject tempObj;
        for (int x = 0; x < Cards.Count; x++)
        {
            //Get gameobject reference
            tempObj = Cards.First.Value;
            //delete node
            RemoveCard(0);
            //delete gameobject reference
            Destroy(tempObj);
        }
    }
}
