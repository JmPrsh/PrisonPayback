using UnityEngine;
using System.Collections;

public class LookTowardsMouse : MonoBehaviour
{

	// Update is called once per frame
	void Update ()
	{
			Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);
			float angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (-angle, transform.forward);

	}


}
