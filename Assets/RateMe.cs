using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Yes(){
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.JamieParish.PrisonPayback");
        this.gameObject.SetActive(false);
    }

    public void Later(){
        CharacterStats.GamesPlayed = 0;
        PlayerPrefs.SetInt("GamesPlayed", CharacterStats.GamesPlayed);
        this.gameObject.SetActive(false);
    }

    public void No(){
        this.gameObject.SetActive(false);
    }
}
