using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPurchases : MonoBehaviour {

    float cost;
    float CostToadd;

    public enum Weapon
    {
        Knife,
        Pipe,
        Pistol,
        Machinegun,
        Shotgun,
        Sniper,
        Minigun,
       
    };
    public Weapon whichWeapon;

    Transform Player;
    bool trigger;
    GameObject Parent;
    public Button BuyButton;
    public Text CostText;
    public Image weapon;

	// Use this for initialization
	void Start () {
        Parent = BuyButton.transform.parent.gameObject;
        BuyButton.onClick.AddListener(() => BuyWeapon());
        Player = CharacterStats.CS.gameObject.transform;
        switch(whichWeapon){
            case Weapon.Knife:
                cost = WeaponPurchasesManager.wpm.knifeCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[0];
                break;
            case Weapon.Pipe:
                cost = WeaponPurchasesManager.wpm.pipeCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[1];
                break;
            case Weapon.Pistol:
                cost = WeaponPurchasesManager.wpm.pistolCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[2];
                break;
            case Weapon.Machinegun:
                cost = WeaponPurchasesManager.wpm.machinegunCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[3];
                break;
            case Weapon.Shotgun:
                cost = WeaponPurchasesManager.wpm.shotgunCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[4];
                break;
            case Weapon.Sniper:
                cost = WeaponPurchasesManager.wpm.sniperCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[5];
                break;
            case Weapon.Minigun:
                cost = WeaponPurchasesManager.wpm.minigunCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[6];
                break;
        }
        CostToadd = (cost / 10);
        CostText.text = cost.ToString();
	}

    void Update(){
//        if(Vector2.Distance(Player.position,transform.position)< 5){
//            if(!Parent.activeInHierarchy){
//                Parent.SetActive(true);
//            }
//        }else{
//            if(Parent.activeInHierarchy){
//                Parent.SetActive(false);
//            }
//        }
    }
	
	// Update is called once per frame
	public void BuyWeapon () {
        if(CharacterStats.CS.Cash >= cost){
            CharacterStats.CS.Cash -= cost;
            cost += CostToadd;
            CostText.text = ((int)cost).ToString();
            CheckWeapon();
        }
	}

    void CheckWeapon(){
        switch(whichWeapon){
            case Weapon.Knife:
                if(!CharacterStats.CS.ShowKnife){
                    CharacterStats.CS.KnifePickup();
                    this.gameObject.SetActive(false);
                }
                break;
            case Weapon.Pipe:
                if(!CharacterStats.CS.ShowPipe){
                    CharacterStats.CS.PipePickup();
                    this.gameObject.SetActive(false);
                }
                break;
            case Weapon.Pistol:
                CharacterStats.CS.PistolPickup(true, false);
                break;
            case Weapon.Machinegun:
                CharacterStats.CS.MachineGunPickup(true, false);
                break;
            case Weapon.Shotgun:
                CharacterStats.CS.ShotgunPickup(true, false);
                break;
            case Weapon.Sniper:
                CharacterStats.CS.SniperPickup(true, false);
                break;
            case Weapon.Minigun:
                CharacterStats.CS.MiniGunPickup(true, false);
                break;
        }
    }
}
