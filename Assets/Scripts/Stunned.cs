using UnityEngine;
using System.Collections;

public class Stunned : MonoBehaviour
{

	public CharacterStats characterparent;
	SpriteRenderer spriterender;
	public SpriteRenderer childspriterender;
	public bool shock = true;
	bool once;

	// Use this for initialization
	void Start ()
	{
		spriterender = GetComponent<SpriteRenderer> ();
//		StartCoroutine (Flip ());
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (shock) {
			spriterender.material.SetFloat ("_ColourAmount", 0.0f);
			childspriterender.enabled = true;
		} else {
			spriterender.material.SetFloat ("_ColourAmount", 2.0f);
			childspriterender.enabled = false;
		}

		if (characterparent.stunned) {
		if (!once) { 
				StartCoroutine (Flip ());
			once = true;

		} 		

		} else {
			once = false;
			spriterender.material.SetFloat ("_ColourAmount", 2.0f);
			childspriterender.enabled = false;
//
		}
	
	}

	IEnumerator Flip ()
	{
		if (characterparent.stunned) {	
		shock = !shock;		
		yield return new WaitForSeconds (0.05f);
		StartCoroutine (Flip ());	
		}
	}

}
