using System;
using System.Collections;
using System.Collections.Generic;
using ChartboostSDK;
using EasyButtons;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour {
    public int MinutesToWait = 15;
    public GameObject WatchedAdvertScreen;
    public GameObject BonusButton;
    public Button bonusbttn;
    public Text bonusTxt;
    public Text ActiveTxt;
    public static int multiplier = 1;

    public DateTime NextRewardTime {
        get {
            return GetNextRewardTime ();
        }
    }

    public TimeSpan TimeUntilReward {
        get {
            return NextRewardTime.Subtract (DateTime.Now);
        }
    }

    private void Awake () {
        // OpenBonusScreen();
    }
    // Start is called before the first frame update
    void OnEnable () {
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.cacheRewardedVideo (CBLocation.GameScreen);

    }
    void OnDisable () {

        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.Find ("BonusButton")) {
            if (BonusButton == null) {
                BonusButton = GameObject.Find ("BonusButton");
                BonusButton.GetComponent<Button> ().onClick.AddListener (() => OpenBonusScreen ());
                bonusbttn = BonusButton.GetComponent<Button> ();
                ActiveTxt = GameObject.Find ("ActiveTxt").GetComponent<Text> ();
                bonusTxt = GameObject.Find ("BonusTimer").GetComponent<Text> ();
            }
        }
        if (bonusbttn)
            bonusbttn.interactable = CanRewardNow ();
        if (ActiveTxt)
            ActiveTxt.enabled = !CanRewardNow ();
        if (CanRewardNow ()) {
            if (bonusTxt)
                bonusTxt.text = "READY!";
            multiplier = 1;
        } else {
            TimeSpan timeToReward = TimeUntilReward;
            if (bonusTxt)
                bonusTxt.text = string.Format ("{0:00}:{1:00}", timeToReward.Minutes, timeToReward.Seconds);
            multiplier = 3;
        }
    }

    [Button]
    public void OpenBonusScreen () {
        WatchedAdvertScreen.SetActive (true);
        print ("Open Screen");
    }

    [Button]
    public void ShowRewardAdThenResetTime () {
#if UNITY_EDITOR
        ResetNextRewardTime ();
#else

        if (Chartboost.hasRewardedVideo (CBLocation.GameScreen)) {
            Chartboost.showRewardedVideo (CBLocation.GameScreen);
        } else {
            // We don't have a cached video right now, but try to get one for next time
            Chartboost.cacheRewardedVideo (CBLocation.GameScreen);
        }
#endif
    }
    public void didCompleteRewardedVideo (CBLocation location, int reward) {
        ResetNextRewardTime ();
    }

    [Button]
    public void ResetNextRewardTime () {
        DateTime next = DateTime.Now.Add (new TimeSpan (0, MinutesToWait, 0));
        StoreNextRewardTime (next);
          TimeSpan timeToReward = TimeUntilReward;
            if (bonusTxt)
                bonusTxt.text = string.Format ("{0:00}:{1:00}", timeToReward.Minutes, timeToReward.Seconds);
    }

    DateTime GetNextRewardTime () {
        string storedTime = PlayerPrefs.GetString ("BonusReward", string.Empty);

        if (!string.IsNullOrEmpty (storedTime))
            return DateTime.FromBinary (Convert.ToInt64 (storedTime));
        else
            return DateTime.Now;
    }

    void StoreNextRewardTime (DateTime time) {
        PlayerPrefs.SetString ("BonusReward", time.ToBinary ().ToString ());
        GetNextRewardTime ();
        PlayerPrefs.Save ();
    }

    public bool CanRewardNow () {
        return TimeUntilReward <= TimeSpan.Zero;
    }
}