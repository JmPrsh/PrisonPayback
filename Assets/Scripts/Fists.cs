using UnityEngine;
using System.Collections;

public class Fists : MonoBehaviour {

	public Sprite[] EthnicHands;
	public Sprite[] EthnicDeathHands;
	public GameObject ParentGO;
    SpriteRenderer sr;
	public enum WhichHolder
	{
		Player,
		Other

		
	}
	;
	public WhichHolder whichholder;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (whichholder == WhichHolder.Player) {
			if (CharacterStats.CS.EthnicCharacter == CharacterStats.Ethnic.White) {
				sr.sprite = EthnicHands [0];
				if (CharacterStats.CS.Dead) {
					sr.sprite = EthnicDeathHands [0];
				}
			} else if (CharacterStats.CS.EthnicCharacter == CharacterStats.Ethnic.Hispanic) {
				sr.sprite = EthnicHands [1];
				if (CharacterStats.CS.Dead) {
					sr.sprite = EthnicDeathHands [1];
				}
			} else if (CharacterStats.CS.EthnicCharacter == CharacterStats.Ethnic.Brown) {
				sr.sprite = EthnicHands [2];
				if (CharacterStats.CS.Dead) {
					sr.sprite = EthnicDeathHands [2];
				}
			} else {
				sr.sprite = EthnicHands [3];
				if (CharacterStats.CS.Dead) {
					sr.sprite = EthnicDeathHands [3];
				}
			}


			switch (CharacterStats.CharacterSpriteID) {

			case 0:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 1:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 2:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 3:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 4:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 5:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 6:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 7:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 8:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 9:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 10:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 11:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 12:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 13:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 14:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 15:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 16:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 17:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 18:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 19:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 20:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 21:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 22:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 23:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 24:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 25:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 26:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 27:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 28:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 29:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 30:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 31:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 32:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 33:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 34:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 35:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 36:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Brown;
				break;
			case 37:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.Hispanic;
				break;
			case 38:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;
			case 39:
				CharacterStats.CS.EthnicCharacter = CharacterStats.Ethnic.White;
				break;

			}


		} 
	}
}
