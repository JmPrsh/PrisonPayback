using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPurchasesManager : MonoBehaviour {

    public static WeaponPurchasesManager wpm;
    public float knifeCost;
    public float pipeCost;
    public float pistolCost;
    public float machinegunCost;
    public float shotgunCost;
    public float sniperCost;
    public float minigunCost;

    public Sprite[] weapons;

	// Use this for initialization
	void Awake () {
        wpm = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
