using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;

public class AdvertHandler : MonoBehaviour {

	

    void OnEnable()
    {
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.cacheRewardedVideo(CBLocation.MainMenu);
    }

    void OnDisable()
    {

        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
    }

    public static void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        // reward player
//        UIManager.uim.GrabDailyRewardAdvert();
        GameOver.GO.DoubleReward();
        Debug.Log("reward player");
    }

    public void SearchAgainUsingAdvert()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.MainMenu))
        { 
            Chartboost.showRewardedVideo(CBLocation.MainMenu);
            Debug.Log("watching ad");
        }
        else
        {
            // We don't have a cached video right now, but try to get one for next time
            Chartboost.cacheRewardedVideo(CBLocation.MainMenu);
            Debug.Log("no ad to be found");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
