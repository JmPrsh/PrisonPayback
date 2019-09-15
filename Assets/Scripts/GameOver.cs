using System.Collections;
using System.Collections.Generic;
using ChartboostSDK;
using EasyButtons;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    public static GameOver GO;
    bool quit;
    public GameObject[] Buttons;
    public Text Scoreboard;
    public Text TotalScore;
    public Text Cigs;
    public Text[] BonusText;
    public GameObject Stats;

    public bool transfer;
    bool allowContinue;

    float tempReward;
    float TargetScore;
    bool allowTransfer;
    public Button adButton;
    float savedScore;
    int gameFinished;
    // Use this for initialization

    void Start () {
        GO = this;
        // PlayerPrefs.SetInt ("AreaProgress", 1);
        // if (CharacterStats.CS) {
        //     CharacterStats.CS.Blur = true;
        // }
        Time.timeScale = 1;
    }

    void OnEnable () {
        Chartboost.cacheRewardedVideo (CBLocation.MainMenu);
        allowTransfer = true;
        adButton.gameObject.SetActive (true);

        savedScore = (Mathf.Ceil ((CharacterStats.Score * BonusManager.multiplier) / 200));
        TargetScore = savedScore;

        Invoke ("TransferMoney", 5);
        Invoke ("ShowStats", 2);

        if (CharacterManager.SpecialsUnlocked == 0)
        {
            gameFinished++;
            if (gameFinished > 1)
            {
                PromoCanvas.instance.ShowPromo(0);
                gameFinished = 0;
            }
        }

    }

    void ShowStats () {
        Stats.SetActive (true);
    }
    public bool testAdSuccess;
    // Update is called once per frame
    void Update () {
        // if (testAdSuccess) {
        //     DoubleReward ();
        //     testAdSuccess = false;
        // }

        if (CoinManager.Instance) {
            Cigs.text = CoinManager.Instance.Coins.ToString ("00");
        }
        // }else{
        // UpdateBonusCoins ();
        // }

        Scoreboard.text = CharacterStats.Score.ToString ("000000");
        TotalScore.text = WaveManager.ScoreTotal.ToString("000000");
        if (transfer) {
            StartCoroutine (AdjustScore ());
            transferCigs ();
        }
        // if (allowContinue) {
        //     if (Input.GetButtonUp ("Cancel") && !quit) {
        //         PlayerPrefs.SetString ("LevelToLoad", "Menu");
        //         GetComponent<Animator> ().SetTrigger ("quit");
        //         Invoke ("LoadScene", 2);
        //         quit = true;

        //     }
        //     if (Input.GetButton ("Submit")) {
        //         PlayerPrefs.SetString ("LevelToLoad", "GameMode");
        //         Invoke ("LoadScene", 2);
        //     }
        // }
    }

    [Button]
    public void Restart () {
        if (!quit) {
            PlayerPrefs.SetString ("LevelToLoad", "GameMode");
            Invoke ("LoadScene", 2);
        }
    }

    [Button]
    public void WatchAd () {
        AdvertHandler.instance.SearchAgainUsingAdvert ();

    }

    [Button]
    public void Quit () {
        if (!quit) {
            PlayerPrefs.SetString ("LevelToLoad", "Main");
            GetComponent<Animator> ().SetTrigger ("quit");
            Invoke ("LoadScene", 2);
            quit = true;
        }
    }

    public void DoubleReward () {
        adButton.gameObject.SetActive (false);
        if (CoinManager.Instance)
            CoinManager.Instance.AddCoins ((int) TargetScore);

        // BonusText[0].GetComponent<Animator> ().SetTrigger ("Show");
    }

    public void LoadScene () {
        allowTransfer = false;
        if (CoinManager.Instance)
            CoinManager.Instance.AddCoins ((int) (TargetScore - tempReward));
        Application.LoadLevel ("LoadingScreen");
    }

    void TransferMoney () {
        transfer = true;

    }
    public void UpdateBonusCoins () {
        
        BonusText[0].text = $"+{TargetScore}";
        BonusText[0].gameObject.SetActive (true);
        BonusText[1].text = $"+{TargetScore}";
    }
    public float CoinsEarned;
    void transferCigs () {
        if (transfer) {
            if (tempReward < TargetScore) {
                tempReward += 1;
                CoinsEarned += 1;
                if (CoinManager.Instance) {
                    CoinManager.Instance.AddCoins (1);

                }

                //              MoneyTick.clip = TickUp;
                //              MoneyTick.GetComponent<AudioSource>().Play ();
            } else {
                tempReward = TargetScore;

            }
        }
    }

    // void AllowContinue () {
    //     foreach (GameObject buttons in Buttons) {
    //         buttons.SetActive (true);
    //     }
    //     allowContinue = true;
    // }

    IEnumerator AdjustScore () {
        while (CharacterStats.Score != 0) {
            if (CharacterStats.Score > 0) {
                CharacterStats.Score -= 5;

                //				MoneyTick.clip = TickUp;
                //				MoneyTick.GetComponent<AudioSource>().Play ();
            } else {
                CharacterStats.Score = 0;

            }
            yield return null;
            UpdateBonusCoins ();
        }

        Invoke ("AllowContinue", 1);
    }
}