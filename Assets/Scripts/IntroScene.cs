using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroScene : MonoBehaviour {

	bool pressed;
	public GameObject PressA;
	EventSystem eventSystem;
	public Animator anim;
	public static int AlreadySeenIntro;
	public Animator IntroAnim;
	public GameObject MenuGO;
	public GameObject Title;
	public GameObject Continue;
	bool canPlay;

	public AudioClip GameClip;
	AudioSource music;

	void OnEnable(){
		PressA.SetActive (false);
	}

	void Awake(){
		PressA.SetActive (false);
	}


	// Use this for initialization
	void Start () {



		if (Music.whichMusic != 1) {
			Music.whichMusic = 1;
			Music.playMusic = true;

		}
		pressed = false;
		Title.GetComponent<Image>().enabled =true;
		AlreadySeenIntro = PlayerPrefs.GetInt ("Intro");
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();

		if (AlreadySeenIntro == 0) {
			StartCoroutine (StartTimer ());
			// play animation
			IntroAnim.SetTrigger("PlayIntro");
//			Invoke ("ShowA", 5);
		} else {
			// animation already played



			Invoke ("ShowMenuStart", 1);
			Invoke ("ShowMenuTitle", 1);
			IntroAnim.SetTrigger("SkipIntro");
		}


	}
	
	void ShowA(){
		PressA.SetActive (true);
		
	}

	void ShowMenuTitle(){

		anim.SetTrigger ("pressed");

		
	}

	void ShowMenuStart(){
		MenuGO.SetActive(true);
//		anim.gameObject.SetActive(true);


		eventSystem.SetSelectedGameObject (Continue, new BaseEventData (eventSystem));

	}

	IEnumerator StartTimer(){
		PressA.SetActive (false);
		yield return new WaitForSeconds (5);
		PressA.SetActive (true);
		yield return new WaitForSeconds (1);
		canPlay = true;

	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("whichMusic " + Music.whichMusic);
		if (Input.GetButton ("Submit") && !pressed && AlreadySeenIntro == 0 && canPlay) {
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			anim.SetTrigger ("pressed");
			GetComponent<AudioSource> ().clip = Menu.staticPressedsound; 
			GetComponent<AudioSource> ().Play ();
			PressA.SetActive (false);
			Invoke ("LoadScene", 0.5f);
			pressed = true;
			AlreadySeenIntro = 1;
			PlayerPrefs.SetInt ("Intro", 1);
			eventSystem.SetSelectedGameObject (Continue, new BaseEventData (eventSystem));
		}

		if (AlreadySeenIntro == 1) {
			PressA.SetActive (false);
			Title.GetComponent<Image>().enabled =true;
		} 

	}

	public void LoadScene(){
		PressA.SetActive (false);
		MenuGO.SetActive(true);
//		Application.LoadLevelAdditiveAsync (Application.loadedLevel + 1);
//		eventSystem.SetSelectedGameObject (GameObject.Find ("CONTINUE"), new BaseEventData (eventSystem));
	}
}
