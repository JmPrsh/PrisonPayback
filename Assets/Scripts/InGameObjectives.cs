using UnityEngine;
using System.Collections;

public class InGameObjectives : MonoBehaviour {


	public enum ObjectiveTask{

		EnemyCount,
		Electricity,
		KeyCollect,
		MiniBoss


	};
	public ObjectiveTask OT;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (OT == ObjectiveTask.EnemyCount) {


		}

		if (OT == ObjectiveTask.Electricity) {
			
			
		}

		if (OT == ObjectiveTask.KeyCollect) {
			
			
		}

		if (OT == ObjectiveTask.MiniBoss) {
			
			
		}









	
	}
}
