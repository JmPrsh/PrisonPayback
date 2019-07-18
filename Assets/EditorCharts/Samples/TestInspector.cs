using UnityEngine;
using System.Collections;

public class TestInspector : MonoBehaviour {
	
	public float random;
	public static bool check;
	
	// Use this for initialization
	void Start () {
		random = PlayerPrefs.GetFloat ("State");
	}
	
	// Update is called once per frame
	void Update () {
		if (check) {
			random = PlayerPrefs.GetFloat ("State");
			check = false;
		}
	}
}
