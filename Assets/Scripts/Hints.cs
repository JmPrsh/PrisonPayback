using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Hints : MonoBehaviour {

	public string[] hints;
    Text t;
    float timer;

	// Use this for initialization
	void Start () {
        t = GetComponent<Text>();
        ChangeHint();
	}
	
    public void ChangeHintbutton(){
        timer = 0;
        ChangeHint();
    }

	// Update is called once per frame
	void Update () {
	
        if(timer > 4){
            ChangeHint();
            timer = 0;
        }else{
            timer += Time.deltaTime;
        }

	}

	void ChangeHint(){
		t.text = hints [Random.Range (0,hints.Length)].ToString();
		
	}
}
