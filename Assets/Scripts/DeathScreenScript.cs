using UnityEngine;
using System.Collections;

public class DeathScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonUp ("Submit")) {
			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
			Invoke ("LoadScene", 0.5f);
		}


	}

	public void LoadScene(){
		Application.LoadLevel(Application.loadedLevel);
	}
}
