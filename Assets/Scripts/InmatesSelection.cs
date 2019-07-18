using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InmatesSelection : MonoBehaviour
{
    bool pressed;
    public Transform Target;
    public float CamY;
    public float speed;
    public Animator ArrowL;
    public Animator ArrowR;
    public List<GameObject> Characters;
    public GameObject Confirm;
    public bool CharSelected;
    public int Cigs;
    public int CigsToShow;
    public int CigsCosts;
    public Text CigCounter;
    public Text[] CigCostCounter;
    public Sprite[] LockedCharacters;
    public Sprite[] UnlockedCharacters;

    public GameObject[] TutorialBoxes;

    int TutorialCompleted;
    public GameObject AButton;

    public int CharID;

    // Use this for initialization
    void Start()
    {
        Music.whichMusic = 1;
        Music.playMusic = true;
//		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");
        if (PlayerPrefs.HasKey("CigsAmount"))
        {
            Cigs = PlayerPrefs.GetInt("CigsAmount");
            CigsCosts = PlayerPrefs.GetInt("CigsCost");
        }
        else
        {
            CigsCosts = 10;
            Cigs = 10;
        }
       
       
    }

//    void ShowTutorial1()
//    {
//        TutorialBoxes[0].SetActive(true);
//        Invoke("ShowTutorial2", 4);
//    }
//
//    void ShowTutorial2()
//    {
//        TutorialBoxes[0].SetActive(false);
//        TutorialBoxes[1].SetActive(true);
//        Invoke("ShowTutorial3", 4);
//    }
//
//    void ShowTutorial3()
//    {
//        TutorialBoxes[1].SetActive(false);
//        TutorialBoxes[2].SetActive(true);
//        Tutorial.AllowCharacterChoose = true;
//        AButton.SetActive(true);
//    }

    void DieForSure()
    {
		
        this.Recycle();
		
    }
	
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AdjustCigs());
//		CigCostCounter = GameObject.Find ("Cost").GetComponents<Text> ();
        foreach (Text t in CigCostCounter)
        {
            t.text = CigsCosts.ToString("00");
        }
        CigCounter.text = CigsToShow.ToString("00");
        foreach (GameObject c in Characters)
        {
            if (EventSystem.current.currentSelectedGameObject == c)
            {
                int index = Characters.IndexOf(c);
                CharacterStats.CharacterSpriteID = index;
                PlayerPrefs.SetInt("CharacterID", index);

            }
			
        }



//		foreach (GameObject c in Characters) {
//			if (EventSystem.current.currentSelectedGameObject == c) {
//				//					Debug.Log ("Character chosen " + c);
//				// make the selection character be the character we play as
//				CharSelected = true;
//				int index = Characters.IndexOf(c);
//				//					Debug.Log ("c ID " + index);
//				PlayerPrefs.SetInt("CharacterID",index);
//			}
//			
//		}

        Target = Characters[CharID].transform;// EventSystem.current.currentSelectedGameObject.transform;
        EventSystem.current.SetSelectedGameObject(Target.gameObject);
//		Debug.Log ("SelectedGameObject " + EventSystem.current.currentSelectedGameObject);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x, transform.position.y, Target.position.z - 10), step);
//		this.transform.position = new Vector3(Target.position.x,Target.position.y+CamY,Target.position.z-10);
        if (Input.GetAxis("Horizontal") > 0)
        {
            ArrowL.SetBool("Highlighted", false);
            ArrowR.SetBool("Highlighted", true);
            CharSelected = false;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            ArrowL.SetBool("Highlighted", true);
            ArrowR.SetBool("Highlighted", false);
            CharSelected = false;
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            ArrowR.SetBool("Highlighted", false);
            ArrowL.SetBool("Highlighted", false);
        }

        if (Input.GetButton("Cancel") && CharSelected)
        {
            CharSelected = false;
            EventSystem.current.currentSelectedGameObject.GetComponent<Animator>().SetTrigger("back");
        }
        foreach (GameObject c in Characters)
        {
            if (EventSystem.current.currentSelectedGameObject == c)
            {
                if (c.GetComponent<Purchased>().purchased == 1)
                {
                    if (CharSelected)
                    {
                        Confirm.GetComponent<Text>().text = "Confirm";
                        GetComponent<GoBack>().enabled = false;
                    }
                    else
                    {
                        Confirm.GetComponent<Text>().text = "Select";
                        GetComponent<GoBack>().enabled = true;
                    }
                }
                else
                {
                    if (!CharSelected)
                    {
                        Confirm.GetComponent<Text>().text = "Unlock";
                    }
                    else
                    {
                        Confirm.GetComponent<Text>().text = "Confirm";
                    }
                }
            }
        }

//		if (Input.GetButtonUp ("Cancel")) {
//			if(!CharSelected && !pressed){
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
//			Invoke ("goback", 0.5f);
//				pressed= true;
//			}
//		}

    }


    public GameObject temp;

    public void LeftArrow()
    {
        CharSelected = false;
        if (CharID > 0)
            CharID -= 1;
    }

    public void RightArrow()
    {
        CharSelected = false;
        if (CharID < Characters.Count - 1)
            CharID += 1;
    }

    public void ChooseToPlay()
    {


    }

    IEnumerator AdjustCigs()
    {
        while (CigsToShow != Cigs)
        {
            if (CigsToShow < Cigs)
            {
                CigsToShow = Cigs;
//				MoneyTick.clip = TickUp;
//				MoneyTick.GetComponent<AudioSource>().Play ();
            }
            else
            {
                CigsToShow -= 1;
//				MoneyTick.clip = TickDown;
//				MoneyTick.GetComponent<AudioSource>().Play ();
            }
            yield return new WaitForSeconds(1);
			
        }
		
    }

    public void hightbutton()
    {
        GetComponent<AudioSource>().clip = Menu.staticSwipesound;
        GetComponent<AudioSource>().Play();
		
    }

}
