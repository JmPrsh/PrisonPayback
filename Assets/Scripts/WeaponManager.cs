using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{

	enum WeaponType
	{
		Fists,
		Pipe,
		Knife,
		Pistol,
		Assault,
		Shotgun,
		Sniper,
        MiniGun
	}
	;

	public bool PickedUp;
	CharacterStats CS;
	public int currentWeaponIndex = 0;

	void Start ()
	{
		CS = GetComponent<CharacterStats> ();

	}

	public void PreviousWeaponButton(){
		if (CS.ammo.reloading) {
            cancelReload();
        }
		PreviousWeapon ();
	}

    void cancelReload()
    {
        CS.ammo.ReloadGUI.SetActive(false);
        CS.ammo.reloading = false;
        CS.ammo.ReloadGUI.GetComponent<EnergyBar>().valueCurrent= CS.ammo.ReloadGUI.GetComponent<EnergyBar>().valueMax;
        CS.ammo.ReloadGraphic.SetActive(false);
    }

	public void NextWeaponButton(){
		if (CS.ammo.reloading) {
            cancelReload();
        }
		NextWeapon ();
	}

	void Update ()
	{
		
	}

	void  NextWeapon ()
	{
		currentWeaponIndex += 1;
		if (currentWeaponIndex >= CS.Weapons.Length) {
			currentWeaponIndex = 0;	
		}
		for (int i = 0; i < CS.Weapons.Length-1; i++) {

			if (CS.Weapons [currentWeaponIndex].GetComponent<PickedUpCheck> ().pickedUp == false) {
				NextWeapon ();
			}

		}
        CS.WhichWeapon = currentWeaponIndex;
    }

	void  PreviousWeapon ()
	{
		currentWeaponIndex -= 1;
		if (currentWeaponIndex < 0) {
			currentWeaponIndex = CS.Weapons.Length - 1;	
		}
		for (int i = 0; i < CS.Weapons.Length-1; i++) {

			if (CS.Weapons [currentWeaponIndex].GetComponent<PickedUpCheck> ().pickedUp == false) {
				PreviousWeapon ();
			}

		}
        CS.WhichWeapon = currentWeaponIndex;
    }

}
