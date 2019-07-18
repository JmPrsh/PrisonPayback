using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;

//using EnergyBarToolkit;

public class PauseMenu : MonoBehaviour
{

	AmmoScript Ammo;

    public Text Health;
    public Text HealthPlayerHUD,Score,ScorePlayerHUD,Damage,FireRate,ReloadTime,Range,AmmoCost,AmmoCapacity,Pills,Milk,Needles,Powder,PillsPlayerHUD,MilkPlayerHUD,NeedlesPlayerHUD,PowderPlayerHUD;

	public GameObject Pause;
	public GameObject PlayerHUD;
	public static bool isPaused;
	public GameObject NormalCamera;
	public GameObject BlurCamera;
	public GameObject[] PauseButtons;
	public GameObject[] AreYouSureButton;
	public GameObject[] SettingsButton;
	public Sprite[] PauseButtonsNormal;
	public Sprite[] PauseButtonsRollOver;
	Vector3 OriginalButtonSize;
	Vector3 BigButtonSize;
	public Image Character;
	public Text CharactersName;
	public Text WeaponName;
	public Sprite[] CharacterSprites;
	public string[] CharacterNames;
	public string[] WeaponNames;
	public bool pressed;
	public GameObject EnemyCanvas;
	public GameObject ControlMenu;
	public GameObject StatsMenu;
	public GameObject SettingsMenu;
	Settings S;
	public Image WeaponSprite;
	public Image PlayerHUDWeapon;
	public bool aboutToLeave;
	public GameObject AreYouSure;
	bool restartPressed;
	bool quitPressed;
	EventSystem eventSystem;
	bool onSettings;
	bool showMap;
	
	// Use this for initialization
	void Start ()
	{
		isPaused = false;
		pressed = false;
		Ammo = GameObject.FindGameObjectWithTag ("Player").GetComponent<AmmoScript> ();	
		OriginalButtonSize = PauseButtons [0].transform.parent.localScale;
		BigButtonSize = PauseButtons [0].transform.parent.localScale * 1.1f;
		S = SettingsMenu.GetComponent<Settings> ();
		updateVariables ();
	}

	void updateVariables(){
		LevelToLoad = leveltoload;
//		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		Health.text = HealthPlayerHUD.text;
		Score.text = ScorePlayerHUD.text;
		Damage.text = CharacterStats.CS.DamageToShow;
		FireRate.text = CharacterStats.CS.shotInterval.ToString ();
		ReloadTime.text = Ammo.GeneralReloadTime.ToString ();
		AmmoCost.text = "1";
		AmmoCapacity.text = Ammo.GeneralMaxAmmo.ToString (); 

		Pills.text = PillsPlayerHUD.text;
		Milk.text = MilkPlayerHUD.text;
		Needles.text = NeedlesPlayerHUD.text;
		Powder.text = PowderPlayerHUD.text;

        int CSSpriteID = PlayerPrefs.GetInt("SGLIB_CURRENT_CHARACTER");
		int CSWeaponID = CharacterStats.WeaponID;
		Character.sprite = CharacterSprites [CSSpriteID];
		CharactersName.text = CharacterNames [CSSpriteID];
		WeaponName.text = WeaponNames [CSWeaponID];
	}

