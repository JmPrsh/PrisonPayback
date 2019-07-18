using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VerticalWallCheck : MonoBehaviour
{


	public GameObject Target;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (RemoveWalls ());
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	IEnumerator RemoveWalls ()
	{

		yield return new WaitForSeconds (1f);
		FindClosestWalls ();
		yield return new WaitForSeconds (0.1f);
		if (Target != null) {
			Target.transform.Recycle ();
			yield return new WaitForSeconds (0.1f);
			this.Recycle ();
		}
	}

	GameObject FindClosestWalls ()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag ("VerticalWall");
		float distance = float.MaxValue;
		Vector3 position = transform.position;
		

		foreach (GameObject go in gos.Where(x=> x != gameObject)) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				if (Vector3.Distance (go.transform.position, transform.position) < 1) {
					Target = go;
				}
//					Debug.Log ("curdistance " + curDistance);
				distance = curDistance;
			} else {

			}
		}
			

		
		return Target;

	}

}
