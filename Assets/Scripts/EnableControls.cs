using UnityEngine;
using System.Collections;

public class EnableControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Tutorial.allowSwitch = true;
		Tutorial.allowFineAim = true;
		Tutorial.allowMove = true;
		Tutorial.allowAim = true;
		Tutorial.AllowCombo = true;
		Tutorial.AllowComboCountdown = true;
		Tutorial.AllowBuffs = true;
		Tutorial.AllowNeedle = true;
		Tutorial.AllowFury = true;
		Tutorial.AllowShowMap = true;
		Tutorial.AllowBuffTimer = true;
		Tutorial.AllowReload = true;
		Tutorial.AllowAttack = true;
		Tutorial.AllowCharacterChoose = true;
	}

}
