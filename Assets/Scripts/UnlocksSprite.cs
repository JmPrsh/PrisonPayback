using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnlocksSprite : MonoBehaviour
{

    public int purchasedCharacter;
    Unlocks u;

    public bool Unlocked;
    public bool isFree = false;

    public bool Special;
    public int SpecialCharacterID;
    public int ObjectiveMax;

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(this.gameObject.name.ToUpper(), 0) == 1);
        }
    }

    public bool IsUnlockedSPECIAL
    {
        get
        {
            return (Special && CharacterManager.SpecialsUnlocked == 1 || Special && ObjectiveComplete());
        }
    }

    private void Awake()
    {
        u = GameObject.Find("Main Camera").GetComponent<Unlocks>();
    }

    // Use this for initialization
    void OnEnable()
    {
        if (!IsUnlocked)
        {
            foreach (GameObject s in u.Characters)
            {
                if (this.gameObject == s)
                {
                    int indexNumber = u.Characters.IndexOf(s);

                    GetComponent<Image>().sprite = u.LockedCharacters[indexNumber];
                    foreach (Transform child in s.transform)
                    {
                        if (child.name == "Bars")
                        {
                            child.gameObject.SetActive(true);
                        }
                    }

                }

            }
        }
        else
        {
            u.CharactersUnlocked.Add(gameObject);
            foreach (GameObject s in u.Characters)
            {
                if (this.gameObject == s)
                {
                    int index = u.Characters.IndexOf(s);
                    GetComponent<Image>().sprite = u.UnlockedCharacters[index];
                    foreach (Transform child in s.transform)
                    {
                        if (child.name == "Bars")
                        {
                            child.gameObject.SetActive(false);
                        }
                    }

                }
            }
        }

        if (IsUnlockedSPECIAL && Special)
        {
            u.CharactersUnlocked.Add(gameObject);
            foreach (GameObject s in u.Characters)
            {
                if (this.gameObject == s)
                {
                    int index = u.Characters.IndexOf(s);
                    GetComponent<Image>().sprite = u.UnlockedCharacters[index];
                    foreach (Transform child in s.transform)
                    {
                        if (child.name == "Bars")
                        {
                            child.gameObject.SetActive(false);
                        }
                    }

                }
            }
        }
    }

    bool ObjectiveComplete()
    {
        switch (SpecialCharacterID)
        {
            case 0:
                // zombie objective
                ObjectiveMax = 5000;
                return StatManager.Instance.ZombieKills >= ObjectiveMax;

            case 1:
                // cyborg objective
                ObjectiveMax = 50;
                return StatManager.Instance.WavesCleared >= ObjectiveMax;

            case 2:
                // gun slinger objective
                ObjectiveMax = 100;
                return StatManager.Instance.BrutesKilled >= ObjectiveMax;

            case 3:
                // agent objective
                ObjectiveMax = 10;
                return StatManager.Instance.BossesKilled >= ObjectiveMax;

            case 4:
                // samurai objective
                ObjectiveMax = 5000;
                return StatManager.Instance.CriticalsHighscore >= ObjectiveMax;

        }
        return false;
    }
}
