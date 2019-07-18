using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class DPadButtons : MonoBehaviour
{

	
    public enum Dpad
    {
        None,
        Right,
        Left,
        Up,
        Down
    }

    private bool flag = true;
    public Image[] DpadGO;
    public Image[] DpadSelected;

    public enum ItemSelected
    {
        None,
        Powder,
        Needle,
        Pills,
        Milk
    }

    public ItemSelected ItemToUse;
    public int Milk;
    public int Needles;
    public int Pills;
    public int Powder;
    public Text[] text;
    public bool consumed;
    public bool ReadyForNextConsume = true;
    public GameObject TimerGO;
    public int NeedleTimer = 5;
    public int MilkTimer = 6;
    public int PowderTimer = 5;
    public int PillsTimer = 4;
    public int NeedleTimerMax = 250;
    // ----- how many seconds(1) * 100 then devide b 2 ( 1 second * 100 == 100 seconds.... then devide by 2)
    public int MilkTimerMax = 300;
    public int PowderTimerMax = 250;
    public int PillsTimerMax = 200;
    public int GeneralTimer;
    public Transform[] HUDText;
    public int HUDTexty;
    public GameObject TimerNormal;
    public GameObject[] TimerCoolDown;
    EnergyBar EB;
    public EnergyBar[] CoolDownEB;
    bool CoolDown;
    public Color TimerColor;
    public GameObject CantUse;
    public ParticleSystem HealthPlusEffect;
    public Animator HealthBar;
    bool once;
    public Button[] BuffButtons;

    void Start()
    {

        EB = TimerNormal.GetComponent<EnergyBar>();
        CoolDownEB[0] = TimerCoolDown[0].GetComponent<EnergyBar>();
        CoolDownEB[1] = TimerCoolDown[1].GetComponent<EnergyBar>();
        CoolDownEB[2] = TimerCoolDown[2].GetComponent<EnergyBar>();
        CoolDownEB[3] = TimerCoolDown[3].GetComponent<EnergyBar>();
        EB.valueCurrent= EB.valueMax;
        CoolDownEB[0].valueCurrent= CoolDownEB[0].valueMax;
        CoolDownEB[1].valueCurrent= CoolDownEB[1].valueMax;
        CoolDownEB[2].valueCurrent= CoolDownEB[2].valueMax;
        CoolDownEB[3].valueCurrent= CoolDownEB[3].valueMax;
        TimerGO.SetActive(false);
        GeneralTimer = (int)EB.valueMax / 60;
        TimerColor = TimerGO.GetComponent<Image>().color;
        foreach (GameObject GO in TimerCoolDown)
        {
            GO.SetActive(false);
        }

    }

    void Update()
    {

        if (!CharacterStats.CS.Dead)
        {
            TimerGO.GetComponent<Image>().color = TimerColor;
//            PadControl();
            text[0].text = Needles.ToString();
            text[1].text = Pills.ToString();
            text[2].text = Milk.ToString();
            text[3].text = Powder.ToString();
            if (!consumed && ReadyForNextConsume)
            {
//			

            }
            else
            {
                if (EB.valueCurrent> 0)
                {
                    EB.valueCurrent--;
                }
                if (CoolDown)
                {
                    foreach (EnergyBar CD in CoolDownEB)
                    {
//						CD.valueMax = (CD.valueMax*2);
                        if (CD.valueCurrent> 0)
                        {
                            CD.valueCurrent--;
                        }
                       
                    }
                }
               
            }

            if (Tutorial.AllowBuffTimer && Tutorial.isTutorial)
            {
                if (!once)
                {
                    StartCoroutine(Consuming());
                    once = true;
                }
            }

        }
        else
        {
            MeleeWeapon.DamageBoost = 1;
            StaticVariables.MovementMultiply = 1;
            StaticVariables.infStamina = false;
			
            StaticVariables.RegenHealth = false;
//            CS.SpriteGO.GetComponent<Animator>().SetBool("HealthRegen", false);
//            CS.SpriteGO.GetComponent<Animator>().SetBool("SteroidUsed", false);
//            CS.SpriteGO.GetComponent<Animator>().SetBool("SpeedUsed", false);
//            CS.SpriteGO.GetComponent<Animator>().SetBool("StaminaBoost", false);
            //HealthPlusEffect.em = false;
			
//			SteroidEffect.SetActive(false);
        }
        if (Milk < 0)
        {
            Milk = 0;
        }
        if (Needles < 0)
        {
            Needles = 0;
        }
        if (Pills < 0)
        {
            Pills = 0;
        }
        if (Powder < 0)
        {
            Powder = 0;
        }

        if (Milk > 5)
        {
            Milk = 5;
        }
        if (Needles > 5)
        {
            Needles = 5;
        }
        if (Pills > 5)
        {
            Pills = 5;
        }
        if (Powder > 5)
        {
            Powder = 5;
        }
    }

    public void NeedleButton()
    {
        if (!consumed && ReadyForNextConsume)
        {
            if (Needles > 0)
            {

                // play buff effects animation HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("SteroidUsed", true);
                //                  SteroidEffect.SetActive (true);
                MeleeWeapon.DamageBoost = 10;
                GeneralTimer = NeedleTimer;
                EB.valueMax = NeedleTimerMax;
                EB.valueCurrent= EB.valueMax;
                CoolDownEB[0].valueMax = NeedleTimerMax * 3;
                CoolDownEB[0].valueCurrent= CoolDownEB[0].valueMax;
                foreach (GameObject GO in TimerCoolDown)
                {
                    GO.SetActive(false);
                    TimerCoolDown[0].SetActive(true);
                }
                Needles -= 1;
                Transform temp2 = CharacterStats.CS.PickUpGUI[4].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(GameObject.Find("PlayerCanvas").transform);
                temp2.localScale = new Vector2(1, 1);
                StartCoroutine(Consuming());

            }
        }
    }

    public void PillsButtons()
    {
        if (!consumed && ReadyForNextConsume)
        {
            if (Pills > 0)
            {
                CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("HealthRegen", true);
                CharacterStats.CS.SteroidEffect.SetActive(false);
                HealthBar.SetBool("HealthRegen", true);
                //HealthPlusEffect.emission.enabled = true;
                StaticVariables.RegenHealth = true;
                GeneralTimer = PillsTimer;
                EB.valueMax = PillsTimerMax;
                EB.valueCurrent= EB.valueMax;
                CoolDownEB[1].valueMax = PillsTimerMax * 3;
                CoolDownEB[1].valueCurrent= CoolDownEB[1].valueMax;
                foreach (GameObject GO in TimerCoolDown)
                {
                    GO.SetActive(false);
                    TimerCoolDown[1].SetActive(true);
                }
                Pills -= 1;
                Transform temp2 = CharacterStats.CS.PickUpGUI[5].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(GameObject.Find("PlayerCanvas").transform);
                temp2.localScale = new Vector2(1, 1);
                StartCoroutine(Consuming());
            }
        }
    }

    public void MilkButton()
    {
        if (!consumed && ReadyForNextConsume)
        {
            if (Milk > 0)
            {
                // play buff effects animation HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("StaminaBoost", true);
                StaticVariables.infStamina = true;
                GeneralTimer = MilkTimer;
                EB.valueMax = MilkTimerMax;
                EB.valueCurrent= EB.valueMax;
                CoolDownEB[2].valueMax = MilkTimerMax * 3;
                CoolDownEB[2].valueCurrent= CoolDownEB[2].valueMax;
                foreach (GameObject GO in TimerCoolDown)
                {
                    GO.SetActive(false);
                    TimerCoolDown[2].SetActive(true);
                }
                Milk -= 1;
                Transform temp2 = CharacterStats.CS.PickUpGUI[7].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(GameObject.Find("PlayerCanvas").transform);
                temp2.localScale = new Vector2(1, 1);
                StartCoroutine(Consuming());
            }
        }
    }

    public void PowderButton()
    {
        if (!consumed && ReadyForNextConsume)
        {
            if (Powder > 0)
            {
                // play buff effects animation HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("SpeedUsed", true);
                StaticVariables.MovementMultiply = 2;
                GeneralTimer = PowderTimer;
                EB.valueMax = PowderTimerMax;
                EB.valueCurrent= EB.valueMax;
                CoolDownEB[3].valueMax = PowderTimerMax * 3;
                CoolDownEB[3].valueCurrent= CoolDownEB[3].valueMax;
                foreach (GameObject GO in TimerCoolDown)
                {
                    GO.SetActive(false);
                    TimerCoolDown[3].SetActive(true);
                }
                Powder -= 1;
                Transform temp2 = CharacterStats.CS.PickUpGUI[6].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(GameObject.Find("PlayerCanvas").transform);
                temp2.localScale = new Vector2(1, 1);
                StartCoroutine(Consuming());
            }
        }
    }


    public static bool isTutorial;

    void PadControl()
    {
        if (!consumed && ReadyForNextConsume && !isTutorial)
        {
//			Debug.Log ("VerticalAxis " + Input.GetAxis ("Xbox360ControllerDPadY"));
//			Debug.Log ("HorizontalAxis " + Input.GetAxis ("Xbox360ControllerDPadX"));
		
            if (Input.GetAxis("Xbox360ControllerDPadX") == 0.0f && Input.GetAxis("Xbox360ControllerDPadY") == 0.0f)
            {

                flag = true;
                foreach (Image i in DpadGO)
                {
                    i.GetComponent<Image>().enabled = false;
                }
            }
		
            if (Tutorial.AllowBuffs)
            {
                if (Input.GetAxis("Xbox360ControllerDPadX") > 0f && flag)
                {
                    StartCoroutine("DpadControl", Dpad.Right);
                }
                else if (Input.GetAxis("Xbox360ControllerDPadX") < 0f && flag)
                {
                    StartCoroutine("DpadControl", Dpad.Left);
                }
                else if (Input.GetAxis("Xbox360ControllerDPadY") < 0f && flag)
                {
                    StartCoroutine("DpadControl", Dpad.Down);
			
                }
            }

            if (Tutorial.AllowNeedle)
            {
                if (Input.GetAxis("Xbox360ControllerDPadY") > 0f && flag)
                {

                    StartCoroutine("DpadControl", Dpad.Up);
                }
            }
        }
    }

    IEnumerator Consuming()
    {
        Stats.Buffs += 1;
        CantUse.SetActive(true);
        for(int i = 0; i < BuffButtons.Length;i++){
            BuffButtons[i].interactable = false;
        }
//        foreach (Image i in DpadSelected)
//        {
//            i.color = new Color(1, 1, 1, 0f);
//        }
        TimerColor = new Color(TimerColor.r, TimerColor.g, TimerColor.b, 1f);
        consumed = true;
        CoolDown = true;
        ReadyForNextConsume = false;
        TimerGO.SetActive(true);
        TimerNormal.SetActive(true);

        yield return new WaitForSeconds(GeneralTimer);
        // END buff effects animation HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        MeleeWeapon.DamageBoost = 1;
        StaticVariables.MovementMultiply = 1;
        StaticVariables.infStamina = false;

        StaticVariables.RegenHealth = false;
        CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("HealthRegen", false);
        CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("SteroidUsed", false);
        CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("SpeedUsed", false);
        CharacterStats.CS.SpriteGO.GetComponent<Animator>().SetBool("StaminaBoost", false);
        //HealthPlusEffect.emit = false;
        HealthBar.SetBool("HealthRegen", false);
//		SteroidEffect.SetActive(false);



		
//		EB.valueCurrent= EB.valueMax;
        TimerColor = new Color(TimerColor.r, TimerColor.g, TimerColor.b, 0f);
        TimerNormal.SetActive(false);
        consumed = false;
//		CoolDown = true;
        yield return new WaitForSeconds(GeneralTimer * 2);
        TimerColor = new Color(TimerColor.r, TimerColor.g, TimerColor.b, 1f);
        ReadyForNextConsume = true;
        CoolDown = false;
        TimerGO.SetActive(false);
        TimerNormal.SetActive(true);
        foreach (GameObject GO in TimerCoolDown)
        {
            GO.SetActive(false);
        }
        foreach (Image i in DpadSelected)
        {
            i.color = new Color(1, 1, 1, 1f);
        }
        CantUse.SetActive(false);
        for(int i = 0; i < BuffButtons.Length;i++){
            BuffButtons[i].interactable = true;
        }
    }

    IEnumerator DpadControl(Dpad value)
    {
        flag = false;
        yield return null; // delay it as you wish 
        if (value == Dpad.Right)
        {
            Debug.Log("Holding Right on DPad");
            ItemToUse = ItemSelected.Powder;
            foreach (Image i in DpadSelected)
            {
                i.GetComponent<Image>().enabled = false;
            }
            DpadSelected[1].enabled = true;
            DpadGO[1].enabled = true;
        }
        if (value== Dpad.Left)
        {
            Debug.Log("Holding Left on DPad");
            ItemToUse = ItemSelected.Pills;
            foreach (Image i in DpadSelected)
            {
                i.GetComponent<Image>().enabled = false;
            }
            DpadSelected[3].enabled = true;
            DpadGO[3].enabled = true;
        }
        if (value == Dpad.Up)
        {
            Debug.Log("Holding Up on DPad");
            ItemToUse = ItemSelected.Needle;
            foreach (Image i in DpadSelected)
            {
                i.GetComponent<Image>().enabled = false;
            }
            DpadSelected[0].enabled = true;
            DpadGO[0].enabled = true;
        }
        if (value == Dpad.Down)
        {
            Debug.Log("Holding Down on DPad");
            ItemToUse = ItemSelected.Milk;
            foreach (Image i in DpadSelected)
            {
                i.GetComponent<Image>().enabled = false;
            }
            DpadSelected[2].enabled = true;
            DpadGO[2].enabled = true;
        }
		
        StopCoroutine("DpadControl");
    }
	

}