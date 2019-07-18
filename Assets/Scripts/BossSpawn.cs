using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSpawn : MonoBehaviour
{

		
		public GameObject Boss;
		public bool increase;
		public int spawnAmount;
		public int numSelectors = 5;
		public GameObject[] selectorArr;
		public GameObject selector; //selected in the editor
		public bool spawn;
		public int bossInt;
		

		void Awake ()
		{
//				spawn = true;
				spawnAmount = 1;
				bossInt = 0;

		}

		void Start ()
		{
				bossInt = 0;
		}

		void Update ()
		{

//				switch (bossInt) {
//				case 0:
//						Boss = GameObject.FindGameObjectWithTag ("Bossplace1");
//						break;
//				case 1:
//						Boss = GameObject.FindGameObjectWithTag ("Bossplace2");
//						break;
//				case 2:
//						Boss = GameObject.FindGameObjectWithTag ("Bossplace3");
//						break;
//				case 3:
//						Boss = GameObject.FindGameObjectWithTag ("Bossplace4");
//						break;
//		
//		
//				}

				numSelectors = spawnAmount;
				
				if (spawn) {
						selectorArr = new GameObject[spawnAmount];
						for (int i = 0; i < spawnAmount; i++) {
						
								GameObject go = Instantiate (selector, new Vector2 (Boss.transform.position.x, Boss.transform.position.y), Quaternion.identity) as GameObject;
								selectorArr [i] = go;
								
						}
						spawn = false;
				}

		}
}
