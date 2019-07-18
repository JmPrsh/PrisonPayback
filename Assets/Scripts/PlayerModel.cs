// State valueCurrentis calculated by the average score of all variables .....
// Variables values are determined at each checkpoint .....
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class PlayerModel : MonoBehaviour
{
	// PLAYER MODEL
    public static PlayerModel PM;
	public float Health;
	public int bulletTotal;
	public int bulletsUsed;
	public int Lives;
	public static int LivesLost;
	public int LivesLosttoshow;
	public float timeTotal;
	public float timePlayed;
	// END PLAYER MODEL
	
	public int EnemyDamageAdd;
	public int EnemyAdd;
	public int EnemySpawnAmount;
	public float StateValue;
	public float BossSpawnChance;
	public bool SpawnBoss;
	public enum State
	{
		zero,
		one,
		two,
		three,
		four,
		five}
	;
	public State PlayerState = State.three;
	CharacterStats Character;
	public bool CheckState;
	public int ChangesID1;
//	EnemyManager enemyGroupscript;
	public bool checkPlayer;
	public int PlayerCigs;
	public string StateName;
	public GameObject[] EnemieGOs;
	public List<GameObject> AllEnemies;
	public List<GameObject> AllEnemiesStillAlive;
	public bool checkingEnemiesInList;
	public int EnemyMax;
	public int EnemyAlive;

	public static bool CheckingWhosLeft;

	// Variable Results
	public int HealthLeft;

	public int BulletsLeft;
	public int LivesLeft;
	public float TimeLeft;
	// End Variable Results

	float TotalTimePlayed;

//	public float EnemyKillTimer

	// Playermodel StateCheck Timer
	public float StateCheckTimer;
	public float EnemyTotal;
	public float healthpercentage;
	// Use this for initialization
	void Start ()
	{
        PM = this;
//		PlayerPrefs.SetFloat ("State", 56);	
        if (PlayerPrefs.HasKey("State"))
        {
            StateValue = PlayerPrefs.GetFloat("State");
        }else{
            StateValue = 50;
        }
		PlayerCigs = PlayerPrefs.GetInt ("Cigs");
		checkingEnemiesInList = false;
		LivesLost = PlayerPrefs.GetInt ("LivesLost");
//		Invoke ("CheckEnemies", 2);
//		Invoke ("CheckEnemiesStillAlive", 2);
	}
	
	public void FindPlayer ()
	{
		Character = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterStats> ();
        if (PlayerPrefs.HasKey("State"))
        {
            StateValue= PlayerPrefs.GetFloat("State");
        }else{
            StateValue = 50;
        }
		EnemySpawnAmount = PlayerPrefs.GetInt ("SpawnAmount");
		EnemyAdd = PlayerPrefs.GetInt ("EnemyAdd");
		StaticVariables.EnemySpeed = PlayerPrefs.GetFloat ("EnemyMoveSpeed");
	}
	
	public void CheckEnemies ()
	{
		checkingEnemiesInList = true;

	}

	void CheckEnemiesStillAlive ()
	{
		CheckingWhosLeft = true;
		
	}


	
	// Update is called once per frame
	void Update ()
	{
		LivesLosttoshow = LivesLost;
		EnemieGOs = GameObject.FindGameObjectsWithTag ("Enemy");

		if (checkingEnemiesInList) {
			
//			Debug.Log ("Adding Enemies to list");
			
			foreach (GameObject go in EnemieGOs) {
				AllEnemies.Add (go);
			}
			
			foreach (GameObject go in AllEnemies) {
				AllEnemiesStillAlive.Add (go);
			}
			
			checkingEnemiesInList = false;

			Invoke("CheckEnemiesStillAlive",1);
			
		}
		if (CheckingWhosLeft) {

//			Debug.Log ("Checking Enemies who are still alive");

			
			
			foreach (GameObject go in AllEnemies) {
				if(go.tag == "Enemy"){
					if(go.GetComponent<attackPlayer>().health <=0){
						AllEnemiesStillAlive.Remove (go);
					}
				}
				
//				if(!go.activeInHierarchy){
//					AllEnemiesStillAlive.Remove (go);
//				}
			}


			if (AllEnemiesStillAlive.Count == 0) {
				CheckState = true;
			} 

		}

		EnemyMax = AllEnemies.Count;
		EnemyAlive = AllEnemiesStillAlive.Count;
		EnemyTotal = ((float)EnemyMax - (float)EnemyAlive) / (float)EnemyMax * 100;
//		Debug.Log ("Enemies that have been killed = " + EnemyTotal + "%");


//		Application.targetFrameRate = 300;
        healthpercentage = Character.Health / Character.HealthStarting * 100;

//		Debug.Log ("Health = " + healthpercentage + "%");

		if (EnemySpawnAmount > 20) {
			EnemySpawnAmount = 20;
		}
		if (EnemySpawnAmount <= 2) {
			EnemySpawnAmount = 2;
		}

		if (Character != null) {
			checkPlayer = true;
		} else {
			checkPlayer = false;
		}
		if (checkPlayer) {
			if (!Character.Dead) {

				// StateCheckTimer
//				StateCheckTimer += 1 * Time.deltaTime; ////////////////// auto timer state check
//				if (StateCheckTimer > 10) {
//					CheckState = true;
//					Debug.Log ("Checked State");
//					StateCheckTimer = 0;
//				}
				 
				TotalTimePlayed +=1 * Time.deltaTime;
				timePlayed += 1 * Time.deltaTime;

				HealthLeft = (int)healthpercentage;
				BulletsLeft = bulletTotal - bulletsUsed;
				LivesLeft = Lives - LivesLost;
				TimeLeft = timeTotal - timePlayed;
				Stats.TimePlayed = (int)TotalTimePlayed;

				ChangesID1 = Random.Range (0, 1);

				if (CheckState) {
                    StateValue = PlayerPrefs.GetFloat ("State");
					CheckingState ();
					CheckState = false;
					EnemySpawnAmount += EnemyAdd;


				}

            }else{
                
            }
		}
	}

	IEnumerator SetPrefs ()
	{
		PlayerPrefs.SetFloat ("State", StateValue);		
		PlayerPrefs.SetInt ("EnemyAdd", EnemyAdd);
		PlayerPrefs.SetFloat ("EnemyMoveSpeed", StaticVariables.EnemySpeed);
		yield return null;
	}
	
	public void CheckingState ()
	{


		CheckingWhosLeft = false;


        // start timer, if all enemies are dead then check to see how much time they have used and then take it away from the overall timer and make that valueCurrentbe the percentage in the calculation
        StateValue = (healthpercentage + (int)TimeLeft) / 2;
//		StatevalueCurrent= (HealthLeft + EnemyTotal + LivesLeft + (int)TimeLeft) / 4;
		AllEnemiesStillAlive.Clear ();
		AllEnemies.Clear ();
		if (StateValue >= 90) {
			PlayerState = State.one;
		}
		if (StateValue < 90 && StateValue >= 60) {
			PlayerState = State.two;
		}
		if (StateValue < 60 && StateValue >= 40) {
			PlayerState = State.three;
		}
		if (StateValue < 40 && StateValue >= 20) {
			PlayerState = State.four;
		}
		if (StateValue < 20) {
			PlayerState = State.five;
		}

		PlayerPrefs.SetFloat ("State", StateValue);
		PlayerPrefs.SetInt ("SpawnAmount", EnemySpawnAmount);
		//EnemySpawnAmount += EnemyAdd;
		//Character.HealthLoss += EnemyDamageAdd;

		if (Character != null) {
			 
			
			if (PlayerState == State.one) {
				// Make it really hard
				EnemyAdd = 3;	
				ItemSpawner.ItemAmount -= 5;
				StaticVariables.EnemySpeed = 1.3f;
				BossSpawnChance = Random.Range(0,1);
				StateName = "One (Hardest State)";
				StartCoroutine (SetPrefs ());	
			} else if (PlayerState == State.two) {
				// Make it harder
				EnemyAdd = 1;
				ItemSpawner.ItemAmount -= 2;
				StaticVariables.EnemySpeed = 1.1f;
				BossSpawnChance = Random.Range(0,3);
				StateName = "Two (Hard State)";
				StartCoroutine (SetPrefs ());	
			
			} else if (PlayerState == State.three) {
				// Change nothing ( In flow ) OR first time playing
				EnemyAdd = 0;
				StateName = "Three (Flow State)";
			} else if (PlayerState == State.four) {
				// Make it easier
				
				ItemSpawner.ItemAmount += 2;
				StaticVariables.EnemySpeed = 0.9f;
				StateName = "Four (Easy State)";
				StartCoroutine (SetPrefs ());
			} else if (PlayerState == State.five) {
				// Make it really easy AND dont spawn boss
				ItemSpawner.ItemAmount += 5;
				StaticVariables.EnemySpeed = 0.7f;
				StateName = "Five (Easiest State)";
				StartCoroutine (SetPrefs ());
			}

		}
		
			if(BossSpawnChance == 0){
                WaveManager.SpawnBrute = true;
			}
		

		timePlayed = 0f;
		bulletsUsed = 0;


	}

        void OnGUI() {
    //        hSlidervalueCurrent= GUI.HorizontalSlider(new Rect(25, 100, 200, 25), hSliderValue, 1.0F, 10.0F);
//        GUI.Label(new Rect(25, Screen.height/2, 200, 25),"State valueCurrent= " +StateValue);
        }
		
}
