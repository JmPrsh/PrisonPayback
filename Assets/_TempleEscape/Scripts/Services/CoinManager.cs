﻿using UnityEngine;
using System;
using System.Collections;


    public class CoinManager : MonoBehaviour
    {
        public static CoinManager Instance;

        public int Coins;
       
        int initialCoins = 0;

        // key name to store high score in PlayerPrefs
        const string PPK_COINS = "SGLIB_COINS";


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
            Coins = PlayerPrefs.GetInt(PPK_COINS);
        }

        void Start()
        {
            Reset();
        }

        public void Reset()
        {
            // Initialize coins
            Coins = PlayerPrefs.GetInt(PPK_COINS, initialCoins);
        }

        public void AddCoins(int amount)
        {
            Coins += amount;


            // Store new coin value
            PlayerPrefs.SetInt(PPK_COINS, Coins);
        }

        public void RemoveCoins(int amount)
        {
            Coins -= amount;

            // Store new coin value
            PlayerPrefs.SetInt(PPK_COINS, Coins);
        }
    
}
