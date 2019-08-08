using UnityEngine;
using System.Collections;

public class DoorEntryRequirement : MonoBehaviour
{

	public int EntryCost;
	public bool ObjectiveCompleted;
	GameObject Player;
	WaveManager WM;

	public enum Sections
	{
		oneTwo,
		oneThree,
		oneFour,
		oneFive,
		twoThree,
		threeFour,
		fourFive,
		fiveTwo}

	;

	public Sections sectionDoors;

	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		WM = GameObject.Find ("GameManager").GetComponent<WaveManager> ();
	}

	void Update ()
	{
		if (Vector2.Distance (transform.position, Player.transform.position) < 3) {
			// show the cost label
			if (Input.GetButton ("Submit")) {
				if (CharacterStats.Score >= EntryCost && ObjectiveCompleted) {
					CharacterStats.Score -= EntryCost;
					AddSpawnLocations ();
					this.Recycle ();
				} else {
					// play error sound
				}
			}

		}
	}

	void AddSpawnLocations ()
	{
		switch (sectionDoors) {

		case Sections.oneTwo:
			foreach (Transform child in WM.SectionLocationSpawns[0].transform) {
				WM.EnemySpawnLocations.Add (child);
			}
			break;
		case Sections.oneThree:

			break;
		case Sections.oneFour:

			break;
		case Sections.oneFive:

			break;
		case Sections.twoThree:

			break;
		case Sections.threeFour:

			break;
		case Sections.fourFive:

			break;
		case Sections.fiveTwo:

			break;
		}
	}

}
