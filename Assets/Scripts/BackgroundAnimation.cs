using UnityEngine;
using System.Collections;

public class BackgroundAnimation : MonoBehaviour {

	public static BackgroundAnimation Instance;

	// Use this for initialization
	void Start () {
		if (Instance) {
			DestroyImmediate (gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
