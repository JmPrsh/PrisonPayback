using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{

	public GameObject TempDoors;
//	EnemyManager EM;
	public bool checkForEnemies;
	PlayerModel playerModel;
	BoxCollider2D bcollider2d;

	public enum RoomType
	{
		Enemy,
		Resource,
		Objective


	}
	;
	public RoomType roomtype;
	public int RandomRoomType;
	public bool spawnitems;

	// Use this for initialization
	void Start ()
	{
//		EM = GameObject.Find ("EnemyLevel1Manager").GetComponent<EnemyManager> ();
		TempDoors.SetActive (false);
		playerModel = GameObject.FindGameObjectWithTag ("PlayerModel").GetComponent<PlayerModel> ();
		bcollider2d = GetComponent<BoxCollider2D> ();

		RandomRoomType = Random.Range (0, 6);
		if (RandomRoomType > 1) {
		
			roomtype = RoomType.Enemy;
		} else {
			roomtype = RoomType.Resource;

		}
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (roomtype == RoomType.Enemy) {
			if (checkForEnemies) {
				if (playerModel.AllEnemiesStillAlive.Count == 0) {
					Stats.Rooms += 1;
					TempDoors.SetActive (false);
					this.Recycle ();
				}
			}
		}
		if (roomtype == RoomType.Resource) {
			if (spawnitems) {
				for (int i = 0; i < ItemSpawner.ItemAmount; i++) {
					ItemSpawner.ItemsStatic [Random.Range (0, ItemSpawner.ItemsStatic.Length)].Spawn (transform.position + (Vector3)Random.insideUnitCircle * 3, Quaternion.identity);
				}
				spawnitems = false;
			}
		}
		if (roomtype == RoomType.Objective) {
	
		}
	}

	void waitforcheck ()
	{
		checkForEnemies = true;
		playerModel.checkingEnemiesInList = true;

	}

}
