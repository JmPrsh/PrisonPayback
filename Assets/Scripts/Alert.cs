using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {

	public Color Red;
	public Color White;
	public Transform Parent;
	attackPlayer AP;
//	Transform Player;

	void Start(){
		GetComponent<SpriteRenderer>().color = White;
		AP = Parent.GetComponent<attackPlayer> ();
//		Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	//void OnTriggerEnter2D (Collider2D other){
	//	if (other.gameObject.CompareTag ("Player")) {
		
	//		GetComponent<SpriteRenderer>().color = Red;
	//		AP.startMoving = true;
	//		if (AP.typeofenemy == attackPlayer.TypeOfEnemy.Pistol) {
	//			AP.startShooting = true;
	//		}
	//		this.Recycle();
	//	}

	//}



}
