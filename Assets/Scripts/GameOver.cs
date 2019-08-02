using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver GO;
    bool quit;
    public GameObject[] Buttons;
    public Text Scoreboard;
    public Text Cigs;

    public GameObject Stats;

    public bool transfer;
    bool allowContinue;

    float tempReward;
    float TargetScore;
    bool allowTransfer;
    public Button adButton;
    float savedScore;
    // Use this for initialization

    void Start()
    {
        GO = this;
        PlayerPrefs.SetInt("AreaProgress", 1);
        if (CharacterStats.CS)
        {
            CharacterStats.CS.Blur = true;
        }


    }

    void OnEnable()
    {
        allowTransfer = true;
        adButton.gameObject.SetActive(true);

        savedScore = (Mathf.Ceil(CharacterStats.Score / 100));
        TargetScore = savedScore;

        Invoke("TransferMoney", 5);
        Invoke("ShowStats", 2);
    }

    void ShowStats()
    {
        Stats.SetActive(true);
    }
    public bool testAdSuccess;
    // Update is called once per frame
    void Update()
    {
        if (testAdSuccess)
        {
            DoubleReward();
            testAdSuccess = false;
        }

        if (SgLib.CoinManager.Instance)
        {
            Cigs.text = SgLib.CoinManager.Instance.Coins.ToString("00");
        }

        Scoreboard.text = CharacterStats.Score.ToString("000000");
        if (transfer)
        {
            StartCoroutine(AdjustScore());
            transferCigs();
        }
        if (allowContinue)
        {
            if (Input.GetButtonUp("Cancel") && !quit)
            {
                PlayerPrefs.SetString("LevelToLoad", "Menu");
                GetComponent<Animator>().SetTrigger("quit");
                Invoke("LoadScene", 2);
                quit = true;

            }
            if (Input.GetButton("Submit"))
            {
                PlayerPrefs.SetString("LevelToLoad", "GameMode");
                Invoke("LoadScene", 2);
            }
        }
    }

    public void Restart()
    {
        if (!quit)
        {
            PlayerPrefs.SetString("LevelToLoad", "GameMode");
            Invoke("LoadScene", 2);
        }
    }

    public void Quit()
    {
        if (!quit)
        {
            PlayerPrefs.SetString("LevelToLoad", "Main");
            GetComponent<Animator>().SetTrigger("quit");
            Invoke("LoadScene", 2);
            quit = true;
        }
    }

    public void DoubleReward()
    {
        adButton.gameObject.SetActive(false);
        SgLib.CoinManager.Instance.AddCoins((int)savedScore);
        
    }

    public void LoadScene()
    {
        allowTransfer = false;
        if (SgLib.CoinManager.Instance)
            SgLib.CoinManager.Instance.AddCoins((int)(TargetScore - tempReward));
        Application.LoadLevel("LoadingScreen");
    }

    void TransferMoney()
    {
        transfer = true;

    }

    void transferCigs()
    {
        if (transfer)
        {
            if (tempReward < TargetScore)
            {
                tempReward += 1;
                SgLib.CoinManager.Instance.AddCoins(1);

                //              MoneyTick.clip = TickUp;
                //              MoneyTick.GetComponent<AudioSource>().Play ();
            }
            else
            {
                tempReward = TargetScore;
            }
        }
    }

    void AllowContinue()
    {
        foreach (GameObject buttons in Buttons)
        {
            buttons.SetActive(true);
        }
        allowContinue = true;
    }

    IEnumerator AdjustScore()
    {
        while (CharacterStats.Score != 0)
        {
            if (CharacterStats.Score > 0)
            {
                CharacterStats.Score -= 5;


                //				MoneyTick.clip = TickUp;
                //				MoneyTick.GetComponent<AudioSource>().Play ();
            }
            else
            {
                CharacterStats.Score = 0;
            }
            yield return null;

        }



        Invoke("AllowContinue", 1);
    }
}
