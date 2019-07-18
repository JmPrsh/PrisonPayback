﻿using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    public static ItemSpawner i;
	public Transform[] Items;
	public static int ItemAmount;
	public static Transform[] ItemsStatic;

    void Awake(){
        i = this;
        ItemsStatic = Items;
        ItemAmount = PlayerPrefs.GetInt("ItemAmount");
    }

	// Use this for initialization
	public void Spawn () {
		for (int i = 0; i < ItemAmount; i++) {
			Items[Random.Range(0,Items.Length)].Spawn (transform.position + (Vector3)Random.insideUnitCircle * 5, Quaternion.identity);
		}
        PlayerPrefs.SetInt("ItemAmount",ItemAmount);
	}
	
	// Update is called once per frame
	void Update () {

		if (ItemAmount < 0) {ItemAmount = 0;}
		if (ItemAmount > 5) {ItemAmount = 5;}
	
	}
}