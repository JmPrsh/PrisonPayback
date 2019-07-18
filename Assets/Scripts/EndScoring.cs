using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EndScoring : MonoBehaviour {


	public Text Scoreboard;
	public Text CigsCounter;
	bool transfer;
	public GameObject Arrow;
	public GameObject AButton;
	bool allowContinue;


	int Cigs;

	int CalculatedScore;
	int CalculatedCigs;
	int TutorialCompleted;
	public GameObject TutorialScoringBox;

	// Use this for initialization
	void Start () {
		Cigs = PlayerPrefs.GetInt ("CigsAmount");
		CalculatedScore = CharacterStats.Score / 100;
		Invoke ("TransferMoney", 4);
		CalculatedCigs = (Cigs + CalculatedScore);
		TutorialCompleted = PlayerPrefs.GetInt ("Tutorial");
		if (TutorialCompleted == 0) {
			Invoke("TutorialBox",5);
		}
	}

	void TutorialBox(){
		TutorialScoringBox.SetActive (true);
		Invoke ("AllowContinue", 2);
	}
	
	// Update is called once per frame
	void Update () {

		Scoreboard.text = CharacterStats.Score.ToString ("000000");
		CigsCounter.text = Cigs.ToString ("00");

		if (transfer) {
			StartCoroutine(AdjustScore());
		}

		if (allowContinue) {

			if(Input.GetButtonUp("Submit")){
				if(TutorialCompleted == 1){
					Debug.Log ("Load Next Level");
					PlayerPrefs.SetString ("LevelToLoad", CharacterStats.ScoringSceneLevelToLoad);
					GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
					AButton.SetActive (false);
					allowContinue = false;
					Invoke ("LoadOut", 1);
				}else{
					Debug.Log ("Load Next Level");
					PlayerPrefs.SetString ("LevelToLoad", "Inmates");
					GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
					AButton.SetActive (false);
					allowContinue = false;
					Invoke ("LoadOut", 1);
				}
			}

		}

	}

	void LoadOut(){
		Application.LoadLevel("LoadingScreen");
	}

	void TransferMoney(){
		transfer = true;
		if (TutorialCompleted == 1) {
			Arrow.SetActive (true);
		}
	}

	void AllowContinue(){

			AButton.SetActive (true);
			allowContinue = true;

	}

	IEnumerator AdjustScore ()
	{
		while (CharacterStats.Score != 0) {
			if (CharacterStats.Score > 0) {
				CharacterStats.Score -= 5;
				if (Cigs < CalculatedCigs) {
					Cigs += 1;
				}

				//				MoneyTick.clip = TickUp;
				//				MoneyTick.GetComponent<AudioSource>().Play ();
			} else{
				CharacterStats.Score = 0;
			}
			yield return null;

		}
		Arrow.SetActive(false);
		PlayerPrefs.SetInt ("CigsAmount", Cigs);
		if (TutorialCompleted == 1) {
			Invoke ("AllowContinue", 1);
		}
	}


}
