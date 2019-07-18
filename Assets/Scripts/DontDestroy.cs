using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	GameObject Player;

	// Use this for initialization
	void Start () {


	

	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.FindGameObjectWithTag("Player");
		if(Player != null){
//			transform.parent = Player.transform;
			transform.position = Player.transform.position;
		}
		DontDestroyOnLoad (this.gameObject);
	}
}
