using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Purchased : MonoBehaviour
{

    public int purchased;
    bool showCost = true;
    InmatesSelection IM;
    public Image selfSprite;
    public int SpriteID;
    public GameObject Padlock;
    Animator anim;
    public GameObject Cigs;
    public GameObject Costs;
    Image CigCounterImage;
    Text CigCounter;
    GameObject Confirm;
    int PlayedTutorial;
    Transform[] children;
    // Use this for initialization
    void Start()
    {
        IM = GameObject.Find("Main Camera").GetComponent<InmatesSelection>();
        PlayedTutorial = PlayerPrefs.GetInt("Tutorial");
        CigCounter = GameObject.Find("CigCounter").GetComponent<Text>();
        CigCounterImage = GameObject.Find("CigCounterImage").GetComponent<Image>();
        Confirm = IM.AButton;
        children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "Cigs")
            {
                Cigs = child.gameObject;
            }
            if (child.name == "Padlock")
            {
                Padlock = child.gameObject;
            }
            if (child.name == "Costs")
            {
                Costs = child.gameObject;
            }
        }
        selfSprite = GetComponent<Image>();

        if (this.name != "1" || this.name != "2" || this.name != "3")
        {
            purchased = PlayerPrefs.GetInt(this.gameObject.name);
		

            if (purchased == 0)
            {
                GetComponent<Button>().onClick.RemoveAllListeners();
                GetComponent<Button>().onClick.AddListener(() => ChooseCharacter());
            }
            else
            {
                GetComponent<Button>().onClick.RemoveAllListeners();
                GetComponent<Button>().onClick.AddListener(() => ChooseCharacterToPlay());
            }
        }
        else
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(() => ChooseCharacterToPlay());
        }

        foreach (Transform p in gameObject.transform)
        {

            if (p.gameObject.name == "Padlock")
            {
                Padlock = p.transform.gameObject;
                anim = Padlock.GetComponent<Animator>();	
					
            }
            if (p.gameObject.name == "Cigs")
            {
                Cigs = p.transform.gameObject;

				
            }
            if (p.gameObject.name == "Cost")
            {
                Costs = p.transform.gameObject;

				
            }
				
        }

			
        if (purchased == 0)
        { // locked
            foreach (GameObject c in IM.Characters)
            {
                if (this.gameObject == c)
                {
                    int index = IM.Characters.IndexOf(c);
                    GetComponent<Image>().sprite = IM.LockedCharacters[index];
                }
				
            }


			
			
        }
        else
        { // unlocked
            Cigs.SetActive(false);
            Costs.SetActive(false);
            Padlock.SetActive(false);
//			GetComponent<Button> ().onClick.RemoveAllListeners ();
//			GetComponent<Button> ().onClick.AddListener (() => ChooseCharacterToPlay ());
            foreach (GameObject c in IM.Characters)
            {
                if (this.gameObject == c)
                {
                    int index = IM.Characters.IndexOf(c);
                    GetComponent<Image>().sprite = IM.UnlockedCharacters[index];
                    GetComponent<Button>().onClick.RemoveAllListeners();
                    GetComponent<Button>().onClick.AddListener(() => ChooseCharacterToPlay());
                }
				
            }
			

        }

    }
	
    // Update is called once per frame
    void Update()
    {
        if (this.name == "1" || this.name == "2" || this.name == "3")
        {
            purchased = 1;
            Cigs.SetActive(false);
            Costs.SetActive(false);
            Padlock.SetActive(false);
        }
        if (purchased == 0)
        { // locked
//			selfSprite.color = Color.black;
            foreach (GameObject c in IM.Characters)
            {
                if (this.gameObject == c)
                {
                    int index = IM.Characters.IndexOf(c);
                    GetComponent<Image>().sprite = IM.LockedCharacters[index];
                }
				
            }
//			Cigs.SetActive (true);
//			Costs.SetActive (true);
            if (EventSystem.current.currentSelectedGameObject == this.gameObject && showCost)
            {
                Cigs.SetActive(true);
                Costs.SetActive(true);
            }
            else
            {
                Cigs.SetActive(false);
                Costs.SetActive(false);
            }

		
        }
        else
        { // unlocked
//			selfSprite.color = Color.white;
            Cigs.SetActive(false);
            Costs.SetActive(false);
            foreach (GameObject c in IM.Characters)
            {
                if (this.gameObject == c)
                {
                    int index = IM.Characters.IndexOf(c);
                    GetComponent<Image>().sprite = IM.UnlockedCharacters[index];
                }
				
            }

        }

    }

    public void ChooseCharacter()
    {
//		StartCoroutine(ButtonInteractionA());
        if (this.name != "1" && this.name != "2" && this.name != "3")
        {
            if (IM.CharSelected)
            {
                if (purchased == 0 && IM.Cigs >= IM.CigsCosts)
                {
                    IM.Cigs -= IM.CigsCosts;
                    IM.CigsCosts += 10;
                    showCost = false;
                    Cigs.SetActive(false);
                    Costs.SetActive(false);
                    Debug.Log(this.gameObject.name + " has been purchased and his bool is " + purchased);
                    PlayerPrefs.SetInt("CigsAmount", IM.Cigs);
                    PlayerPrefs.SetInt("CigsCost", IM.CigsCosts);
                    IM.GetComponent<AudioSource>().clip = Menu.staticUnlocksound;
                    IM.GetComponent<AudioSource>().Play();
//					Debug.Log ("Character Purchased!!");

                    GetComponent<Button>().onClick.RemoveAllListeners();
                    GetComponent<Button>().onClick.AddListener(() => ChooseCharacterToPlay());

                    StartCoroutine(UnlockCharacter());
                    anim.SetTrigger("Unlock");
                }
                else
                {
                    IM.GetComponent<AudioSource>().clip = Menu.staticInvalidsound;
                    IM.GetComponent<AudioSource>().Play();
//				Debug.Log ("Cant Afford");
                    // not enough cigs
                    StartCoroutine(NoFunds());
                }
            }
            else
            {
                if (EventSystem.current.currentSelectedGameObject == this.gameObject)
                {
//					Debug.Log ("Character Selected.... Confirm Purchase?");
                    IM.CharSelected = true;
                    IM.GetComponent<AudioSource>().clip = Menu.staticPressedsound;
                    IM.GetComponent<AudioSource>().Play();
                }
                else
                {
                    IM.CharSelected = false;
                }
				
				
            }
        }	

    }

    IEnumerator NoFunds()
    {
        CigCounterImage.color = Color.red;
        CigCounter.color = Color.red;
        anim.SetTrigger("nofunds");
        yield return new WaitForSeconds(0.2f);
        CigCounterImage.color = Color.white;
        CigCounter.color = Color.white;

    }

    IEnumerator UnlockCharacter()
    {
        Padlock.SetActive(true);
        yield return new WaitForSeconds(1);
        purchased = 1;
        PlayerPrefs.SetInt(this.gameObject.name, purchased);
    }

    IEnumerator ButtonInteractionA()
    {
//		Confirm.transform.localScale = new Vector2(0.41f,0.41f);
        yield return new WaitForSeconds(0.2f);
//		Confirm.transform.localScale = new Vector2(0.4f,0.4f);
    }

    public void ChooseCharacterToPlay()
    {
//		if (Tutorial.AllowCharacterChoose) {
//			StartCoroutine (ButtonInteractionA ());
        if (purchased == 1)
        {
            if (IM.CharSelected)
            {
//					GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
                Invoke("LoadSceneInmates", 0.5f);
                IM.GetComponent<AudioSource>().clip = Menu.staticPressedsound;
                IM.GetComponent<AudioSource>().Play();
            }
            else
            {
                if (EventSystem.current.currentSelectedGameObject == this.gameObject)
                {
                    GetComponent<Animator>().SetTrigger("NormalPurchased");
                    IM.CharSelected = true;
                    IM.GetComponent<AudioSource>().clip = Menu.staticPressedsound;
                    IM.GetComponent<AudioSource>().Play();
                }
                else
                {
                    IM.CharSelected = false;
                }
				
				
            }
        }
//		}
    }

    void LoadSceneInmates()
    {
        if (PlayedTutorial == 0)
        {
            PlayerPrefs.SetString("LevelToLoad", "TitleScreen");
            Application.LoadLevel("LoadingScreen");
//			PlayerPrefs.SetInt ("Tutorial",1);
        }
        else
        {
            PlayerPrefs.SetString("LevelToLoad", "LevelMap");
            Application.LoadLevel("LoadingScreen");
        }
    }
}
