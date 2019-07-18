using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	public string leveltoload;
	bool pressed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel") && !pressed) {
//			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("goback", 0.5f);
			pressed = true;
		}
	}

	public void goback(){
		Application.LoadLevel (leveltoload);
	}
}
