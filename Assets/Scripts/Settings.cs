using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Settings : MonoBehaviour
{
	//	public GameObject FadeGO;
	public GameObject[] Buttons;
	int whichButton;
	public Color selectedColor;
	
	public float MusicSlider;
	public bool Vibration;
	public int VibrationInt;
	public Sprite[] ToggleSprite;
	public float VibrateAmount;
	public GameObject MusicBar;
	public GameObject SoundFXBar;
	public static float MusicValue;
	
	public float OveralSoundEffectsVolume;
	
	public float BrightnessValue;
	
	public float BrightnessSlider;
	
	public static Color c;
	// Use this for initialization
	void Start ()
	{
		//		FadeGO.SetActive (true);
		
//		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");
		
		Buttons [0].GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("Volume");
		
		Buttons [4].GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("Brightness");
		
		Buttons [6].GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("SoundFXVolume");
		
		MusicBar.GetComponent<Slider> ().value = 20;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//		Debug.Log (Buttons [0].GetComponent<Slider> ().value);
		
		
		//		if (VibrateAmount > 0f) {
		//			VibrateAmount-=1f * Time.deltaTime*2;
		//		}else{
		//			VibrateAmount = 0f;
		//		}
		//		Debug.Log ("Selected GameObject " + EventSystem.current.currentSelectedGameObject);
		
//		if (EventSystem.current.currentSelectedGameObject == Buttons [0]) {
//			Buttons [1].GetComponent<Text> ().color = selectedColor;
//			
//		} else {
//			Buttons [1].GetComponent<Text> ().color = Color.white;
//		}
//		if (EventSystem.current.currentSelectedGameObject == Buttons [2]) {
//			Buttons [3].GetComponent<Text> ().color = selectedColor;
//			
//		} else {
//			Buttons [3].GetComponent<Text> ().color = Color.white;
//		}
//		if (EventSystem.current.currentSelectedGameObject == Buttons [4]) {
//			Buttons [5].GetComponent<Text> ().color = selectedColor;
//			
//		} else {
//			Buttons [5].GetComponent<Text> ().color = Color.white;
//		}
//		if (EventSystem.current.currentSelectedGameObject == Buttons [6]) {
//			Buttons [7].GetComponent<Text> ().color = selectedColor;
//			
//		} else {
//			Buttons [7].GetComponent<Text> ().color = Color.white;
//		}
		
		// volume controller
		MusicSlider = Buttons [0].GetComponent<Slider> ().value;
		MusicBar.GetComponent<Slider> ().value = (int)Buttons [0].GetComponent<Slider> ().value;
		
		SoundFXBar.GetComponent<Slider> ().value = (int)Buttons [6].GetComponent<Slider> ().value;
//		OveralSoundEffectsVolume = Buttons [6].GetComponent<Slider> ().value;
		
		//		AudioListener.volume = Buttons [0].GetComponent<Slider> ().value/20; // change this to get the valueCurrentof the music 
		// brightness controller
		BrightnessSlider = Buttons [4].GetComponent<Slider> ().value;
//		GameObject Brightness = GameObject.Find ("BrightnessValue");
//		c = Brightness.GetComponent<Image> ().color;
//		c.a = Mathf.Lerp (0.5f, -0.5f, BrightnessSlider);
//		Brightness.GetComponent<Image> ().color = c;
		
		PlayerPrefs.SetFloat ("Volume", Buttons [0].GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("SoundFXVolume", Buttons [6].GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("Brightness", BrightnessSlider);
		
		if (Vibration) {
			Buttons [2].GetComponentInChildren<Image>().sprite = ToggleSprite[0];
			VibrationInt = 1;
			PlayerPrefs.SetInt("Vibration",VibrationInt);
			
			
		} else {
			Buttons [2].GetComponentInChildren<Image>().sprite = ToggleSprite[1];
			VibrationInt = 0;
			PlayerPrefs.SetInt("Vibration",VibrationInt);
		}
		
	}
	
	IEnumerator Vibrate(){
		
		VibrateAmount = 1;
		yield return new WaitForSeconds(0.3f);
		VibrateAmount = 0;
	}
	
	public void LoadScene ()
	{
		
		Application.LoadLevel ("TitleScreen");
	}
	
	public void VibrationToggle(){
		
		Vibration = !Vibration;
		GetComponent<AudioSource> ().clip = Menu.staticPressedsound;
		GetComponent<AudioSource> ().Play ();
		if (Vibration) {
			StartCoroutine(Vibrate());
		}
	}
	
	public void hightbutton(){
		GetComponent<AudioSource> ().clip = Menu.staticHighlightsound;
		GetComponent<AudioSource> ().Play ();
		
	}
	
	public void PlaySound(){
		GetComponent<AudioSource> ().clip = Menu.staticPressedsound;
		GetComponent<AudioSource> ().Play ();
		
	}
}
