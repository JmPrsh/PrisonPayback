using UnityEngine;
using System.Collections;

public class GetHit : MonoBehaviour {

	public AudioClip[] HitSound;
	AudioSource audiosource;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D ( Collider2D other ){

		if (other.GetComponent<Collider2D> ().CompareTag ("PlayerMelee")) {
			audiosource.clip = HitSound[Random.Range (0,HitSound.Length)];
			audiosource.Play ();

		}

	}
}
