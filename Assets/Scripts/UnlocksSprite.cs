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

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(this.gameObject.name.ToUpper(), 0) == 1);
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        u = GameObject.Find("Main Camera").GetComponent<Unlocks>();
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
	
   
}
