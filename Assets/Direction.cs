using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour {
    float x;
    public bool flip;
    public RightJoystick rightJoystick;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        x = rightJoystick.GetInputDirection().x;
        if (x > 0)
            flip = true;
        else if (x < 0)
            flip = false;
	}
}
