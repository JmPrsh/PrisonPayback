using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CurrentLevel : MonoBehaviour {

	public string currentLevel;

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = currentLevel;
		Invoke ("RemoveSelf", 3);
	}
	
	// Update is called once per frame
	void RemoveSelf () {
//		this.Recycle ();
		if (this.transform.parent != null) {
			this.transform.parent.Recycle();
		}
	}
}
