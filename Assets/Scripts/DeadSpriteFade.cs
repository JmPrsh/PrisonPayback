using UnityEngine;
using System.Collections;

public class DeadSpriteFade : MonoBehaviour
{

//	Transform Child;
//	Transform ChildChild;
	public SpriteRenderer ChildSprite;
	public SpriteRenderer ChildChildSprite;
	float FadeOut = 1f;
	bool FadeOutBool;

	// Use this for initialization
	void Start ()
	{
//		Child = GetComponentInChildren<Transform> ();
//		ChildChild = Child.GetComponentInChildren<Transform> ();
		foreach (Transform Child in this.gameObject.transform) {
			ChildSprite = Child.GetComponent<SpriteRenderer> ();
			foreach (Transform Children in Child) {
				ChildChildSprite = Children.GetComponent<SpriteRenderer> ();
			}
		}

		StartCoroutine (Fade ());
	}

	// Update is called once per frame
	void Update ()
	{
	
		if (ChildSprite.color.a == 0.0f) {
			this.Recycle ();
		}
		if (FadeOutBool) {
			FadeOut -= 0.7f * Time.deltaTime;
			ChildSprite.color = new Color (1f, 1f, 1f, FadeOut);
			ChildChildSprite.color = new Color (1f, 1f, 1f, FadeOut);
		}
	}

	IEnumerator Fade(){
		yield return new WaitForSeconds (4);
		FadeOutBool = true;
	}
}
