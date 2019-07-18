using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour
{

	public GameObject[] Levels;
	public int progress;
	public int CharacterProgress;
	int i;
	public int j;
	Animator anim;
	int ChosenLevel;
	GameObject[] LevelButtons;
	public string[] PlayerPrefLevelName; // the selected name is the name that will be loaded from the loading screen
	public Color selectedColor;
	public Color UnselectedColor;
	public Animator CanvasAnim;
	public int CanvasID;
	public GameObject[] CanvasGO;
	public Text Boss1Name;
	public Text Boss2Name;
	public Text Boss3Name;
	public Text Boss4Name;
	public Text Boss5Name;
	public GameObject ESGO;

	void Start ()
	{
//		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("Progress", 15);
//		if (!PlayerPrefs.HasKey ("Progress")) {
////			alreadyAnimated = 0;
//			PlayerPrefs.SetInt ("Progress", 1);
//		}
		progress = PlayerPrefs.GetInt ("Progress");
		if (progress >= 10) {
			Boss1Name.text = "Boss: Snitch";
		} else {
			Boss1Name.text = "Boss: ******";
		}
		if (progress >= 20) {
			Boss2Name.text = "Boss: Chef";
		} else {
			Boss2Name.text = "Boss: ******";
		}
		if (progress >= 30) {
			Boss3Name.text = "Boss: Brute";
		} else {
			Boss3Name.text = "Boss: ******";
		}
		if (progress >= 40) {
			Boss4Name.text = "Boss: Pysco";
		} else {
			Boss4Name.text = "Boss: ******";
		}
		if (progress >= 50) {
			Boss5Name.text = "Boss: Warden";
		} else {
			Boss5Name.text = "Boss: ******";
		}

		while ((i+1) <= progress) {
			if (Levels [i].GetComponent<buttonAnimatedScript> ().alreadyAnimated >= 1) {

				Levels [i].GetComponent<Animator> ().SetTrigger ("AlreadyUnlocked");
			}
			if (Levels [i].GetComponent<buttonAnimatedScript> ().alreadyAnimated == 0) {
//				Debug.Log ("bars animating");

				Levels [i].GetComponent<Animator> ().SetTrigger ("OpenBars");
			}
			Levels [i].GetComponent<buttonAnimatedScript> ().alreadyAnimated += 1;
			PlayerPrefs.SetInt (Levels [i].GetComponent<buttonAnimatedScript> ().playerprefsavename, Levels [i].GetComponent<buttonAnimatedScript> ().alreadyAnimated);
			i++;
		}

		CanvasID = 0;

//		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");

	}

	void Update ()
	{
		EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		CanvasController ();
		LevelButtons = Levels;


		if (EventSystem.current.currentSelectedGameObject == LevelButtons [0]) {
			LevelButtons [0].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[0].transform.localScale = new Vector2(1.2f,1.2f);
		} else {
			LevelButtons [0].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[0].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [1]) {
			LevelButtons [1].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[1].transform.localScale = new Vector2(1.2f,1.2f);
		} else {
			LevelButtons [1].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[1].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [2]) {
			LevelButtons [2].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[2].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [2].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[2].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [3]) {
			LevelButtons [3].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[3].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [3].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[3].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [4]) {
			LevelButtons [4].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[4].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [4].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[4].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [5]) {
			LevelButtons [5].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[5].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [5].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[5].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [6]) {
			LevelButtons [6].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[6].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [6].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[6].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [7]) {
			LevelButtons [7].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[7].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [7].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[7].transform.localScale = new Vector2(1f,1f);
		}
		if (EventSystem.current.currentSelectedGameObject == LevelButtons [8]) {
			LevelButtons [8].GetComponentInChildren<Text> ().color = selectedColor;
			LevelButtons[8].transform.localScale = new Vector2(1.2f,1.2f);
			
		} else {
			LevelButtons [8].GetComponentInChildren<Text> ().color = UnselectedColor;
			LevelButtons[8].transform.localScale = new Vector2(1f,1f);
		}

		if (CheckingCanvas) {
			if(CanvasID == 0){
				eventSystem.SetSelectedGameObject(LevelButtons[0].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 1){
				eventSystem.SetSelectedGameObject(LevelButtons[9].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 2){
				eventSystem.SetSelectedGameObject(LevelButtons[10].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 3){
				eventSystem.SetSelectedGameObject(LevelButtons[19].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 4){
				eventSystem.SetSelectedGameObject(LevelButtons[20].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 5){
				eventSystem.SetSelectedGameObject(LevelButtons[29].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 6){
				eventSystem.SetSelectedGameObject(LevelButtons[30].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 7){
				eventSystem.SetSelectedGameObject(LevelButtons[39].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 8){
				eventSystem.SetSelectedGameObject(LevelButtons[40].gameObject, new BaseEventData(eventSystem));
			}
			if(CanvasID == 9){
				eventSystem.SetSelectedGameObject(LevelButtons[49].gameObject, new BaseEventData(eventSystem));
			}


			CheckingCanvas = false;
		}

	}
	bool CheckingCanvas;

	void CanvasController ()
	{

		if (Input.GetButtonUp ("LB")) {
			CheckingCanvas = true;
			if (CanvasID > 0) {

				CanvasID -= 1;
//				CanvasAnim.speed =-1;

			}
		}
		if (Input.GetButtonUp ("RB")) {
			CheckingCanvas = true;
			if (CanvasID < CanvasGO.Length-1) { // change canvas limit amount in the array from the inspector
				CanvasID += 1;
//				CanvasAnim.speed =1;
			}
		}

		switch (CanvasID) {
		case 0:
			CanvasAnim.SetBool("gotoArea1Boss",false);
			break;
		case 1:
			CanvasAnim.SetBool("gotoArea1Boss",true);
			CanvasAnim.SetBool("gotoArea2",false);
			break;
		case 2:
			CanvasAnim.SetBool("gotoArea2",true);
			CanvasAnim.SetBool("gotoArea2Boss",false);
			break;
		case 3:
			CanvasAnim.SetBool("gotoArea2Boss",true);
			CanvasAnim.SetBool("gotoArea3",false);
			break;
		case 4:
			CanvasAnim.SetBool("gotoArea3",true);
			CanvasAnim.SetBool("gotoArea3Boss",false);
			break;
		case 5:
			CanvasAnim.SetBool("gotoArea3Boss",true);
			CanvasAnim.SetBool("gotoArea4",false);
			break;
		case 6:
			CanvasAnim.SetBool("gotoArea4",true);
			CanvasAnim.SetBool("gotoArea4Boss",false);
			break;
		case 7:
			CanvasAnim.SetBool("gotoArea4Boss",true);
			CanvasAnim.SetBool("gotoArea5",false);
			break;
		case 8:
			CanvasAnim.SetBool("gotoArea5",true);
			CanvasAnim.SetBool("gotoArea5Boss",false);
			break;
		case 9:
			CanvasAnim.SetBool("gotoArea5Boss",true);

			break;
		

		}
//		Debug.Log ("CanvasID " + CanvasID);


	}

	public void LoadScene ()
	{
		Application.LoadLevel ("Menu");
	}



	// Area1
	public void LevelA1 ()
	{
		foreach (GameObject c in LevelButtons) {
			if (EventSystem.current.currentSelectedGameObject == c) {
				Application.LoadLevel ("Inmates");
				PlayerPrefs.SetString ("LevelToLoad", PlayerPrefLevelName[0]);
			}
		}
	}



}