	// Update is called once per frame
	void Update ()
	{
		WeaponSprite.sprite = PlayerHUDWeapon.sprite;

//		if (Input.GetButtonUp ("Start")) {
//			if (!isPaused && !showMap) {
//				updateVariables ();
////				eventSystem.SetSelectedGameObject (PauseButtons [0].gameObject, new BaseEventData (eventSystem));
//				ControlMenu.SetActive (false);
//				StatsMenu.SetActive (true);
//				SettingsMenu.SetActive (false);
//				Invoke ("stopTime", 0.6f);
////				Time.timeScale = 0;
//			}
//			isPaused = !isPaused;

//		}
//		if (Tutorial.AllowShowMap) {
//			if (Input.GetButtonUp ("XboxBackButton")) {
//				if (!showMap) {
//					GameObject.Find ("Main Camera").GetComponent<Camera> ().orthographicSize = 40;
//					ControlMenu.SetActive (false);
//					StatsMenu.SetActive (false);
//					SettingsMenu.SetActive (false);
//					Time.timeScale = 0;
//					isPaused = false;
//				} else {
//					GameObject.Find ("Main Camera").GetComponent<Camera> ().orthographicSize = 6.2f;
//					Time.timeScale = 1;
//				}
//				showMap = !showMap;
			
//			}
//		}

//		if (Input.GetButtonUp ("Cancel") && Pause.activeInHierarchy) {
//
//			isPaused = false;
//
//		}

		if (isPaused) {

//			if (EventSystem.current.currentSelectedGameObject == null) {
//				eventSystem.SetSelectedGameObject (PauseButtons [0].gameObject, new BaseEventData (eventSystem));
//			}
			EnemyCanvas.SetActive (false);
			Pause.SetActive (true);

//			Image[] HUDImage = PlayerHUD.GetComponentsInChildren<Image>();
//			foreach(Image i in HUDImage){
//				i.enabled = false;
//			}

//			PlayerHUD.SetActive (false);
//			Camera.main.GetComponent<BlurOptimized> ().enabled = true;

			if (aboutToLeave) {
				AreYouSure.SetActive (true);
				foreach (GameObject GO in PauseButtons) {
					GO.GetComponent<Button> ().interactable = false;
				}
				// show are you sure page

			} else {

				AreYouSure.SetActive (false);
				foreach (GameObject GO in PauseButtons) {
					GO.GetComponent<Button> ().interactable = true;
				}
				quitPressed = false;
				restartPressed = false;
			}

			if (onSettings) {
				SettingsMenu.SetActive (true);
				foreach (GameObject GO in PauseButtons) {
					GO.GetComponent<Button> ().interactable = false;
				}
				// show are you sure page
				
			} else {
				
				SettingsMenu.SetActive (false);
				foreach (GameObject GO in PauseButtons) {
					GO.GetComponent<Button> ().interactable = true;
				}
			}

			if (Input.GetButtonUp ("Cancel") && onSettings) {
				onSettings = false;
				PlayerPrefs.SetFloat ("Volume", S.Buttons [0].GetComponent<Slider> ().value);
				PlayerPrefs.SetFloat ("SoundFXVolume", S.Buttons [6].GetComponent<Slider> ().value);
				PlayerPrefs.SetFloat ("Brightness", S.BrightnessSlider);
//				eventSystem.SetSelectedGameObject (PauseButtons [0].gameObject, new BaseEventData (eventSystem));
			}

		} else {

			if (!showMap) {
				Time.timeScale = 1;
				EnemyCanvas.SetActive (true);
				Pause.SetActive (false);
				if(!Tutorial.isTutorial){
					PlayerHUD.SetActive (true);
				}
//				Camera.main.GetComponent<BlurOptimized> ().enabled = false;
				pressed = false;
//				eventSystem.SetSelectedGameObject (null, new BaseEventData (eventSystem));

			}
		}


//		// event system
//		if (EventSystem.current.currentSelectedGameObject == PauseButtons [0]) {
//			PauseButtons [0].GetComponent<Image> ().sprite = PauseButtonsRollOver [0];
//			PauseButtons [0].transform.parent.localScale = BigButtonSize;
//			ControlMenu.SetActive (false);
//			StatsMenu.SetActive (true);
//			SettingsMenu.SetActive (false);
//		} else {
//			PauseButtons [0].GetComponent<Image> ().sprite = PauseButtonsNormal [0];
//			PauseButtons [0].transform.parent.localScale = OriginalButtonSize;
//		}
//		if (EventSystem.current.currentSelectedGameObject == PauseButtons [1]) {
//			PauseButtons [1].GetComponent<Image> ().sprite = PauseButtonsRollOver [1];
//			PauseButtons [1].transform.parent.localScale = BigButtonSize;
//			ControlMenu.SetActive (false);
//			StatsMenu.SetActive (true);
//			SettingsMenu.SetActive (false);
//		} else {
//			PauseButtons [1].GetComponent<Image> ().sprite = PauseButtonsNormal [1];
//			if (!restartPressed) {
//				PauseButtons [1].transform.parent.localScale = OriginalButtonSize;
//			}
//		}
//		if (EventSystem.current.currentSelectedGameObject == PauseButtons [2]) {
//			PauseButtons [2].GetComponent<Image> ().sprite = PauseButtonsRollOver [2];
//			PauseButtons [2].transform.parent.localScale = BigButtonSize;
//			
//		} else {
//			PauseButtons [2].GetComponent<Image> ().sprite = PauseButtonsNormal [2];
//			PauseButtons [2].transform.parent.localScale = OriginalButtonSize;
//		}
//		if (EventSystem.current.currentSelectedGameObject == PauseButtons [3]) {
//			PauseButtons [3].GetComponent<Image> ().sprite = PauseButtonsRollOver [3];
//			PauseButtons [3].transform.parent.localScale = BigButtonSize;
//			pressed = false;
//			
//		} else {
//			PauseButtons [3].GetComponent<Image> ().sprite = PauseButtonsNormal [3];
//			PauseButtons [3].transform.parent.localScale = OriginalButtonSize;
//		}
//		if (EventSystem.current.currentSelectedGameObject == PauseButtons [4]) {
//			PauseButtons [4].GetComponent<Image> ().sprite = PauseButtonsRollOver [4];
//			PauseButtons [4].transform.parent.localScale = BigButtonSize;
//			
//		} else {
//			PauseButtons [4].GetComponent<Image> ().sprite = PauseButtonsNormal [4];
//			PauseButtons [4].transform.parent.localScale = OriginalButtonSize;
//		}
//		if (EventSystem.current.currentSelectedGameObject == PauseButtons [5]) {
//			PauseButtons [5].GetComponent<Image> ().sprite = PauseButtonsRollOver [5];
//			PauseButtons [5].transform.parent.localScale = BigButtonSize;
//			ControlMenu.SetActive (false);
//			StatsMenu.SetActive (true);
//			SettingsMenu.SetActive (false);
//			pressed = false;
//			
//		} else {
//			PauseButtons [5].GetComponent<Image> ().sprite = PauseButtonsNormal [5];
//			if (!quitPressed) {
//				PauseButtons [5].transform.parent.localScale = OriginalButtonSize;
//			}
//		}
//
//		if (AreYouSureButton [0].transform.parent.gameObject.activeInHierarchy) {
//			if (EventSystem.current.currentSelectedGameObject == AreYouSureButton [0]) {
//				AreYouSureButton [0].GetComponentInChildren<Text> ().color = CS.OrangeColour;
//			} else {
//				AreYouSureButton [0].GetComponentInChildren<Text> ().color = Color.white;
//			}
//
//			if (EventSystem.current.currentSelectedGameObject == AreYouSureButton [1]) {
//				AreYouSureButton [1].GetComponentInChildren<Text> ().color = CS.OrangeColour;
//			} else {
//				AreYouSureButton [1].GetComponentInChildren<Text> ().color = Color.white;
//			}
//		}
//
//		if (SettingsButton [0].transform.parent.gameObject.activeInHierarchy) {
//			if (EventSystem.current.currentSelectedGameObject == SettingsButton [0]) {
//			} else {
//			}
//			
//			if (EventSystem.current.currentSelectedGameObject == SettingsButton [1]) {
//			} else {
//			}
//		}



	}

