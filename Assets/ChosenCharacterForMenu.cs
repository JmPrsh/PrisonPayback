using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenCharacterForMenu : MonoBehaviour {

    public Sprite[] Characters;
    Image sr;

    void Awake(){
        sr = GetComponent<Image>();
    }

	// Use this for initialization
	void Start () {
        sr.sprite = Characters[CharacterManager.Instance.CurrentCharacterIndex];
	}
}
