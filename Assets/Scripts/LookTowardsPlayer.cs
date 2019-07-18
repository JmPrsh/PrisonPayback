using UnityEngine;
using System.Collections;

public class LookTowardsPlayer : MonoBehaviour
{
	attackPlayer aP;
	GameObject Player;
	public Transform[] Targets;
	public GameObject Target;
	int chooseTarget;

	// Update is called once per frame

	void Start ()
	{
		aP = GetComponentInParent<attackPlayer> ();
	}

	void Update ()
	{

		Target = aP.Target;

		if (aP.seenPlayer && !aP.reload) {
			if (Target != null) {
			Player = GameObject.FindGameObjectWithTag ("Player");
			var dir = Target.transform.position - transform.position;
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			var newRotation = Quaternion.AngleAxis (angle - 90f, transform.forward);
			if (aP.seenPlayer == false) {
				transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime);
			} else {
				if (aP.typeofenemy == attackPlayer.TypeOfEnemy.Sniper) {
					transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 8);
				} else {
					transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 4);
				}
			}
			}
		} else {
			if (Target != null) {
				var dir = Target.transform.position - transform.position;
				var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				var newRotation = Quaternion.AngleAxis (angle - 90f, transform.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime / 2);
			}

		}
	
	}
	
}
