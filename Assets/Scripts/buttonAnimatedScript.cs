using UnityEngine;
using System.Collections;

public class buttonAnimatedScript : MonoBehaviour {

	public int alreadyAnimated;
	public string playerprefsavename;

	void Start(){
		playerprefsavename = this.gameObject.name;
		alreadyAnimated = PlayerPrefs.GetInt (playerprefsavename);
	}
	void Update(){
		if (alreadyAnimated > 1) {
			alreadyAnimated = 1;
		}
	}
}
