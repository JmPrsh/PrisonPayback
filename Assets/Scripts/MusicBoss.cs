using UnityEngine;
using System.Collections;

public class MusicBoss : MonoBehaviour
{
	
		float s = 0.0F;
		AudioListener main;

	public bool higher;

		// Use this for initialization
		void Start ()
		{
				main =GetComponent<AudioListener> ();

		}
	
		// Update is called once per frame
		void Update ()
		{

				main.GetComponent<AudioSource>().volume = s;
				if (s < 0.0f) {
						s = 0.0f;
//			this.gameObject.SetActive(false);
				} else if (s > 1.0f) {
						s = 1.0f;
				}
		if(higher){
			s += 0.2f * Time.deltaTime *2;

		}
		else{
			s -= 0.2f * Time.deltaTime *2;

		}
//				DontDestroyOnLoad (this.gameObject);
		}



		
}
