using System.Collections;
using System.Collections.Generic;
using ChartboostSDK;
using UnityEngine;
using EasyButtons;

public class AdvertHandler : MonoBehaviour {

    public static AdvertHandler instance;
    public static int RemovedAds;

    void Awake () {
        instance = this;
    }

    void OnEnable () {
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.cacheRewardedVideo (CBLocation.MainMenu);
        Chartboost.cacheInterstitial (CBLocation.GameScreen);
    }

    void OnDisable () {

        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
    }

    public static void didCompleteRewardedVideo (CBLocation location, int reward) {
        // reward player
        //        UIManager.uim.GrabDailyRewardAdvert();
        GameOver.GO.DoubleReward ();
        Debug.Log ("reward player");
    }

    [Button]
    public void WatchAdvert () {
#if UNITY_EDITOR
        GameOver.GO.DoubleReward ();
#else
        if (Chartboost.hasInterstitial (CBLocation.GameScreen)) {
            Chartboost.showInterstitial (CBLocation.GameScreen);
            Debug.Log ("watching ad");
        } else {
            // We don't have a cached video right now, but try to get one for next time
            Chartboost.cacheInterstitial (CBLocation.GameScreen);
            Debug.Log ("no ad to be found");
        }
#endif

    }

    [Button]
    public void SearchAgainUsingAdvert () {
        if (Chartboost.hasRewardedVideo (CBLocation.MainMenu)) {
            Chartboost.showRewardedVideo (CBLocation.MainMenu);
            Debug.Log ("watching ad");
        } else {
            // We don't have a cached video right now, but try to get one for next time
            Chartboost.cacheRewardedVideo (CBLocation.MainMenu);
            Debug.Log ("no ad to be found");
        }
    }

    // Update is called once per frame
    void Update () {

    }
}