    public void PauseButtonUI(){
        if (!isPaused && !showMap) {
            updateVariables ();
//            eventSystem.SetSelectedGameObject (PauseButtons [0].gameObject, new BaseEventData (eventSystem));
            ControlMenu.SetActive (false);
            StatsMenu.SetActive (true);
            SettingsMenu.SetActive (false);
            Invoke ("stopTime", 0.6f);
            //              Time.timeScale = 0;
        }
        isPaused = !isPaused;

    }

	IEnumerator PressedBool ()
	{
		pressed = true;
		yield return new WaitForSeconds (0.2f);
		pressed = false;

	}

	public void Resume ()
	{
		isPaused = false;
		GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
		GetComponent<AudioSource> ().Play ();
	}

	public void stopTime ()
	{
		Time.timeScale = 0;

	}

	public static string LevelToLoad;
	public string leveltoload;

	public void Restart ()
	{
		pressed = false;
		if (!pressed) {
			GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
			GetComponent<AudioSource> ().Play ();
			StartCoroutine (PressedBool ());
//			Debug.Log ("pressed");
			restartPressed = true;
//			PauseButtons [1].transform.parent.localScale = BigButtonSize;
			Time.timeScale = 1;
			aboutToLeave = true;
//			eventSystem.SetSelectedGameObject (AreYouSureButton [0].gameObject, new BaseEventData (eventSystem));
		}
	}

	public void Yes ()
	{
		if (!pressed) {
			GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
			GetComponent<AudioSource> ().Play ();
			Time.timeScale = 1;
			if (restartPressed) {

				PlayerPrefs.SetString ("LevelToLoad", Application.loadedLevelName);
			}
			if (quitPressed) {
				PlayerPrefs.SetString ("LevelToLoad", "Main");
			}
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadScene", 2);

		}

	}

	public void No ()
	{
		aboutToLeave = false;
//		Time.timeScale = 0;
		GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
		GetComponent<AudioSource> ().Play ();
//		eventSystem.SetSelectedGameObject (PauseButtons [0].gameObject, new BaseEventData (eventSystem));
	}

	public void ShowAchievements ()
	{
		if (!pressed) {
			GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}

	public void ShowControls ()
	{
		GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
		GetComponent<AudioSource> ().Play ();
		ControlMenu.SetActive (true);
		StatsMenu.SetActive (false);
		SettingsMenu.SetActive (false);

	}

	public void Settings ()
	{
		if (!onSettings) {
			GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
			GetComponent<AudioSource> ().Play ();
			ControlMenu.SetActive (false);
			StatsMenu.SetActive (false);
			SettingsMenu.SetActive (true);
			onSettings = true;
//			eventSystem.SetSelectedGameObject (SettingsButton [0].gameObject, new BaseEventData (eventSystem));
		}
	}
	
	public void Quit ()
	{
		pressed = false;
		if (!pressed) {
			GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
			GetComponent<AudioSource> ().Play ();
			StartCoroutine (PressedBool ());
//			Debug.Log ("pressed");
			quitPressed = true;
//			PauseButtons [5].transform.parent.localScale = BigButtonSize;
			Time.timeScale = 1;
			aboutToLeave = true;
//			eventSystem.SetSelectedGameObject (AreYouSureButton [0].gameObject, new BaseEventData (eventSystem));
		}
	}

	public void LoadScene ()
	{
		Application.LoadLevel ("LoadingScreen");
	}

	public void HighlightedButton ()
	{
		
		GetComponent<AudioSource> ().clip = Menu.staticHighlightsound; 
		GetComponent<AudioSource> ().Play ();
	}

	
}
