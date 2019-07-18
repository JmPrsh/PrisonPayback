using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{

	public AudioClip MenuMusic;
	public AudioClip InGameMusic;
	public AudioClip BuffedStrengthMusic;
	public AudioClip BuffedSpeedMusic;
	public static AudioSource staticGeneralMusic;

	public static int whichMusic;
	public static bool playMusic;

	// Use this for initialization
	void Start ()
	{
		staticGeneralMusic = GetComponent<AudioSource> ();	
		whichMusic = 1;

	}
	
	// Update is called once per frame
	void Update ()
	{
//		staticGeneralMusic.Play ();
			
		if (playMusic) {
			switch (whichMusic) {
			case 1:
				staticGeneralMusic.clip = MenuMusic;
			staticGeneralMusic.Play ();
				break;
			case 2:
				staticGeneralMusic.clip = InGameMusic;
			staticGeneralMusic.Play ();
				break;
			case 3:
				staticGeneralMusic.clip = BuffedStrengthMusic;
			staticGeneralMusic.Play ();
				break;
			case 4:
				staticGeneralMusic.clip = BuffedSpeedMusic;
			staticGeneralMusic.Play ();
				break;

			}
			playMusic=false;
		}
	}



		
}
