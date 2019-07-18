using UnityEngine;
using System.Collections;

public class EndLevelCheck : MonoBehaviour {

	BoxCollider2D boxCollider2D;
	public bool end;
	public GameObject[] Arrows;
	// Use this for initialization
	void Start () {
		foreach (Transform child in gameObject.transform) {
			if(child.name == "finishCollider"){
				boxCollider2D = child.GetComponent<BoxCollider2D> ();

			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (end) {
			boxCollider2D.enabled = true;

			foreach (Transform child in gameObject.transform) {
				if(child.name == "finishCollider"){
					
					foreach (Transform children in child) {
						children.GetComponent<SpriteRenderer>().enabled = true;

					}
					
				}
			}


		} else {
			boxCollider2D.enabled = false;

		}
	
	}




}
