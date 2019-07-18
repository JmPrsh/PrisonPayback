using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundFXController : MonoBehaviour {
	
	AudioSource musicsource;
	public static bool Check;

	// Use this for initialization
	void Start () {
		musicsource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {



		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;


		foreach (GameObject go in allObjects) {
			if (go.GetComponent<AudioSource>() && go.tag != "Music"){
//				print (go.name + " has an audiosource");
				go.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundFXVolume")/20;
			}
		}
		musicsource.volume = PlayerPrefs.GetFloat("Volume")/20;



	}
}
