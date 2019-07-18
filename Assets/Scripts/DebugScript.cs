using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour {
	PlayerModel playermodel;
	public Text StateValue;
	public Text State;
	public Text EnemySpawnAmount;

	public Text EnemySpeed;
	public Text EnemyAddAmount;

	// Use this for initialization
	void Start () {
		playermodel = GameObject.FindGameObjectWithTag ("PlayerModel").GetComponent<PlayerModel> ();
	}
	
	// Update is called once per frame
	void Update () {

		StateValue.text = "State Value: " + playermodel.StateValue.ToString ();
		State.text = "State: " + playermodel.StateName.ToString();
		EnemySpawnAmount.text = "Enemy Spawn Amount: " + playermodel.EnemySpawnAmount.ToString ();
	
		EnemySpeed.text = "Enemy Speed: " + StaticVariables.EnemySpeed.ToString ();
		EnemyAddAmount.text = "Enemies For Next Area: " + playermodel.EnemyAdd.ToString ();
	
	}
}
