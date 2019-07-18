using UnityEngine;
using System.Collections;

public class RemoveChild : MonoBehaviour {
	Transform Player;
	Vector3 Target;
	bool flip;


	// Use this for initialization
	void Start () {
		transform.DetachChildren ();
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		Target = Player.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, Target, 5 * Time.deltaTime);
//		if (transform.localScale.x <= 0.4f && transform.localScale.y <= 0.4f && !flip) {
//			transform.localScale = new Vector3(transform.localScale.x += 0.1f * Time.deltaTime,transform.localScale.y += 0.1f * Time.deltaTime,transform.localScale.z);
//
//			flip = true;
//		}
//		if (transform.localScale.x >= 0.4f && transform.localScale.y >= 0.4f && flip) {
//			transform.localScale = new Vector3(transform.localScale.x -= 0.1f * Time.deltaTime,transform.localScale.y -= 0.1f * Time.deltaTime,transform.localScale.z);
//		}
		if (transform.position == Target) {
			this.Recycle();
		}

	}


}
