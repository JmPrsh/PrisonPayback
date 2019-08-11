using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{

    //Stats
    public Text KillAmount;
    public Text HighestComboAmount;
    public Text RoomsClearedAmount;
    public Text CriticalHitsAmount;
    public Text BuffsUsedAmount;
    public Text TimePlayedAmount;

    public static int Kills;
    public static int Combo;
    public static int Rooms;
    public static int Criticals;
    public static int Buffs;
    public static int TimePlayed;
    public static int BrutesKilled;
    public static int BossesKilled;

    public static int KillsHighscore;
    public static int ComboHighscore;
    public static int RoomsHighscore;
    public static int CriticalsHighscore;
    public static int BuffsHighscore;
    public static int TimePlayedHighscore;
    public static int BrutesKilledHighscore;
    public static int BossesKilledHighscore;

    public GameObject KillNew;
    public GameObject ComboNew;
    public GameObject RoomsNew;
    public GameObject CriticallNew;
    public GameObject BuffsNew;
    public GameObject TimeNew;
    public GameObject BrutesNew;
    public GameObject BossesNew;

    int TutorialCompleted;

    // Use this for initialization
    void Start()
    {
        StatManager.Instance.LoadStats("KillsHighscore", KillsHighscore);
        StatManager.Instance.LoadStats("ComboHighscore", ComboHighscore);
        StatManager.Instance.LoadStats("RoomsHighscore", RoomsHighscore);
        StatManager.Instance.LoadStats("CriticalsHighscore", CriticalsHighscore);
        StatManager.Instance.LoadStats("BuffsHighscore", BuffsHighscore);
        StatManager.Instance.LoadStats("TimePlayedHighscore", TimePlayedHighscore);
        StatManager.Instance.LoadStats("BrutesKilledHighscore", BrutesKilledHighscore);
        StatManager.Instance.LoadStats("BossesKilledHighscore", BossesKilledHighscore);
		
        if (PlayerPrefs.GetInt("Adverts") == 0)
            AdvertHandler.instance.WatchAdvert();
    }

    void OnEnable()
    {
        Rooms = WaveManager.WM.WavesCleared;
        TimePlayed = (int)CharacterStats.timeplayed;
    }

    // Update is called once per frame
    void Update()
    {
        //		if (TutorialCompleted == 1) {
        KillAmount.text = Kills.ToString();
        HighestComboAmount.text = Combo.ToString();
        if (Rooms > 0)
        {
            RoomsClearedAmount.text = Rooms.ToString();
        }
        else
        {
            RoomsClearedAmount.text = "0";
        }
        CriticalHitsAmount.text = Criticals.ToString();
        BuffsUsedAmount.text = Buffs.ToString();


        int minutes = Mathf.FloorToInt(TimePlayed / 60F);
        int seconds = Mathf.FloorToInt(TimePlayed - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        TimePlayedAmount.text = niceTime;


        if (Kills > KillsHighscore)
        {
            KillsHighscore = Kills;
            KillNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("KillsHighscore", KillsHighscore);
        }
        if (Combo > ComboHighscore)
        {
            ComboHighscore = Combo;
            ComboNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("ComboHighscore", ComboHighscore);
        }
        if (Rooms > RoomsHighscore)
        {
            RoomsHighscore = Rooms;
            RoomsNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("RoomsHighscore", RoomsHighscore);
        }
        if (Criticals > CriticalsHighscore)
        {
            CriticalsHighscore = Criticals;
            CriticallNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("CriticalsHighscore", CriticalsHighscore);
        }
        if (Buffs > BuffsHighscore)
        {
            BuffsHighscore = Buffs;
            BuffsNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("BuffsHighscore", BuffsHighscore);
        }
        if (TimePlayed > TimePlayedHighscore)
        {
            TimePlayedHighscore = TimePlayed;
            TimeNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("TimePlayedHighscore", TimePlayedHighscore);
        }
        if (BrutesKilled > BrutesKilledHighscore)
        {
            BrutesKilledHighscore = BrutesKilled;
            BrutesNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("BrutesKilledHighscore", BrutesKilledHighscore);
        }
        if (BossesKilled > BossesKilledHighscore)
        {
            BossesKilledHighscore = BossesKilled;
            BossesNew.SetActive(true);
            // show (new) gameobject
            StatManager.Instance.SaveStats("BossesKilledHighscore", BossesKilledHighscore);
        }

        StatManager.Instance.UpdateStats();
    }
}
