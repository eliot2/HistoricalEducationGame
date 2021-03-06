﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfluenceManager : MonoBehaviour {

    private int influenceCount;
    public GameObject YourBar;
    public GameObject InfluenceText;
    private int AITurn;  //influence manager should know when to
                        //increase or decrease influence.
                        //-1 means AITurn
    private int DoubleCount;
    private int tripleCount;
    private int SpecialModDouble;
    private bool InfluenceBooster;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void NewGame()
    {
        AITurn = 1;
        influenceCount = 15;
        DoubleCount = 1;
        tripleCount = 1;
        SpecialModDouble = 1;
        YourBar.GetComponent<Image>().fillAmount = 0.5f;
        InfluenceText.GetComponent<Text>().text = "15/30";
        InfluenceBooster = false;
    }

    public void DecreaseInfluence(int amount)
    {
        amount *= AITurn;
        amount *= (DoubleCount*SpecialModDouble*tripleCount); //applying modification from any double cards.
        //Once double's been used, reset it.
        DoubleCount = 1;
        //floor influence at 0
        influenceCount = ((influenceCount-amount) < 0 )? 0 : 
            influenceCount - amount;
        YourBar.GetComponent<Image>().fillAmount = influenceCount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + influenceCount+ "/30";
    }

    public void IncreaseInfluence(int amount)
    {
        if (InfluenceBooster)
        {
            SingleIncreaseInstance();
        }
        amount *= AITurn; //AITurn = -1
        amount *= (DoubleCount*SpecialModDouble*tripleCount); //applying modification from any double cards.
        //Once double's been used, reset it.
        DoubleCount = 1;
        //cap influence at 30
        Debug.Log("Increasing by amount: " + amount);
        int fillAmount = ((influenceCount + amount) > 30) ? 30 :
            influenceCount + amount;
        influenceCount = influenceCount + amount;
        YourBar.GetComponent<Image>().fillAmount = fillAmount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + influenceCount + "/30";
    }

    public void SetInfluence(int amount)
    {
        influenceCount = amount;
        YourBar.GetComponent<Image>().fillAmount = amount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + amount + "/30";
    }

    public void disableDouble()
    {
        DoubleCount = 1;
    }

    public int GetWinStatus()
    {
        if (influenceCount >= 30)
        {
            return 1;
        }
        else if (influenceCount <= 0)
        {
            return -1;
        }else{
            return 0;
        }
    }

    public void TurnChange()
    {
        AITurn = AITurn * -1;
        DoubleCount = 1;
        tripleCount = 1;
        SpecialModDouble = 1;
        InfluenceBooster = false;
    }

    public void DoubleNext()
    {
        DoubleCount = 2;
    }


    public void TripleNext()
    {
        tripleCount = 3; //bad variable name, sue me.
    }

    public void CancelDouble()
    {
        tripleCount = 1;
        DoubleCount = 1;
        SpecialModDouble = 1;
    }

    public void SetSpecialMod(int amount)
    {
        SpecialModDouble = amount;
    }

    public void EnableSpoilsInfluence()
    {
        InfluenceBooster = true;
    }

    public void DisableSpoilsInfluence()
    {
        InfluenceBooster = false;
    }

    /// <summary>
    /// Helper function for card 19 ability.
    /// </summary>
    private void SingleIncreaseInstance()
    {
        int amount = 1*AITurn;
        Debug.Log("Increasing by amount: " + amount);
        
        influenceCount = influenceCount + amount;
        YourBar.GetComponent<Image>().fillAmount = influenceCount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + influenceCount + "/30";
    }

    public bool IsPlayerWinning()
    {
        if (influenceCount >= 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
