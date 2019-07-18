using UnityEngine;
using System.Collections;

public class DifficultyLevel : MonoBehaviour {
	public int died;
	public int LevelID;
	public GameObject Level1;
	public GameObject Level2;
	public enum Difficulty
	{
		Easy,
		Medium,
		Hard}
	;
	public Difficulty DifficultySetting = Difficulty.Easy;
	// Use this for initialization
	void Start () {
//		Application.targetFrameRate = 300;

	}
	
	// Update is called once per frame
	void Update () {
//			Level1 = GameObject.FindGameObjectWithTag("Level1");
//			Level2 = GameObject.FindGameObjectWithTag("Level2");
//		DontDestroyOnLoad(this.gameObject);

		switch (LevelID) {
		case 0:
			Level1.SetActive(true);
			Level2.SetActive(false);
			break;
		case 1:
			Level1.SetActive(false);
			Level2.SetActive(true);
			break;
			
		}
	}
}
