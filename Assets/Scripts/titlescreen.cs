using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class titlescreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Invoke ("StartGame");
		Application.LoadLevel(Application.loadedLevel+1);
//		AudioListener.volume = PlayerPrefs.GetFloat ("Volume")/20;
		PlayerPrefs.SetFloat ("Volume", 20f);
		PlayerPrefs.SetFloat ("SoundFXVolume", 20f);
//		PlayerPrefs.SetFloat ("Brightness", 0.0f);
	
		PlayerPrefs.SetInt("Intro",0);
//		PlayerPrefs.DeleteAll ();
	}
	
	// Update is called once per frame
	void StartGame () {

	}
}
