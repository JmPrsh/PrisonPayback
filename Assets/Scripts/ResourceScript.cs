using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceScript : MonoBehaviour
{

		public List<GameObject> AmmoBoxes;
		public List<GameObject> HealthPack;
		public bool loseall;
		public bool losehalf;
		public bool regainhalf;
		public bool regainall;
		// Use this for initialization
		void Start ()
		{


				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ammo")) {

						AmmoBoxes.Add (obj);
						obj.transform.parent = this.gameObject.transform;
			
				}
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("HealthPack")) {

						HealthPack.Add (obj);
						obj.transform.parent = this.gameObject.transform;
				}
		}

		void Update ()
		{
	
				if (loseall) {
	
						if (AmmoBoxes != null) {
								try {
										AmmoBoxes [0].SetActive (false);
										AmmoBoxes [1].SetActive (false);
										AmmoBoxes [2].SetActive (false);
										AmmoBoxes [3].SetActive (false);
										HealthPack [0].SetActive (false);
										HealthPack [1].SetActive (false);
										HealthPack [2].SetActive (false);
										HealthPack [3].SetActive (false);
								} catch {
								}
						}
						loseall = false;
				} else if (losehalf) {
	
						if (AmmoBoxes != null) {
								try {
										AmmoBoxes [Random.Range (0, 3)].SetActive (false);
										AmmoBoxes [Random.Range (0, 3)].SetActive (false);
										HealthPack [Random.Range (0, 3)].SetActive (false);
										HealthPack [Random.Range (0, 3)].SetActive (false);
								} catch {
								}
						}
						losehalf = false;
				} else if (regainhalf) {
	
						if (AmmoBoxes != null) {
								try {
										AmmoBoxes [Random.Range (0, 3)].SetActive (true);
										AmmoBoxes [Random.Range (0, 3)].SetActive (true);
										HealthPack [Random.Range (0, 3)].SetActive (true);
										HealthPack [Random.Range (0, 3)].SetActive (true);
								} catch {
								}
						}
						regainhalf = false;
				} else if (regainall) {
	

						if (AmmoBoxes != null) {
								try {
										AmmoBoxes [0].SetActive (true);
										AmmoBoxes [1].SetActive (true);
										AmmoBoxes [2].SetActive (true);
										AmmoBoxes [3].SetActive (true);
										HealthPack [0].SetActive (true);
										HealthPack [1].SetActive (true);
										HealthPack [2].SetActive (true);
										HealthPack [3].SetActive (true);
								} catch {
								}
						}
						regainall = false;
	
				}
		}
}
