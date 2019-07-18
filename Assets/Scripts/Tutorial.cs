using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class Tutorial : MonoBehaviour
{
	
	public static bool isTutorial;
	public int ObjectiveID;
	public GameObject ObjectParent;
	public List<GameObject> ObjectiveGOTemp;
	public bool CompletedTask;
	public CharacterStats CS;
	public GameObject PlayerHUD;
	public GameObject WeaponHUD;
	public GameObject StaminaHUD;
	public GameObject DpadHUD;
	public GameObject Score;
	public GameObject[] DoorText;
	public Transform spawnLocation;
	public Transform MarkerspawnLocation;
	public Transform BossspawnLocation;
	public bool spawn;
	public Transform Pistol;
	public Transform HealthPack;
	public Transform Needle;
	public static bool allowSwitch = false;
	public static bool allowFineAim = false;
	public static bool allowMove = false;
	public static bool allowAim = false;
	public static bool AllowCombo = false;
	public static bool AllowComboCountdown = false;
	public static bool AllowBuffs = false;
	public static bool AllowNeedle = false;
	public static bool AllowFury = false;
	public static bool AllowShowMap = false;
	public static bool AllowBuffTimer = false;
	public static bool AllowReload = false;
	public static bool AllowAttack = false;
	public static bool AllowCharacterChoose = false;

	public Transform Marker;
	public Transform[] EnemyTemp;
	public List<GameObject> EnemiesSpawned;

	public GameObject FuryBarGO;
	public Slider FuryBar;
	public GameObject[] HUDTutorials;

	public GameObject[] SteroidTutorial;

	public AmmoScript AS;

	public Transform BossGO;
    public GameObject SecondDoor;
    public GameObject AButton;

	

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt ("Tutorial",0);
		isTutorial = true;
		CompletedTaskDelay ();
		Tutorial.allowSwitch = false;
		Tutorial.allowFineAim = false;
		CharacterStats.allowMovement = false;
//		Music.whichMusic = 2;
//		Music.playMusic = true;
		foreach (Transform child in ObjectParent.transform){
			ObjectiveGOTemp.Add(child.gameObject);

		}
		spawn = false;

		allowSwitch = false;
		allowFineAim = false;
		allowMove = false;
		allowAim = false;
		AllowCombo = false;
		AllowComboCountdown = false;
		AllowBuffs = false;
		AllowNeedle = false;
		AllowFury = false;
		AllowShowMap = false;
		AllowBuffTimer = false;
		AllowReload = false;
		AllowAttack = false;
		AllowCharacterChoose = false;
	}
	
	// Update is called once per frame
	void Update ()
	{	
//		Debug.Log ("CharacterStats.allowMovement " + CharacterStats.allowMovement);
		for (int i = 0; i < ObjectiveGOTemp.Count; i++) {
			
			if(i != ObjectiveID){
				ObjectiveGOTemp[i].SetActive(false);
			}
			
			
		}

		if (CompletedTask) {
			CompletedTask = false;
		}

	}

	public void CompletedTaskDelay(){
		foreach (GameObject go in HUDTutorials) {
			go.SetActive(false);
		}

		spawn = false;
		ObjectiveID ++;
		Invoke("enableMovement",2);
	}

	void enableMovement(){
		CharacterStats.allowMovement = false;
		ObjectiveGOTemp[ObjectiveID].SetActive(true);
		Invoke("FullControl",2);
	}

	void FullControl(){
		CharacterStats.allowMovement = true;
	}

}
