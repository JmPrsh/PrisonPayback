using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteRendererCheck : MonoBehaviour {

	public bool SROFF;
	public static SpriteRenderer[] sprites;

	// Use this for initialization
	void Start () {

		sprites = GetComponentsInChildren<SpriteRenderer>();
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if (SROFF) {
			for (int i = 0; i < sprites.Length; i++) {
				sprites [i].enabled = false;
			}
		} else {
			for(int i = 0; i < sprites.Length; i++){
				sprites[i].enabled = true;
			}
		}

	}
}
