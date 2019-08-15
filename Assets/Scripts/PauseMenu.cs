using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;

//using EnergyBarToolkit;

public class PauseMenu : MonoBehaviour
{

    AmmoScript Ammo;

    public Text Health;
    public Text HealthPlayerHUD, Score, ScorePlayerHUD, Damage, FireRate, ReloadTime, Range, AmmoCost, AmmoCapacity, Pills, Milk, Needles, Powder, PillsPlayerHUD, MilkPlayerHUD, NeedlesPlayerHUD, PowderPlayerHUD;

    public GameObject Pause;
    public GameObject PlayerHUD;
    public static bool isPaused;
    public GameObject[] PauseButtons;
    Vector3 OriginalButtonSize;
    Vector3 BigButtonSize;
    public Image Character;
    public Text CharactersName;
    public Text WeaponName;
    public Sprite[] CharacterSprites;
    public string[] CharacterNames;
    public string[] WeaponNames;
    public bool pressed;
    public GameObject EnemyCanvas;
    public Image WeaponSprite;
    public Image PlayerHUDWeapon;
    public bool aboutToLeave;
    bool restartPressed;
    bool quitPressed;
    bool showMap;
    public GameObject[] PauseScreens;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
        pressed = false;
        Ammo = GameObject.FindGameObjectWithTag("Player").GetComponent<AmmoScript>();
        OriginalButtonSize = PauseButtons[0].transform.parent.localScale;
        BigButtonSize = PauseButtons[0].transform.parent.localScale * 1.1f;
        updateVariables();
    }

    void updateVariables()
    {
        LevelToLoad = leveltoload;
        //		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
        Health.text = HealthPlayerHUD.text;
        Score.text = ScorePlayerHUD.text;
        Damage.text = CharacterStats.CS.DamageToShow;
        FireRate.text = CharacterStats.CS.shotInterval.ToString();
        ReloadTime.text = Ammo.GeneralReloadTime.ToString();
        AmmoCost.text = "1";
        AmmoCapacity.text = Ammo.GeneralMaxAmmo.ToString();

        Pills.text = PillsPlayerHUD.text;
        Milk.text = MilkPlayerHUD.text;
        Needles.text = NeedlesPlayerHUD.text;
        Powder.text = PowderPlayerHUD.text;

        int CSSpriteID = PlayerPrefs.GetInt("SGLIB_CURRENT_CHARACTER");
        int CSWeaponID = CharacterStats.WeaponID;
        Character.sprite = CharacterSprites[CSSpriteID];
        CharactersName.text = CharacterNames[CSSpriteID];
        WeaponName.text = WeaponNames[CSWeaponID];
        // PlayerHUDWeapon = CharacterStats.CS.WeaponGUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterStats.CS.WeaponGUI)
            WeaponSprite.sprite = CharacterStats.CS.WeaponGUI.sprite;// PlayerHUDWeapon.sprite;
        if (isPaused)
        {
            EnemyCanvas.SetActive(false);
            Pause.SetActive(true);
        }
        else
        {

            if (!showMap)
            {
                Time.timeScale = 1;
                EnemyCanvas.SetActive(true);
                Pause.SetActive(false);
                if (!Tutorial.isTutorial)
                {
                    PlayerHUD.SetActive(true);
                }
                pressed = false;

            }
        }


    }

    public void PauseButtonUI()
    {
        if (!isPaused && !showMap)
        {
            updateVariables();
            Invoke("stopTime", 0.6f);
        }
        isPaused = !isPaused;

    }

    IEnumerator PressedBool()
    {
        pressed = true;
        yield return new WaitForSeconds(0.2f);
        pressed = false;

    }

    public void LeftSideButton(int ID)
    {
        ShowScreens(PauseScreens[ID]);
        GetComponent<AudioSource>().clip = Menu.staticPressedsound;
        GetComponent<AudioSource>().Play();
        switch (ID)
        {
            case 0: // resume
                isPaused = false;
                break;
            case 1: // restart
                Time.timeScale = 1;
                aboutToLeave = true;
                restartPressed = true;
                quitPressed = false;
                break;
            case 2: // achievements
            case 3: // controls
            case 4: // settings
            case 5: // quit
                quitPressed = true;
                restartPressed = false;
                Time.timeScale = 1;
                aboutToLeave = true;
                break;

        }
    }

    void ShowScreens(GameObject screenToShow)
    {
        foreach (var item in PauseScreens)
        {
            item.SetActive(item == screenToShow);
        }
    }

    public void stopTime()
    {
        Time.timeScale = 0;

    }

    public static string LevelToLoad;
    public string leveltoload;

    public void Yes()
    {
        if (!pressed)
        {
            GetComponent<AudioSource>().clip = Menu.staticPressedsound;
            GetComponent<AudioSource>().Play();
            Time.timeScale = 1;
            if (restartPressed)
            {

                PlayerPrefs.SetString("LevelToLoad", Application.loadedLevelName);
            }
            if (quitPressed)
            {
                PlayerPrefs.SetString("LevelToLoad", "Main");
            }
            //			GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
            Invoke("LoadScene", 2);

        }

    }

    public void No()
    {
        aboutToLeave = false;
        GetComponent<AudioSource>().clip = Menu.staticPressedsound;
        GetComponent<AudioSource>().Play();
        ShowScreens(PauseScreens[0]);
    }

    public void LoadScene()
    {
        Application.LoadLevel("LoadingScreen");
    }

    public void HighlightedButton()
    {
        GetComponent<AudioSource>().clip = Menu.staticHighlightsound;
        GetComponent<AudioSource>().Play();
    }


}
