using UnityEngine;
using System.Collections;

public class destroyafterseconds : MonoBehaviour {

    public float removetime = 1;

	// Use this for initialization
	void Start () {
        Invoke("waittilldie", removetime);
	}
	
	void waittilldie(){
	
		this.Recycle ();

	}
}
