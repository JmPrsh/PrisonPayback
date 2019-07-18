using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour
{
	public string instructions;

	void Update ()
	{
		style.fontSize = Screen.width / 30;
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel (Application.loadedLevel + 1);
		}
		instructions = 
						"Try and reach the end\n" +
			"\nUse the Arrow keys or WASD keys to move\n" +
				"\nUse the mouse to aim where you want to strike/shoot and click to strike/fire\n" +
			"\nBat - 1 \n" +
			"\nPistol - 2 \n" +
			"\nMachine Gun - 3 \n" +
			"\nShotgun - 4 \n" +
			"\nCLICK ANYWHERE TO START";

	}
	public GUIStyle style;
	void OnGUI ()
	{
		GUI.Label (new Rect (20, 20, Screen.width - 20, Screen.height - 20), instructions,style);
	}

}
