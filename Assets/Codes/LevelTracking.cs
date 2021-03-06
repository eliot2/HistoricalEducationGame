﻿using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Reflection;

public class LevelTracking : MonoBehaviour {

    private int PlayerLevel;
    private bool DataLoaded;
    public int TempLevel;

    // Use this for initialization
    void Start () {

        if (Application.isEditor)
        {
            PlayerLevel = TempLevel;
            if (TempLevel == 0)
                ResetStats();
            SaveData();
        }

        DataLoaded = false;
        LoadData();
        DataLoaded = true;

    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("s"))
        {
            SaveData();
        }

    }

    public void LevelUp()
    {
        PlayerLevel = PlayerLevel==15?15:PlayerLevel+1;
        SaveData();
    }

    public int GetLevel()
    {
        return PlayerLevel;
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("IsFirstTime", 0);
        PlayerLevel = 0;
        PlayerPrefs.SetInt("Level", PlayerLevel);
        PlayerPrefs.SetInt("Rating", 0);
        GetComponent<DataTracking>().WriteNewGameLine();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Level", PlayerLevel);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        PlayerLevel = PlayerPrefs.GetInt("Level");
        Debug.Log("PLayer level is: " + PlayerLevel);
    }

    private static char GetRandomLetter()
    {
        System.Random _random = new System.Random();
        int randInt = _random.Next(0, 26); // Zero to 25
        char randLetter = (char)('a' + randInt);
        return randLetter;
    }

    public void MaxLevelCheat()
    {
        PlayerLevel = 14;
        TempLevel = 14;
        SaveData();
        LoadData();
    }
}
