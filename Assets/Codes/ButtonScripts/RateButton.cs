﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RateButton : MonoBehaviour {

    public Button[] StarRatings;

    private ColorBlock DefaultColorSet;
	// Use this for initialization
	void Start () {
        DefaultColorSet = StarRatings[0].colors;
	}
	
	// Update is called once per frame
	void Update () {
	    
    }

    public void SetRating(float Rating)
    {
        int RateSwitch = (int)Rating;
        switch (RateSwitch)
        {
            case 1:
                ColorRaters(1);
                break;
            case 2:
                ColorRaters(2);
                break;
            case 3:
                ColorRaters(3);
                break;
            case 4:
                ColorRaters(4);
                break;
            case 5:
                ColorRaters(5);
                break;
            default:
                Debug.LogError("INCORRECT RATING GIVEN.");
                break;
        }
    }

    private void ColorRaters(int Rating)
    {
        
        for (int x = 0; x < Rating; x++)
        {
            ColorBlock TempColorSet =  StarRatings[x].colors;
            TempColorSet.normalColor = Color.yellow;
            StarRatings[x].colors = TempColorSet;
        }
        for (int x = Rating; x < 5; x++)
        {
            StarRatings[x].colors = DefaultColorSet;
        }
    }
}