﻿using UnityEngine;
using System;
using System.Collections;


public class StatManager : MonoBehaviour
{
    public static StatManager Instance;

    public int KillsHighscore;
    public int ComboHighscore;
    public int HighestWave;
    public int CriticalsHighscore;
    public int BuffsHighscore;
    public int BrutesHighscore;
    public int BossesHighscore;


    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        UpdateStats();
    }

    public void UpdateStats()
    {
        KillsHighscore = PlayerPrefs.GetInt("KillsHighscore");
        ComboHighscore = PlayerPrefs.GetInt("ComboHighscore");
        HighestWave = PlayerPrefs.GetInt("RoomsHighscore");
        CriticalsHighscore = PlayerPrefs.GetInt("CriticalsHighscore");
        BuffsHighscore = PlayerPrefs.GetInt("BuffsHighscore");
        BrutesHighscore = PlayerPrefs.GetInt("BrutesKilledHighscore");
        BossesHighscore = PlayerPrefs.GetInt("BossesKilledHighscore");
    }

    public void SaveStats(string saveName,int id)
    {
        PlayerPrefs.SetInt(saveName,id);
    }

    public void LoadStats(string saveName,int id)
    {
        id = PlayerPrefs.GetInt(saveName);
    }
}
