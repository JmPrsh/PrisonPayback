using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMultiplier : MonoBehaviour {
    Text t;
    public bool MenuText;
    // Start is called before the first frame update
    void Awake () {
        t = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update () {
        if (MenuText)
            t.text = "x3 Multiplier Activated";
            
        t.enabled = BonusManager.multiplier > 1;
    }
}