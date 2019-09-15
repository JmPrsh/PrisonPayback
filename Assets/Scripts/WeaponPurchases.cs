using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPurchases : MonoBehaviour
{

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
        Needle,
        Pills,
        Milk,
        Powder

    };
    public Weapon whichWeapon;

    Transform Player;
    bool trigger;
    GameObject Parent;
    public Button BuyButton;
    public Text CostText;
    public Image weapon;

    // Use this for initialization
    void Start()
    {
        Parent = BuyButton.transform.parent.gameObject;
        Player = CharacterStats.CS.gameObject.transform;
        switch (whichWeapon)
        {
            case Weapon.Knife:
                cost = WeaponPurchasesManager.wpm.knifeCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[0];
                BuyButton.onClick.AddListener(() => BuyWeapon(false,0));
                break;
            case Weapon.Pipe:
                cost = WeaponPurchasesManager.wpm.pipeCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[1];
                BuyButton.onClick.AddListener(() => BuyWeapon(false, 0));
                break;
            case Weapon.Pistol:
                cost = WeaponPurchasesManager.wpm.pistolCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[2];
                BuyButton.onClick.AddListener(() => BuyWeapon(false, 0));
                break;
            case Weapon.Machinegun:
                cost = WeaponPurchasesManager.wpm.machinegunCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[3];
                BuyButton.onClick.AddListener(() => BuyWeapon(false, 0));
                break;
            case Weapon.Shotgun:
                cost = WeaponPurchasesManager.wpm.shotgunCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[4];
                BuyButton.onClick.AddListener(() => BuyWeapon(false, 0));
                break;
            case Weapon.Sniper:
                cost = WeaponPurchasesManager.wpm.sniperCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[5];
                BuyButton.onClick.AddListener(() => BuyWeapon(false, 0));
                break;
            case Weapon.Minigun:
                cost = WeaponPurchasesManager.wpm.minigunCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[6];
                BuyButton.onClick.AddListener(() => BuyWeapon(false, 0));
                break;
            case Weapon.Needle:
                cost = WeaponPurchasesManager.wpm.needleCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[7];
                BuyButton.onClick.AddListener(() => BuyWeapon(true,DPadButtons.Needles));
                break;
            case Weapon.Pills:
                cost = WeaponPurchasesManager.wpm.pillsCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[8];
                BuyButton.onClick.AddListener(() => BuyWeapon(true, DPadButtons.Pills));
                break;
            case Weapon.Milk:
                cost = WeaponPurchasesManager.wpm.milkCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[9];
                BuyButton.onClick.AddListener(() => BuyWeapon(true, DPadButtons.Milk));
                break;
            case Weapon.Powder:
                cost = WeaponPurchasesManager.wpm.powderCost;
                weapon.sprite = WeaponPurchasesManager.wpm.weapons[10];
                BuyButton.onClick.AddListener(() => BuyWeapon(true, DPadButtons.Powder));
                break;
        }
        CostToadd = (cost / 10);
        CostText.text = cost.ToString();
    }

    void Update()
    {
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
    public void BuyWeapon(bool Item,int amt)
    {
        if (CharacterStats.CS.Cash >= cost)
        {
            if (amt <4) {
                switch (whichWeapon)
                {
                    case Weapon.Needle:
                        DPadButtons.Needles++;
                        break;
                    case Weapon.Pills:
                        DPadButtons.Pills++;
                        break;
                    case Weapon.Milk:
                        DPadButtons.Milk++;
                        break;
                    case Weapon.Powder:
                        DPadButtons.Powder++;
                        break;
                }
                CharacterStats.CS.Cash -= (int)cost;
                cost += (int)CostToadd;
                CostText.text = ((int)cost).ToString();
                CheckWeapon(Item);
            }
           
        }
        else
        {
            WaveManager.WM.CashAnim.SetTrigger("Error");
        }
    }



    void CheckWeapon(bool Item)
    {
        switch (whichWeapon)
        {
            case Weapon.Knife:
                if (!CharacterStats.CS.ShowKnife)
                {
                    CharacterStats.CS.KnifePickup();
                }
                this.gameObject.SetActive(false);
                break;
            case Weapon.Pipe:
                if (!CharacterStats.CS.ShowPipe)
                {
                    CharacterStats.CS.PipePickup();
                }
                this.gameObject.SetActive(false);
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
        if (Item)
        {
            WeaponStoreManager.WSM.ShowText("+ Item");
        }
            CharacterStats.CS.UpdateText();
    }
}
