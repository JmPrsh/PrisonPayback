using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Unlocks : MonoBehaviour {

	public List<GameObject> Characters;
	public Sprite[] LockedCharacters;
	public Sprite[] UnlockedCharacters;
	public int CharacterID;
	EventSystem eventSystem;

	public GameObject Scrollbar;
	public GameObject Scrollbar2;
	public GameObject SnapShotPosition;
	public GameObject SnapShotAchievementPosition;
	RectTransform R;
	RectTransform A;
	Vector3 MypositionI;
	Vector3 MypositionA;

	public Animator anim;

	public GameObject inmates;
	public GameObject achievements;
	public int CanvasID;
    public Vector3 offsetpos;

	// Use this for initialization
	void Start () {
		R = SnapShotPosition.GetComponent<RectTransform> ();
		A = SnapShotAchievementPosition.GetComponent<RectTransform> ();
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
	}

    public void GoToMenu(){
        SceneManager.LoadScene("Main");
    }
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetButtonUp ("LB")) {
		//	if (CanvasID > 0) {
		//		CanvasID -= 1;
		//		anim.SetTrigger("ShowInmates");
		//		GetComponent<AudioSource> ().clip = Menu.staticPressedsound;
		//		GetComponent<AudioSource> ().Play ();
		//	}
		//}
		//if (Input.GetButtonUp ("RB")) {
		//	if (CanvasID < 1) { // change canvas limit amount in the array from the inspector
		//		CanvasID += 1;
		//		anim.SetTrigger("ShowAchievements");
		//		GetComponent<AudioSource> ().clip = Menu.staticPressedsound;
		//		GetComponent<AudioSource> ().Play ();
		//	}
		//}

//		switch (CanvasID) {
//		case 0:
////			CanvasAnim.SetBool ("gotoArea1Boss", false);
//                MypositionI = new Vector3 (743, ((Scrollbar.GetComponent<EnergyBar> ().valueCurrent* 650)-227), MypositionI.z) + offsetpos;
//			R.localPosition = MypositionI;
//			eventSystem.SetSelectedGameObject (Scrollbar, new BaseEventData (eventSystem));
////			inmates.SetActive(true);
////			achievements.SetActive(false);
//			break;
//		case 1:
////			CanvasAnim.SetBool ("gotoArea1Boss", true);
////			CanvasAnim.SetBool ("gotoArea2", false);
//			MypositionA = new Vector3 (0, Scrollbar2.GetComponent<EnergyBar> ().valueCurrent* 2750, MypositionA.z);
//			A.localPosition = MypositionA;
//			eventSystem.SetSelectedGameObject (Scrollbar2, new BaseEventData (eventSystem));
////			inmates.SetActive(false);
////			achievements.SetActive(true);
		//	break;
		//}
	}
}
