using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelMap : MonoBehaviour
{
	
	public Animator animCharacter;
	public bool isatTutorial;
	public bool isatLevel1;
	public bool isatLevel2;
	public bool isatLevel3;
	public bool isatLevel4;
	public bool isatLevel5;
	public int TutorialCompleted;
	public string[] PlayerPrefLevelName;
	public List<GameObject> LevelButtons;
	public int LevelID;
	public int i;
	public int AreaProgress;
	EventSystem eventSystem;
	public int[] LevelsSelected;
	bool Level1Selected;
	bool Level2Selected;
	bool Level3Selected;
	bool Level4Selected;
	bool Level5Selected;
	public GameObject Character;
	public GameObject CharacterParent;
	public int CharacterSpriteID;
	public Sprite[] CharacterSprites;
	public Transform Target;

	void Start ()
	{
		Music.whichMusic = 1;
		Music.playMusic = true;
//		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");
		CharacterSpriteID = PlayerPrefs.GetInt ("CharacterID");
		AreaProgress = PlayerPrefs.GetInt ("AreaProgress");
		TutorialCompleted = PlayerPrefs.GetInt ("Tutorial");
		Character.GetComponent<Image> ().sprite = CharacterSprites [CharacterSpriteID];

//		PlayerPrefs.SetInt ("Tutorial", 0);
//		PlayerPrefs.SetInt ("AreaProgress", 0);

		if (!PlayerPrefs.HasKey ("AreaProgress") && TutorialCompleted == 0) {
			PlayerPrefs.SetInt ("AreaProgress", 0);
			isatTutorial = true;
			isatLevel1 = false;
			isatLevel2 = false;
			isatLevel3 = false;
			isatLevel4 = false;
		} else if(!PlayerPrefs.HasKey ("AreaProgress") && TutorialCompleted == 1) {
			PlayerPrefs.SetInt ("AreaProgress", 1);
		}

		AreaProgress = PlayerPrefs.GetInt ("AreaProgress");
		TutorialCompleted = PlayerPrefs.GetInt ("Tutorial");

		switch (AreaProgress) {
		case 0:
			isatTutorial = true;
			break;
		case 1:
			isatLevel1 = true;
			break;
		case 2:
			isatLevel2 = true;
			break;
		case 3:
			isatLevel3 = true;
			break;
		case 4:
			isatLevel4 = true;
			break;
		case 5:
			isatLevel5 = true;
			break;

		}

		if (AreaProgress <= 1) {
			LevelsSelected [0] = 1;
		} 
		if (isatTutorial) {
			Target = LevelButtons [0].transform;
			CharacterParent.transform.position = new Vector3 (LevelButtons [0].transform.position.x, LevelButtons [0].transform.position.y + 40, LevelButtons [0].transform.position.z);
		}
		if (isatLevel1) {
			Target = LevelButtons [1].transform;
			CharacterParent.transform.position = new Vector3 (LevelButtons [0].transform.position.x, LevelButtons [0].transform.position.y + 40, LevelButtons [0].transform.position.z);
		}
		if (isatLevel2) {
			Target = LevelButtons [2].transform;
			CharacterParent.transform.position = new Vector3 (LevelButtons [2].transform.position.x, LevelButtons [2].transform.position.y + 40, LevelButtons [2].transform.position.z);
		}
		if (isatLevel3) {
			Target = LevelButtons [3].transform;
			CharacterParent.transform.position = new Vector3 (LevelButtons [3].transform.position.x, LevelButtons [3].transform.position.y + 40, LevelButtons [3].transform.position.z);
		}
		if (isatLevel4) {
			Target = LevelButtons [4].transform;
			CharacterParent.transform.position = new Vector3 (LevelButtons [4].transform.position.x, LevelButtons [4].transform.position.y + 40, LevelButtons [4].transform.position.z);
		}
		if (isatLevel5) {
			Target = LevelButtons [5].transform;
			CharacterParent.transform.position = new Vector3 (LevelButtons [5].transform.position.x, LevelButtons [5].transform.position.y + 40, LevelButtons [5].transform.position.z);
		}




	}

	void Update ()
	{

		float step = 300 * Time.deltaTime;
		CharacterParent.transform.position = Vector3.MoveTowards (CharacterParent.transform.position, new Vector3 (Target.position.x, Target.position.y + 40, Target.position.z), step);


		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();

		while ((i) == AreaProgress) {
			LevelButtons [i].GetComponent<Button> ().interactable = true;
			foreach (GameObject g in LevelButtons) {
				if (g.GetComponent<Button> ().interactable) {
					eventSystem.SetSelectedGameObject (g, new BaseEventData (eventSystem)); // change this to the next one
				}
			}

			i++;
		}

		if (AreaProgress > 1) {
			LevelButtons [0].GetComponent<Button> ().interactable = false;
		} else {
			LevelButtons [0].GetComponent<Button> ().interactable = true;
		}


		while ((i) < AreaProgress) {
			if (i < AreaProgress) {
				LevelButtons [i].GetComponent<Button> ().interactable = false;
			} else {
				LevelButtons [i].GetComponent<Button> ().interactable = true;
			}
			i++;
		}



	}

	public void ToTutorial ()
	{

		if (LevelsSelected [0] == 1) {
			Debug.Log ("Pressed Tutorial");
//			PlayerPrefs.SetInt ("Tutorial", 1);
//			PlayerPrefs.SetInt ("AreaProgress", 1);

			if (isatLevel1) {
				isatLevel1 = false;
				isatTutorial = true;
				
			}

			if (isatTutorial) {
				foreach (GameObject c in LevelButtons) {
					if (EventSystem.current.currentSelectedGameObject == c) {

						Application.LoadLevel ("LoadingScreen");
						PlayerPrefs.SetString ("LevelToLoad", LevelButtons [0].name);

					}
				}
			}
		} else {
			Target = LevelButtons [0].transform;
			LevelsSelected [0] = 1;
			animCharacter.SetTrigger ("Walk");
			LevelsSelected [1] = 0;
		}
	}

	public void ToLevel1 ()
	{

		if (LevelsSelected [1] == 1) {
			PlayerPrefs.SetInt ("AreaProgress", 2); // move this to the end of the area1 boss victory screen

			if (isatTutorial) {


				isatLevel1 = true;
				isatTutorial = false;

			}

			if (isatLevel1) {
				foreach (GameObject c in LevelButtons) {
					if (EventSystem.current.currentSelectedGameObject == c) {

						Application.LoadLevel ("LoadingScreen");
						PlayerPrefs.SetString ("LevelToLoad", LevelButtons [1].name);

					}
				}
			}
		} else {
			Target = LevelButtons [1].transform;
			Debug.Log ("Pressed Level 1");
			animCharacter.SetTrigger ("Walk");
			LevelsSelected [1] = 1;
			LevelsSelected [0] = 0;
		}
	}

	public void ToLevel2 ()
	{
	
		if (LevelsSelected [2] == 1) {
			PlayerPrefs.SetInt ("AreaProgress", 3); // move this to the end of the area2 boss victory screen


			if (isatTutorial) {
				animCharacter.SetTrigger ("Walk");
				isatLevel1 = true;
				isatTutorial = false;

			}
			if (isatLevel1) {

				isatLevel2 = true;
				isatLevel1 = false;

			}

			if (isatLevel2) {
				foreach (GameObject c in LevelButtons) {
					if (EventSystem.current.currentSelectedGameObject == c) {

						Application.LoadLevel ("LoadingScreen");
						PlayerPrefs.SetString ("LevelToLoad", LevelButtons [2].name);

					}
				}
			}
		} else {
			Target = LevelButtons [2].transform;
			animCharacter.SetTrigger ("Walk");
			LevelsSelected [2] = 1;
		}

	}

	public void ToLevel3 ()
	{
	
		if (LevelsSelected [3] == 1) {

			PlayerPrefs.SetInt ("AreaProgress", 4); // move this to the end of the area3 boss victory screen


			if (isatTutorial) {
				animCharacter.SetTrigger ("Walk");
				isatLevel1 = true;
				isatTutorial = false;

			}
			if (isatLevel1) {
				animCharacter.SetTrigger ("Walk");
				isatLevel2 = true;
				isatLevel1 = false;

			}
		
	
			if (isatLevel2) {

				isatLevel3 = true;
				isatLevel2 = false;

			}
	
			if (isatLevel3) {
				foreach (GameObject c in LevelButtons) {
					if (EventSystem.current.currentSelectedGameObject == c) {

						Application.LoadLevel ("LoadingScreen");
						PlayerPrefs.SetString ("LevelToLoad", LevelButtons [3].name);
					
					}
				}
			}
		} else {
			Target = LevelButtons [3].transform;
			animCharacter.SetTrigger ("Walk");
			LevelsSelected [3] = 1;
		}
	}

	public void ToLevel4 ()
	{

		if (LevelsSelected [4] == 1) {

			PlayerPrefs.SetInt ("AreaProgress", 5); // move this to the end of the area4 boss victory screen


			if (isatTutorial) {
				animCharacter.SetTrigger ("Walk");
				isatLevel1 = true;
				isatTutorial = false;

			}
			if (isatLevel1) {
				animCharacter.SetTrigger ("Walk");
				isatLevel2 = true;
				isatLevel1 = false;
		
			}
		
		
			if (isatLevel2) {
				animCharacter.SetTrigger ("Walk");
				isatLevel3 = true;
				isatLevel2 = false;

			}
	
			if (isatLevel3) {

				isatLevel4 = true;
				isatLevel3 = false;

			}

			if (isatLevel4) {
				foreach (GameObject c in LevelButtons) {
					if (EventSystem.current.currentSelectedGameObject == c) {

						Application.LoadLevel ("LoadingScreen");
						PlayerPrefs.SetString ("LevelToLoad", LevelButtons [4].name);

					}
				}
			}
		} else {
			Target = LevelButtons [4].transform;
			animCharacter.SetTrigger ("Walk");
			LevelsSelected [4] = 1;
		}

	}

	public void ToLevel5 ()
	{		

		if (LevelsSelected [5] == 1) {

			if (isatTutorial) {
				animCharacter.SetTrigger ("Walk");
				isatLevel1 = true;
				isatTutorial = false;
			
			}
			if (isatLevel1) {
				animCharacter.SetTrigger ("Walk");
				isatLevel2 = true;
				isatLevel1 = false;
			
			}
		
		
			if (isatLevel2) {
				animCharacter.SetTrigger ("Walk");
				isatLevel3 = true;
				isatLevel2 = false;
			
			}
		
			if (isatLevel3) {
				animCharacter.SetTrigger ("Walk");
				isatLevel4 = true;
				isatLevel3 = false;
			
			}

			if (isatLevel4) {

				isatLevel5 = true;
				isatLevel4 = false;
			
			}

			if (isatLevel5) {
				foreach (GameObject c in LevelButtons) {
					if (EventSystem.current.currentSelectedGameObject == c) {

						Application.LoadLevel ("LoadingScreen");
						PlayerPrefs.SetString ("LevelToLoad", LevelButtons [5].name);

					}
				}
			}
		
		} else {
			Target = LevelButtons [5].transform;
			animCharacter.SetTrigger ("Walk");
			LevelsSelected [5] = 1;
		}
	}

	public void hightbutton ()
	{
		GetComponent<AudioSource> ().clip = Menu.staticSwipesound;
		GetComponent<AudioSource> ().Play ();
		
	}

	public void pushbutton ()
	{
		GetComponent<AudioSource> ().clip = Menu.staticPressedsound;
		GetComponent<AudioSource> ().Play ();
		
	}

}
