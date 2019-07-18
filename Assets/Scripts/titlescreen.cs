using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class titlescreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Invoke ("StartGame");
		Application.LoadLevel(Application.loadedLevel+1);
	
		PlayerPrefs.SetInt("Intro",0);
	}
	
	// Update is called once per frame
	void StartGame () {

	}
}
