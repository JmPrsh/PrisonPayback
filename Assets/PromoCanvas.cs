using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoCanvas : MonoBehaviour
{
    public static PromoCanvas instance;
    public GameObject[] PromoScreens;
    Canvas c;
    int gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        c = GetComponent<Canvas>();

        gameStarted = PlayerPrefs.GetInt("GameStarted");
        if (CharacterManager.SpecialsUnlocked == 0)
        {
            gameStarted++;
            PlayerPrefs.SetInt("GameStarted", gameStarted);
            if (gameStarted > 5)
            {
                ShowPromo(0);
                gameStarted = 0;
            }
        }
    }

    // Update is called once per frame
    public void ShowPromo(int i)
    {
        c.enabled = true;
        PromoScreens[i].SetActive(true);
    }

    public void closeScreens()
    {
        c.enabled = false;
        foreach (GameObject g in PromoScreens)
            g.SetActive(false);
    }
}
