using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;

public class Menu : MonoBehaviour
{
	bool pressed;
	EventSystem eventSystem;
	public GameObject[] LevelButtons;
	IntroScene IS;
	public Color SelectedText;

	public static AudioClip staticHighlightsound;
	public static AudioClip staticPressedsound;
	public static AudioClip staticInvalidsound;
	public static AudioClip staticSwipesound;
	public static AudioClip staticUnlocksound;

	public AudioClip highsound;
	public AudioClip unlocksound;
	public AudioClip swipesound;
	public AudioClip presssound;
	public AudioClip invalidsound;

	int TutorialCompleted;
//	GameObject BackgroundAnimation;
	// Use this for initialization
	void Start ()
	{
        AYSGOCanvas = AreYouSureGO.GetComponent<Canvas>();
        TutorialCompleted = PlayerPrefs.GetInt ("Tutorial");
		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");
//		MenuButtons = GetComponents<Button>();
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		eventSystem.SetSelectedGameObject (GameObject.Find ("CONTINUE"), new BaseEventData (eventSystem));
//		BackgroundAnimation = GameObject.Find ("BackgroundCanvas");
//		BackgroundAnimation.SetActive (true);
		IS = GetComponent<IntroScene> ();
		IS.Title.GetComponent<Image>().enabled =true;
	}

	// Update is called once per frame
	void Update ()
	{
		staticHighlightsound = highsound;
		staticPressedsound = presssound;
		staticInvalidsound = invalidsound;
		staticSwipesound = swipesound;
		staticUnlocksound = unlocksound;

		IS.Title.GetComponent<Image>().enabled =true;
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [0]) {
			LevelButtons[0].transform.localScale = new Vector2(1.2f,1.2f);
		} else {
			LevelButtons[0].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [1]) {
			LevelButtons[1].transform.localScale = new Vector2(1.2f,1.2f);
		} else {
			LevelButtons[1].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [2]) {
			LevelButtons[2].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons[2].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [3]) {
			LevelButtons[3].transform.localScale = new Vector2(1.2f,1.2f);
		} else {
			LevelButtons[3].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [4]) {
			LevelButtons[4].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons[4].transform.localScale = new Vector2(1f,1f);
		}
		if (LevelButtons [5].activeInHierarchy) {
			if (EventSystem.current.currentSelectedGameObject == LevelButtons [5]) {
				LevelButtons [5].transform.localScale = new Vector2 (1.2f, 1.2f);
				LevelButtons [5].GetComponentInChildren<Text> ().color = SelectedText;
			} else {
				LevelButtons [5].transform.localScale = new Vector2 (1f, 1f);
				LevelButtons [5].GetComponentInChildren<Text> ().color = Color.white;
			}
		}
		if (LevelButtons [6].activeInHierarchy) {
			if (EventSystem.current.currentSelectedGameObject == LevelButtons [6]) {
				LevelButtons [6].transform.localScale = new Vector2 (1.2f, 1.2f);
				LevelButtons [6].GetComponentInChildren<Text> ().color = SelectedText;
			} else {
				LevelButtons [6].transform.localScale = new Vector2 (1f, 1f);
				LevelButtons [6].GetComponentInChildren<Text> ().color = Color.white;
			}
		}
	}
	
	public void Continue ()
	{
		if (!pressed) {
			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadScene", 0.5f);
			Debug.Log ("Continued");
			GetComponent<AudioSource> ().clip = presssound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}



	public void LoadScene(){
//		BackgroundAnimation.SetActive (false);
		Application.LoadLevel ("Inmates");
	}

	public void Yes(){
		if (!pressed) {
			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadNewGame", 0.5f);
			Debug.Log ("New Game");
			PlayerPrefs.SetInt ("AreaProgress", 1);
			GetComponent<AudioSource> ().clip = presssound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}

	public void No(){
		MenuButtons.SetActive (true);
        AYSGOCanvas.enabled = false;
        eventSystem.SetSelectedGameObject (LevelButtons [0], new BaseEventData (eventSystem));
	}

	public bool StartAgain;
	public GameObject AreYouSureGO;
    Canvas AYSGOCanvas;
	public GameObject MenuButtons;

	public void NewGame ()
	{
		if (TutorialCompleted == 1) {
            AYSGOCanvas.enabled=true;
			MenuButtons.SetActive (false);
			eventSystem.SetSelectedGameObject (LevelButtons [5], new BaseEventData (eventSystem));
		} else {
			PlayerPrefs.SetString ("LevelToLoad", "Inmates");
            Application.LoadLevel ("Inmates");
		}
	}

	public void LoadNewGame(){
		//		BackgroundAnimation.SetActive (false);
		Application.LoadLevel ("Inmates");
	}

	public void Settings ()
	{
		if (!pressed) 
		{
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadSceneSettings", 0.5f);
			GetComponent<AudioSource> ().clip = presssound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}

	public void LoadSceneSettings(){
//		BackgroundAnimation.SetActive (false);
		Application.LoadLevel ("Settings");
	}

	public void Inmates ()
	{
		if (!pressed) {
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadSceneInmates", 0.5f);
			GetComponent<AudioSource> ().clip = presssound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}

	public void LoadSceneInmates(){
//		BackgroundAnimation.SetActive (false);
		Application.LoadLevel ("Inmates");
	}

	public void Controls ()
	{
		if (!pressed) {
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadSceneControls", 0.5f);
			GetComponent<AudioSource> ().clip = presssound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}
	
	public void LoadSceneControls(){
		Application.LoadLevel ("Controls");
	}

	public void Unlocks ()
	{
		if (!pressed) {
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadSceneUnlocks", 0.5f);
			GetComponent<AudioSource> ().clip = presssound; 
			GetComponent<AudioSource> ().Play ();
			pressed = true;
		}
	}

	public void LoadSceneUnlocks(){
		//		BackgroundAnimation.SetActive (false);
		Application.LoadLevel ("Unlocks");
	}

	public void HighlightedButton(){

		GetComponent<AudioSource> ().clip = highsound; 
		GetComponent<AudioSource> ().Play ();
	}

}
