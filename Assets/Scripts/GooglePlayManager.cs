using UnityEngine;
using System;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class GooglePlayManager : MonoBehaviour
{
#if UNITY_ANDROID
    public static GooglePlayManager Instance;


    void Awake()
    {
        PlayGamesPlatform.Activate();
    }

    void Start()
    {
        LogIn();
    }

    public void LogIn() // so it can be accessed by a button
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ReportScore((int)PlayerPrefs.GetFloat("HighScoreZombie"), "CgkI9OO2ssgEEAIQAQ", (bool success2) => { });
                Social.ReportScore((int)PlayerPrefs.GetFloat("HighScoreNormal"), "CgkI9OO2ssgEEAIQAA", (bool success2) => { });
            }



        });
    }

    public void Leaderboards()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            LogIn();
        }
    }

    public void Achievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            LogIn();
        }
    }
#endif
}

