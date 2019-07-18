using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SameSprite : MonoBehaviour {

	public GameObject Killstreak;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image> ().sprite = Killstreak.GetComponent<Image> ().sprite;
	}
}
