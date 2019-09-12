using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
 
    // Gun Capacities
    public int PistolMaxAmmo = 10;
    public int MachineGunMaxAmmo = 10;
    public int ShotgunMaxAmmo = 10;
    public int SniperMaxAmmo = 10;
    public int MinigunMaxAmmo = 10;

    // Gun Clip Size
    [Header("Clips Size")]
    public int PistolClip = 15;
    public int MachineGunClip = 30;
    public int ShotgunClip = 7;
    public int SniperClip = 10;
    public int MinigunClip = 100;

    int PistolClipUsed;
    int MachineGunClipUsed;
    int ShotgunClipUsed;
    int SniperClipUsed;
    int MinigunClipUsed;


    [Header("Clips Left")]
    public int PistolClipLeft;
    public int MachineGunClipLeft;
    public int ShotgunClipLeft;
    public int SniperClipLeft;
    public int MinigunClipLeft;


    [Header("Reload Seconds")]
    public float PistolReloadTime = 1;
    public float MachineGunReloadTime = 2;
    public float ShotgunReloadTime = 3;
    public float SniperReloadTime = 4;
    public float MinigunReloadTime = 6;

    [Header("Reload Times Max")]
    public int pistolReloadBarMax = 60;
    // ----- how many seconds(1) * 100 then devide b 2 ( 1 second * 100 == 100 seconds.... then devide by 2)
    public int MachineGunReloadBarMax = 120;
    public int ShotgunReloadBarMax = 150;
    public int SniperReloadBarMax = 200;
    public int MinigunReloadBarMax = 300;

    [Header("General")]
    public float GeneralReloadTime;
    public int GeneralMaxAmmo;
    public int GeneralClip;
    public int GeneralClipUsed;
    public int GeneralClipLeft;
    public GameObject GeneralGUI;
    public GameObject GeneralClipGUI;
    public GameObject GeneralClipMaxGUI;

    [Header("The Rest")]
    public bool reloading;
    public GameObject ReloadGUI;
    public GameObject ReloadGraphic;
    public GameObject XButton;
    public GameObject AButton;
    public GameObject Infinite;

    // Use this for initialization
    void Start()
    {
        //"∞"
		
        PistolClipLeft = PistolClip;
        MachineGunClipLeft = MachineGunClip;
        ShotgunClipLeft = ShotgunClip;
        SniperClipLeft = SniperClip;
        MinigunClipLeft = MinigunClip;
    }
	
    // Update is called once per frame
    void Update()
    {
//        PistolClipUsed = PistolClip - PistolClipLeft;
//        MachineGunClipUsed = MachineGunClip - MachineGunClipLeft;
//        ShotgunClipUsed = ShotgunClip - ShotgunClipLeft;
//        SniperClipUsed = SniperClip - SniperClipLeft;

        if (PistolClipLeft > PistolClip)
        {
            PistolClipLeft = PistolClip;
        }
        if (MachineGunClipLeft > MachineGunClip)
        {
            MachineGunClipLeft = MachineGunClip;
        }
        if (ShotgunClipLeft > ShotgunClip)
        {
            ShotgunClipLeft = ShotgunClip;
        }
        if (SniperClipLeft > SniperClip)
        {
            SniperClipLeft = SniperClip;
        }
        if (MinigunClipLeft > MinigunClip)
        {
            MinigunClipLeft = MinigunClip;
        }

//		ReloadGUI.GetComponent<EnergyBar> ().valueCurrent
//		ReloadGUI.GetComponent<EnergyBar> ().valueMax
        if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Pistol)
        {
            GeneralClip = PistolClip;
            GeneralClipUsed = PistolClipUsed;
            GeneralClipLeft = PistolClipLeft;
            GeneralReloadTime = PistolReloadTime;
            GeneralMaxAmmo = PistolMaxAmmo;
            ReloadGUI.GetComponent<EnergyBar>().valueMax = ((int)PistolReloadTime * 60);
           
        }
        else
        if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.MachineGun)
        {
            GeneralClip = MachineGunClip;
            GeneralClipUsed = MachineGunClipUsed;
            GeneralClipLeft = MachineGunClipLeft;
            GeneralReloadTime = MachineGunReloadTime;
            GeneralMaxAmmo = MachineGunMaxAmmo;
            ReloadGUI.GetComponent<EnergyBar>().valueMax = ((int)MachineGunReloadTime * 60);
         
            
        }
        else
        if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Shotgun)
        {
            GeneralClip = ShotgunClip;
            GeneralClipUsed = ShotgunClipUsed;
            GeneralClipLeft = ShotgunClipLeft;
            GeneralReloadTime = ShotgunReloadTime;
            GeneralMaxAmmo = ShotgunMaxAmmo;
            ReloadGUI.GetComponent<EnergyBar>().valueMax = ((int)ShotgunReloadTime * 60);
           
            
        }
        else
        if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Sniper)
        {
            GeneralClip = SniperClip;
            GeneralClipUsed = SniperClipUsed;
            GeneralClipLeft = SniperClipLeft;
            GeneralReloadTime = SniperReloadTime;
            GeneralMaxAmmo = SniperMaxAmmo;
            ReloadGUI.GetComponent<EnergyBar>().valueMax = ((int)SniperReloadTime * 60);
           
        }
        else
        if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Minigun)
        {
            GeneralClip = MinigunClip;
            GeneralClipUsed = MinigunClipUsed;
            GeneralClipLeft = MinigunClipLeft;
            GeneralReloadTime = MinigunReloadTime;
            GeneralMaxAmmo = MinigunMaxAmmo;
            ReloadGUI.GetComponent<EnergyBar>().valueMax = ((int)MinigunReloadTime * 60);

        }
        

        if (CharacterStats.CS.TypeofWeapon != CharacterStats.Weapon.Fist && CharacterStats.CS.TypeofWeapon != CharacterStats.Weapon.Knife
            && CharacterStats.CS.TypeofWeapon != CharacterStats.Weapon.Pipe && !CharacterStats.CS.Special)
        {
            Infinite.SetActive(false);
            GeneralClipGUI.GetComponent<Text>().text = GeneralClipLeft.ToString();
            GeneralClipMaxGUI.GetComponent<Text>().text = GeneralMaxAmmo.ToString();
            GeneralClipUsed = GeneralClip - GeneralClipLeft; // show this in the HUD
            if (GeneralClipUsed >= 1 && !reloading && GeneralMaxAmmo > 0)
            {
                XButton.SetActive(true);
                ReloadGraphic.SetActive(true);
            }
            else
            {
                XButton.SetActive(false);
                ReloadGraphic.SetActive(false);
            }
            PistolClipUsed = PistolClip - PistolClipLeft;
            MachineGunClipUsed = MachineGunClip - MachineGunClipLeft;
            ShotgunClipUsed = ShotgunClip - ShotgunClipLeft;
            SniperClipUsed = SniperClip - SniperClipLeft;
            MinigunClipUsed = MinigunClip - MinigunClipLeft;
        }
        else
        {
            GeneralClipGUI.GetComponent<Text>().text = "";
            GeneralClipMaxGUI.GetComponent<Text>().text = "";
            Infinite.SetActive(true);
            XButton.SetActive(false);
            ReloadGraphic.SetActive(false);
        }

       
      


        if (!reloading)
        {
            if (GeneralClipLeft <= 0)
            {
                StartReloading();
            }
            reloadTimer = 0;
            ReloadGUI.SetActive(false);
            ReloadGUI.GetComponent<EnergyBar>().valueCurrent= 0;
            if (Tutorial.AllowReload)
            {
                //if (Input.GetButtonUp("X") && GeneralClipUsed > 0 && GeneralMaxAmmo > 0)
                //{ // if pressed x and the clip is smaller than the max
                //    StartCoroutine(Reload());
                //    reloading = true;
                //    ReloadGUI.SetActive(true);
                //    XButton.SetActive(false);
                //}
            }

        }
        else
        {
            //ReloadGUI.GetComponent<EnergyBar>().valueCurrent= ReloadGUI.GetComponent<EnergyBar>().valueMax;
          
            ReloadGUI.SetActive(true);
            if (reloadTimer < ReloadGUI.GetComponent<EnergyBar>().valueMax)
            {
                reloadTimer += 1 * Time.deltaTime * 60;
                ReloadGUI.GetComponent<EnergyBar>().valueCurrent= (int)reloadTimer;
            }
            //ReloadGUI.GetComponent<EnergyBar>().valueCurrent= ReloadGUI.GetComponent<EnergyBar>().valueMax;
        }




    }

     float reloadTimer;

    public void StartReloading()
    {
        if (GeneralClipUsed > 0 && GeneralMaxAmmo > 0)
        { // if pressed x and the clip is smaller than the max
            StartCoroutine(Reload());
            reloading = true;
      
            XButton.SetActive(false);
        }
    }

    public IEnumerator Reload()
    {
        reloading = true;
        // start reloading and show reload HUD
        yield return new WaitForSeconds(GeneralReloadTime);
        if (reloading)
        {
            // reload finished... subtract ammo from gun ammo max to its clip
//			if (GeneralReloadingTime <= 0) {
            if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Pistol)
            {
                if (PistolMaxAmmo >= PistolClipUsed)
                {
                    PistolMaxAmmo -= PistolClipUsed;
                    PistolClipLeft = PistolClip;
                    PistolClipUsed = 0;
                }
                else
                {
                    PistolClipLeft += PistolMaxAmmo;
                    PistolMaxAmmo = 0;
                    PistolClipUsed = 0;
                }
            }
            if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.MachineGun)
            {
                if (MachineGunMaxAmmo >= MachineGunClipUsed)
                {
                    MachineGunMaxAmmo -= MachineGunClipUsed;
                    MachineGunClipLeft = MachineGunClip;
                    MachineGunClipUsed = 0;
//					Debug.Log("Reloading full clip size");
                }
                else
                {
                    MachineGunClipLeft += MachineGunMaxAmmo;
                    MachineGunMaxAmmo = 0;
                    MachineGunClipUsed = 0;
//					Debug.Log("Reloading clip size with what ever is left in the backup");
                }
            }
            if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Shotgun)
            {
                if (ShotgunMaxAmmo >= ShotgunClipUsed)
                {
                    ShotgunMaxAmmo -= ShotgunClipUsed;
                    ShotgunClipLeft = ShotgunClip;
                    ShotgunClipUsed = 0;
                }
                else
                {
                    ShotgunClipLeft += ShotgunMaxAmmo;
                    ShotgunMaxAmmo = 0;
                    ShotgunClipUsed = 0;
                }
            }
            if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Sniper)
            {
                if (SniperMaxAmmo >= SniperClipUsed)
                {
                    SniperMaxAmmo -= SniperClipUsed;
                    SniperClipLeft = SniperClip;
                    SniperClipUsed = 0;
                }
                else
                {
                    SniperClipLeft += SniperMaxAmmo;
                    SniperMaxAmmo = 0;
                    SniperClipUsed = 0;
                }
            }
            if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Minigun)
            {
                if (MinigunMaxAmmo >= MinigunClipUsed)
                {
                    MinigunMaxAmmo -= MinigunClipUsed;
                    MinigunClipLeft = MinigunClip;
                    MinigunClipUsed = 0;
                }
                else
                {
                    MinigunClipLeft += MinigunMaxAmmo;
                    MinigunMaxAmmo = 0;
                    MinigunClipUsed = 0;
                }
            }
//			}
            reloading = false;
//			Debug.Log("Reloading Finished");
            ReloadGUI.SetActive(false);
            ReloadGraphic.SetActive(false);
        }
    }
}
