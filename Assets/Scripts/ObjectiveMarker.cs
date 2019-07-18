using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{

	Transform Player;
	public Tutorial TutorialGO;
	public enum Task
	{

		Move,
		Aim,
		Attack,
		UseBuff

	}
	public Task taskset;
	public Text ObjectiveText;
	bool startmoveTask;
	bool movedleft;
	bool movedright;
	bool moveddown;
	bool movedup;
	int moveTaskID;
	bool startaimTask;
	bool aimleft;
	bool aimright;
	bool aimdown;
	bool aimup;
	int aimTaskID;
	bool AttackTask;
	bool Attacked;
	public Transform Enemy;
	Transform EnemyTemp;
	public static List<Transform> EnemiesAlive = new List<Transform>(1);
	bool spawnEnemy;
	bool useBuffTask;
	bool usedBuff;
	bool selectedBuff;
	bool spawnBuffs;
	public static bool PickedUpItem;
	public Transform SpawnLocation;


	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find ("Player").transform;

		DPadButtons.isTutorial = true;
		CharacterStats.allowMovement = true;

	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Vector3.Distance (Player.transform.position, transform.position) < 1) {

			if (taskset == Task.Move) {
				CharacterStats.allowMovement = false;
				startmoveTask = true;
//				TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [0];
			}
			if (taskset == Task.Aim) {
				CharacterStats.allowMovement = false;
				startaimTask = true;
//				TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [1];
			}
			if (taskset == Task.Attack) {
				CharacterStats.allowMovement = false;
				AttackTask = true;
//				TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [2];
			}
			if (taskset == Task.UseBuff) {
				useBuffTask = true;
				DPadButtons.isTutorial = false;
//				TutorialGO.ObjectiveHUDGO.sprite = null;
			}


		} 

		if (startmoveTask) {

			switch (moveTaskID) {
			case 0:
				if (!movedleft) {
					ObjectiveText.enabled = true;
					ObjectiveText.text = "Move Left";
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("LeftRight") == -1) {
						movedleft = true;
					}
				} else {
					CharacterStats.allowMovement = false;
					Invoke ("AllowToMove", 1);
					moveTaskID = 1;
				}
				break;
			case 1:
				if (!movedright) {
					ObjectiveText.text = "Move Right";
					
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("LeftRight") == 1) {
						movedright = true;
					}
					
				} else {
					CharacterStats.allowMovement = false;
					Invoke ("AllowToMove", 1);
					moveTaskID = 2;
					
				}
				break;
			case 2:
				if (!moveddown) {
					ObjectiveText.text = "Move Down";
					
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("UpDown") == 1) {
						moveddown = true;
					}
					
				} else {
					CharacterStats.allowMovement = false;
					moveTaskID = 3;
				}
				break;
			case 3:
				if (!movedup) {
					ObjectiveText.text = "Move Up";
					
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("UpDown") == -1) {
						movedup = true;
					}
					
				} else {
					CharacterStats.allowMovement = false;
					ObjectiveText.text = "Press A to Continue";
//					TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [5];
					if (Input.GetButtonUp ("Submit")) {
						CharacterStats.allowMovement = true;
//						TutorialGO.ObjectiveHUDGO.sprite = null;
//						TutorialGO.objectives.RemoveAt (0);
						ObjectiveText.enabled = false;
						this.Recycle ();
						Debug.Log ("Objective Finished and removed Objective");
					}

				}
				break;
				
				
			}
		}

		// aim task
		if (startaimTask) {
			switch (aimTaskID) {
			case 0:
				if (!aimleft) {
					ObjectiveText.enabled = true;
					ObjectiveText.text = "Aim Left";
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("Xbox360ControllerRightX") == -1) {
						aimleft = true;
					}
				} else {
					CharacterStats.allowMovement = false;
					Invoke ("AllowToMove", 1);
					aimTaskID = 1;
				}
				break;
			case 1:
				if (!aimright) {
					ObjectiveText.text = "Aim Right";
					
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("Xbox360ControllerRightX") == 1) {
						aimright = true;
					}
					
				} else {
					CharacterStats.allowMovement = false;
					Invoke ("AllowToMove", 1);
					aimTaskID = 2;
					
				}
				break;
			case 2:
				if (!aimdown) {
					ObjectiveText.text = "Aim Down";
					
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("Xbox360ControllerRightY") == 1) {
						aimdown = true;
					}
					
				} else {
					CharacterStats.allowMovement = false;
					aimTaskID = 3;
				}
				break;
			case 3:
				if (!aimup) {
					ObjectiveText.text = "Aim Up";
					
					Invoke ("AllowToMove", 1);
					if (Input.GetAxis ("Xbox360ControllerRightY") == -1) {
						aimup = true;
					}
					
				} else {
					CharacterStats.allowMovement = false;
					ObjectiveText.text = "Press A to Continue";
//					TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [5];
					if (Input.GetButtonUp ("Submit")) {
						CharacterStats.allowMovement = true;
//						TutorialGO.ObjectiveHUDGO.sprite = null;
//						TutorialGO.objectives.RemoveAt (0);
						ObjectiveText.enabled = false;
						this.Recycle ();
						Debug.Log ("Objective Finished and removed Objective");
					}
					
				}
				break;
				
				
			}
		}

		if (AttackTask) {
			if (!Attacked) {
				ObjectiveText.enabled = true;

				if (!spawnEnemy) {
					ObjectiveText.text = "Press RT to Attack";
					Transform temp = Enemy.Spawn (SpawnLocation.transform.position, Quaternion.identity);
					temp.GetComponent<attackPlayer> ().enemyclass = attackPlayer.EnemyClass.Warden;
					temp.GetComponent<attackPlayer> ().WeaponToSpawn = null;
					temp.position = SpawnLocation.transform.position;
					EnemyTemp = temp;
					EnemiesAlive.Add (temp);
					spawnEnemy = true;
				}
				Invoke("CheckEnemy",1);

				if (Input.GetAxis ("RT") == 1) {
					CharacterStats.allowMovement = true;
					ObjectiveText.text = "Attack and Kill guard";

				}

			} else {
				CharacterStats.allowMovement = false;
				ObjectiveText.text = "Killing an Enemy adds to your score\nPress A to Continue";
//				TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [5];
				if (Input.GetButtonUp ("Submit")) {
//					TutorialGO.ObjectiveHUDGO.sprite = null;
					CharacterStats.allowMovement = true;
//					TutorialGO.objectives.RemoveAt (0);
					ObjectiveText.enabled = false;
					this.Recycle ();
					Debug.Log ("Objective Finished and removed Objective");
				}
			}

		}


		if (useBuffTask) {
			if (!usedBuff) {
				if (!spawnBuffs) {
					for (int i = 0; i < 4; i++) {
						ItemSpawner.ItemsStatic [Random.Range (0, ItemSpawner.ItemsStatic.Length)].Spawn (SpawnLocation.transform.position + (Vector3)Random.insideUnitCircle * 2f, Quaternion.identity);
					}
					spawnBuffs = true;
				}
				if (!PickedUpItem) {
					ObjectiveText.enabled = true;
					ObjectiveText.text = "Pick up any Item";

				} else {
					CharacterStats.allowMovement = false;
//					TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [3];
					if (!selectedBuff) {
						ObjectiveText.text = "Press Dpad to Select Item";
						if (Input.GetAxis ("Xbox360ControllerDPadX") > 0f) {
							selectedBuff = true;
						} else if (Input.GetAxis ("Xbox360ControllerDPadX") < 0f) {
							selectedBuff = true;
						} else if (Input.GetAxis ("Xbox360ControllerDPadY") > 0f) {
							selectedBuff = true;
						} else if (Input.GetAxis ("Xbox360ControllerDPadY") < 0f) {
							selectedBuff = true;
						
						}

					} else {
						ObjectiveText.text = "Press Y to consume Item";
//						TutorialGO.ObjectiveHUDGO.sprite = TutorialGO.ObjectiveHUD [4];
						if (Input.GetButtonUp ("Y")) {
							ObjectiveText.text = "You are now ready... exit through any door to leave this tutorial";
							usedBuff = true;
							CharacterStats.allowMovement = true;
//							TutorialGO.ObjectiveHUDGO.sprite = null;
//							TutorialGO.objectives.RemoveAt (0);
							this.Recycle ();
						}
					}
				}
			} else {

			}


		}

	}

	public void CheckEnemy(){
		if(EnemyTemp.GetComponent<attackPlayer> ().health <=0){
			Attacked = true;
		}
	}

	public void AllowToMove ()
	{
		CharacterStats.allowMovement = true;

	}



}
