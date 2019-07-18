using UnityEngine;
using System.Collections;

public class Roof : MonoBehaviour {

	SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		SR = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerStay2D ( Collider2D other ){
		if (other.GetComponent<Collider2D> ().tag == "Player") {
			SR.enabled = false;
		}

	}

	void OnTriggerExit2D ( Collider2D other ){
		if (other.GetComponent<Collider2D> ().tag == "Player") {
//			SR.enabled = true;
		}
		
	}
}
