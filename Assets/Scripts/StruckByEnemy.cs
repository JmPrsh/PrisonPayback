using UnityEngine;
using System.Collections;

public class StruckByEnemy : MonoBehaviour {

	public Rigidbody2D ParentGORigidbody2D;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Knockback(float knockDur,float knockbackPwr,Vector2 knockbackDir){

		// float timer = 0;
		// ParentGORigidbody2D.velocity = new Vector2 (ParentGORigidbody2D.velocity.x, 0);
		// while (knockDur > timer) {

		// 	timer += Time.deltaTime;

		// 	ParentGORigidbody2D.AddForce(new Vector2(knockbackDir.x * -100, knockbackDir.y * knockbackPwr));

		// }

		yield return null;
	}
}
