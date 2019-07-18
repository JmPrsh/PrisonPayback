using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{

	public Transform Target;
	public float speed;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
//		float step = speed * Time.deltaTime;
//		if (Vector2.Distance (transform.position, Target.position) > 0.1f) {
//			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (Target.position.x, Target.position.y, this.transform.position.z), step);
//		} 
//		Vector3.Lerp (transform.position, new Vector3 (Target.position.x, Target.position.y + 0.8f, this.transform.position.z), step);
		transform.position = new Vector3 (Target.position.x, Target.position.y, this.transform.position.z);
	}
}
