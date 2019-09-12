using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager WM;

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

	void Awake ()
	{
        WM = this;
		CS = GetComponent<CharacterStats> ();
       
    }

    private void Start()
    {
        SetCurrentWeaponIndex(0);
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

	void NextWeapon ()
	{
		currentWeaponIndex += 1;
		if (currentWeaponIndex >= CS.Weapons.Count) {
			currentWeaponIndex = 0;	
		}
		for (int i = 0; i < CS.Weapons.Count-1; i++) {

			if (CS.Weapons [currentWeaponIndex].GetComponent<PickedUpCheck> ().pickedUp == false) {
				NextWeapon ();
			}

		}
        CS.UpdateWeapon(currentWeaponIndex);
    }

	void PreviousWeapon ()
	{
		currentWeaponIndex -= 1;
		if (currentWeaponIndex < 0) {
			currentWeaponIndex = CS.Weapons.Count - 1;	
		}
		for (int i = 0; i < CS.Weapons.Count-1; i++) {

			if (CS.Weapons [currentWeaponIndex].GetComponent<PickedUpCheck> ().pickedUp == false) {
				PreviousWeapon ();
			}

		}
        CS.UpdateWeapon(currentWeaponIndex);
    }

    public void SetCurrentWeaponIndex(int i)
    {
        currentWeaponIndex = i;
        CS.UpdateWeapon(currentWeaponIndex);
    }
}
