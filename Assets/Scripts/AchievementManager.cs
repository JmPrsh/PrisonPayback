using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievementHandler
{
    public static int[] completedallgameID = new int[10];

    void Awake()
    {
#if UNITY_ANDROID
        if (PlayerPrefs.HasKey("completedallgameID"))
            completedallgameID = PlayerPrefsX.GetIntArray("completedallgameID");

#endif
    }

    public static void WhichAchievement(int i)
    {
        if (AchievementManager.Achievements[i] < 1)
        {
            AchievementManager.Achievements[i] = 1;
            AchievementManager.CheckingAchievements();
        }
    }

    public static void CheckCompletedAllGame()
    {
        PlayerPrefsX.SetIntArray("completedallgameID", completedallgameID);
        if (SumArray(completedallgameID) == 3)
        {
            WhichAchievement(8);
        }
    }

    public static int SumArray(int[] toBeSummed)
    {
        int sum = 0;
        foreach (int item in toBeSummed)
        {
            sum += item;
        }
        return sum;
    }

    public static void DebugTest()
    {
        Debug.Log("Not Earned all 25 levels");
    }

}

public class AchievementManager 
{
    public static AchievementManager AM;
    public int[] Achievementss;
    public static int[] Achievements = new int[27];


    // Use this for initialization
    void Awake()
    {
        AM = this;
#if UNITY_ANDROID
        if (PlayerPrefs.HasKey("Achievements"))
            Achievements = PlayerPrefsX.GetIntArray("Achievements");

#endif
    }

    void Start()
    {
#if UNITY_ANDROID
        CheckingAchievements();
#endif
    }


    // Update is called once per frame
    void Update()
    {
        Achievementss = Achievements;
    }

    public static void CheckingAchievements()
    {
#if UNITY_ANDROID
        if (Achievements[0] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQAw", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[1] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQBA", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[2] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQBQ", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[3] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQBg", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[4] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQBw", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[5] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQCA", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[6] == 1)
        {

            Social.ReportProgress("CgkI9OO2ssgEEAIQCQ", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[7] == 1)
        {
            Social.ReportProgress("CgkI9OO2ssgEEAIQCg", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[8] == 1)
        {
            Social.ReportProgress("CgkI9OO2ssgEEAIQCw", 100.0f, (bool success) =>
            {

            });
        }
        if (Achievements[9] == 1) // 9000 guards
        {
            Social.ReportProgress("CgkI9OO2ssgEEAIQDA ", 100.0f, (bool success) =>
            {

            });
        }

        PlayerPrefsX.SetIntArray("Achievements", Achievements);
#endif
    }

}
