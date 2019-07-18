using UnityEngine;
using System.Collections;

public class bossattackPlayer : MonoBehaviour
{

		GameObject Player;
		public GameObject PlayerLook;
		public float tweenStopDistance = 0.2f;
		public float tweenSpeed = 1.0f;
		public Vector3 targetPosition = new Vector3 ();
		Animator anim;
		public bool startMoving;
	
		// Use this for initialization
		void Start ()
		{
				PlayerLook = GameObject.FindGameObjectWithTag ("Player");
				startMoving = false;
				Player = GameObject.FindGameObjectWithTag ("Player");
				PlayerLook = GameObject.FindGameObjectWithTag ("Player");
				targetPosition = Player.transform.position;
		}
	
		// Update is called once per frame
	
	
	
	
		void Update ()
		{

				if (!startMoving) {
						transform.Translate (Random.Range (-0.0001f, 0.0002f), Random.Range (-0.0001f, 0.0002f), 0);
				}
				if (startMoving) {
						Vector3 dir = Player.transform.position - transform.position;
						float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
						transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
						targetPosition = Player.transform.position;
						if ((transform.position - targetPosition).magnitude > tweenStopDistance) {
								transform.position = Vector3.MoveTowards (transform.position, targetPosition, tweenSpeed * Time.deltaTime);
						}
			
				} else {
						targetPosition = Player.transform.position;
				}
		}

}
