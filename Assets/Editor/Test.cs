using UnityEditor;
using UnityEngine;

public class PlayerPrefListing : EditorWindow 
{
	[MenuItem("Jamie's Plugin/Clear PlayerPrefs %_1")]


//	[MenuItem("Tools/Clear PlayerPrefs ")]
	private static void NewMenuOption()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log ("PlayerPrefs Deleted");
	}

//	[MenuItem ("Jamie's Plugin/Show PlayerPrefs %_2")]
//	static void Init () {
//		// Get existing open window or if none, make a new one:
//		PlayerPrefListing window = (PlayerPrefListing)EditorWindow.GetWindow (typeof (PlayerPrefListing));
//		window.Show();
//	}
}
