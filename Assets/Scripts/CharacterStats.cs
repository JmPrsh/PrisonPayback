using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.ImageEffects;
using EasyButtons;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats CS;
    public Player PlayerSettings;
    public float Health;
    float startingHealth;
    public float Damage;
    [HideInInspector]
    public float HealthStarting;
    public GameObject SteroidEffect;
    public Transform hittext;
    public Transform coinText;
    // Ammo Script
    [HideInInspector]
    public AmmoScript ammo;
    public float shotInterval = 0.5f;
    private float shootTime = 0.0f;
    public Transform bulletPrefab;
    GameObject bulletSpawn;
    public AudioClip[] fire;
    public AudioClip[] impact;

    [HideInInspector]
    public bool ShowMinigun, ShowSniper, ShowShotgun, ShowMachineGun, ShowPistol, ShowKnife, ShowPipe;
    [HideInInspector]
    public Animator anim, animGun;
    public Animator[] animMuzzle;
    [HideInInspector]
    public PlayerModel playermodel;
    Rigidbody2D r;
    [HideInInspector]
    public bool Dead;
    [HideInInspector]
    public SpriteRenderer SpriteGORenderer;
    float screenX;
    float screenY;
    // which weapon are we holding
    public enum Weapon
    {
        Fist,
        Pipe,
        Knife,
        Pistol,
        MachineGun,
        Shotgun,
        Sniper,
        Minigun,
        ZombieHand,
        Cannon,
        Revolver,
        PistolAgent,
        Sword
    };
    // [HideInInspector]
    public Weapon TypeofWeapon;

    public Image[] WeaponUIParent;
    public GameObject RevolverSecondGun;
    public GameObject WeaponSwitches;
    public int WhichWeapon;
    public List<GameObject> Weapons;
    public GameObject[] Controls;
    public float Stamina;
    EnergyBar LivesGOEB;
    int Lives;
    // Dodge
    public Transform DodgeHorizontal;
    // Sprites
    public Sprite[] CharacterSprites;
    public Transform AmmoCollectedText;
    public Transform HealthCollectedText;
    public Transform ScoreCollectedText;
    public Transform CoinPrefab;

    public enum Ethnic
    {
        White,
        Hispanic,
        Brown,
        DarkerBrown
    };
    [HideInInspector]
    public Ethnic EthnicCharacter;
    public Sprite[] CurrentWeapon;
    public Image WeaponGUI;
    [HideInInspector]
    public bool Blur;
    public RectTransform[] PickUpGUI;
    public Animator[] WeaponShoot;
    [HideInInspector]
    public Animator WSanim, GeneralMuzzle;
    public Transform[] BulletCasing;
    int chosenCasing;
    public float bursttimer = 0.2f;
    bool shootBurst;
    int shotsfired;
    GameObject Shadow;
    bool showgrey;
    public GameObject AimingGO, StaminaGO, LivesGO, ScoreGO, SpriteGO, DyingGO, BloodSplat, BloodPool, ComboGO, PlayerHUD, EnemyCanvas, MainPauseParent;
    public static bool flipped;
    [HideInInspector]
    public float greyAmount;
    float CharacterAngle;
    [HideInInspector]
    public DPadButtons DPB;
    public static int CharacterSpriteID;
    public static int Score;
    public float Cash;
    public Text CashText;
    public static int Combo;
    int highestCombo;
    [HideInInspector]
    public float HighScoreNormal;
    [HideInInspector]
    public float HighScoreZombie;
    [HideInInspector]
    public float ComboTimer;
    public Text ScoreText;
    public Text ComboText;
    public EnergyBar ComboTimerEB;
    public EnergyBar HealthBar;
    bool showkillstreaks;
    public Transform KillStreakGO;
    public Sprite[] Killstreaks;
    public Color OrangeColour;
    public Text ComboParent;
    CanvasGroup cg;
    [HideInInspector]
    public string DamageToShow;
    public static int WeaponID;

    public static bool allowMovement;
    public static bool Finished;
    public static string ScoringSceneLevelToLoad;
    public string ScoringSceneLevelToLoadForLoadingScreen;
    [Space]
    public bool stunned;
    public GameObject ShockScreen;
    public float StunDelay;
    [Space]
    public float evadeTimer;
    // this tells us how far player will evade
    public float cooldownTimer;
    [HideInInspector]
    public bool evading;
    public Transform ImpactSound;
    public static float timeplayed;
    [HideInInspector]
    public Transform PlayerCanvas;
    public SpriteRenderer Aiming;
    public Button DodgeButton;
    int tempAmmoCount;
    public int ControlType;
    float bulletLife;
    public static float xAmount;
    public float MoveForce;
    [HideInInspector]
    public Vector2 vel;
    public float maxVel;

    float smokeTimer;
    [HideInInspector]
    public bool Special, zombie, cyborg, gunslinger, agent, samurai;

    public EnergyBar Shield;

    public static float EnemyDelayTimer;

    void Awake()
    {
        CS = this;
        r = GetComponent<Rigidbody2D>();
        HighScoreNormal = PlayerPrefs.GetFloat("HighScoreNormal");
        HighScoreZombie = PlayerPrefs.GetFloat("HighScoreZombie");

        CharacterSpriteID = PlayerPrefs.GetInt("SGLIB_CURRENT_CHARACTER");

        anim = GetComponent<Animator>();
        animGun = AimingGO.GetComponent<Animator>();

        for (int i = 0; i < AimingGO.transform.GetChild(0).childCount - 1; i++)
        {
            Weapons.Add(AimingGO.transform.GetChild(0).GetChild(i).gameObject);
        }

        playermodel = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerModel>();
        ammo = GetComponent<AmmoScript>();
        Music.whichMusic = 2;
        Music.playMusic = true;
        cg = PlayerHUD.GetComponent<CanvasGroup>();
        SpriteGORenderer = SpriteGO.GetComponent<SpriteRenderer>();
        LivesGOEB = LivesGO.GetComponent<EnergyBar>();
        PlayerCanvas = GameObject.Find("PlayerCanvas").transform;

        ControlChoice(PlayerPrefs.GetInt("Controls", ControlType));

    }

    void Start()
    {
        Score = 0;
        Cash = 0;

        Stats.Kills = 0;
        Stats.Rooms = 0;
        Stats.Buffs = 0;
        Stats.Combo = 0;
        Stats.Criticals = 0;
        Stats.TimePlayed = 0;
        timeplayed = 0;

        screenX = (Screen.width * 2) / 100;
        screenY = (Screen.height * 2) / 100;

        Finished = false;
        // 270
        AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
        SoundFXController.Check = true;

        Invoke("AllowToMove", 2);

        allowMovement = false;
        HealthStarting = Health;
        startingHealth = Health;
        playermodel.TimeLeft = 100;
        playermodel.timePlayed = 0;
        playermodel.Health = 100;
        Dead = false;
        playermodel.FindPlayer();
        playermodel.EnemySpawnAmount = PlayerPrefs.GetInt("SpawnAmount");
        playermodel.EnemyAdd = 0;
        Lives = 1;
        //		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");
        StartCoroutine(IncrementStamina());
        Shadow = GameObject.FindGameObjectWithTag("shadow");
        DPB = GetComponent<DPadButtons>();
        ComboTimerEB.valueMax = 40;
        cg.alpha = 1;

        ScoringSceneLevelToLoad = ScoringSceneLevelToLoadForLoadingScreen;
        UpdateText();
        CheckSpecial();

    }

    void CheckSpecial()
    {
        Special = CharacterSpriteID == (Mathf.Clamp(CharacterSpriteID, 40, 44));
        if (Special)
        {
            WeaponSwitches.SetActive(false);
            // check switch for what special and decide what type of weapon they are using
            switch (CharacterSpriteID)
            {

                case 40:
                    WhichWeapon = 8;
                    Aiming.transform.localPosition = new Vector3(0, 0.3f, 0);
                    zombie = true; // done
                    break;
                case 41:
                    WhichWeapon = 9;
                    Aiming.transform.localPosition = new Vector3(0, 1.5f, 0);
                    cyborg = true; // done
                    Shield.gameObject.SetActive(true);
                    break;
                case 42:
                    WhichWeapon = 10;
                    Aiming.transform.localPosition = new Vector3(0, 1.2f, 0);
                    gunslinger = true;
                    break;
                case 43:
                    WhichWeapon = 11;
                    Aiming.transform.localPosition = new Vector3(0, 1.5f, 0);
                    agent = true;
                    break;
                case 44:
                    WhichWeapon = 12;
                    Aiming.transform.localPosition = new Vector3(0, 0.3f, 0);
                    samurai = true;
                    break;

            }
            WeaponManager.WM.SetCurrentWeaponIndex(WhichWeapon);
        }
    }

    void AllowToMove()
    {
        allowMovement = true;
    }

    void FixedUpdate()
    {
        UpdateText();
        HealthBar.valueCurrent = (int)Health;
        HealthBar.valueMax = (int)HealthStarting;

        if (EnemyDelayTimer > 1)
            EnemyDelayTimer = 0;
        else
            EnemyDelayTimer += Time.deltaTime;

        if (PauseMenu.isPaused)
            return;

        RegenerateHealth();

        // ?
        isDead(Dead);

        ComboHandler();

        EnemyCanvas.SetActive(!Dead);

        PlayerHUD.SetActive(!Blur);

        if (!bulletSpawn)
            bulletSpawn = GameObject.FindGameObjectWithTag("bulletSpawn");

        if (Health <= 0)
            StartCoroutine(Death());

    }

    void isDead(bool yes)
    {
        if (yes)
        {
            SpriteGORenderer.material.SetFloat("_GrayScale", greyAmount);
            DyingGO.transform.rotation = Quaternion.AngleAxis(CharacterAngle - 90, Vector3.forward);
            if (showgrey)
            {
                if (greyAmount > 0)
                    greyAmount -= 1 * Time.deltaTime * 2;
            }
            else
            {
                if (greyAmount < 1)
                    greyAmount += 1 * Time.deltaTime * 2;
            }

            EnemyCanvas.SetActive(false);
            if (!flipped)
            {
                if (CharacterAngle < 90)
                {
                    CharacterAngle += 90 * Time.deltaTime * 2;
                }
                else
                {
                    BloodPool.SetActive(true);
                }
            }
            else
            {
                if (CharacterAngle > -90)
                {
                    CharacterAngle -= 90 * Time.deltaTime * 2;
                }
                else
                {
                    BloodPool.SetActive(true);
                }
            }
        }
        else
        {
            SpriteGORenderer.sprite = CharacterSprites[CharacterSpriteID];
            if (!stunned)
            {
                ProcessEvasion();
                MoveForward(); // Player Movement
                timeplayed += Time.deltaTime;
            }

            if (StunDelay > 0)
                StunDelay -= Time.deltaTime;

            if (Time.timeScale < 1)
                Time.timeScale += 1f * Time.deltaTime / 4;

            if (TypeofWeapon != Weapon.Fist && TypeofWeapon != Weapon.Pipe && TypeofWeapon != Weapon.Knife && TypeofWeapon != Weapon.ZombieHand && TypeofWeapon != Weapon.Sword)
            {
                ShootingHandler();
            }
            else
            {

                if (Weapons[WeaponID].GetComponent<MeleeWeapon>())
                    Weapons[WeaponID].GetComponent<MeleeWeapon>().Attack();

            }

            CheckSpecial();



            PlayerModelHandler();

            LivesGOEB.valueCurrent = Lives * 33;

            if (Health < HealthStarting / 7)
            {
                SteroidEffect.SetActive(true);
            }

            if (shootBurst)
            {
                BurstFireHandler();
            }
        }
    }

    public void UpdateWeapon(int i)
    {
        WhichWeapon = i;
        TypeOfWeaponHandler();
    }

    public void UpdateText()
    {
        ScoreText.text = Score.ToString("000000");
        CashText.text = Cash.ToString();
    }

    void ComboHandler()
    {
        ComboText.text = Combo.ToString();

        if (Combo > highestCombo)
        {
            highestCombo = Combo;
        }
        Stats.Combo = highestCombo;

        ComboTimerEB.valueCurrent = (int)ComboTimer;

        ComboGO.SetActive(Combo > 1);

        if (ComboTimer > 0)
        {

            if (Tutorial.AllowComboCountdown)
            {
                ComboTimer -= 10 * Time.deltaTime;

            }
        }
        else
        {
            Combo = 0;
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
            ComboGO.SetActive(false);
        }

        if (Combo == 4 && !showkillstreaks)
        {
            Transform temp = KillStreakGO.Spawn(transform.position, transform.rotation) as Transform;
            temp.GetComponentInChildren<Image>().sprite = Killstreaks[0];
            showkillstreaks = true;
            ComboText.color = OrangeColour;
            ComboParent.color = OrangeColour;
            //conviction
        }
        if (Combo == 5)
        {
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
        }
        if (Combo == 8 && !showkillstreaks)
        {
            Transform temp = KillStreakGO.Spawn(transform.position, transform.rotation) as Transform;
            temp.GetComponentInChildren<Image>().sprite = Killstreaks[1];
            showkillstreaks = true;
            ComboText.color = OrangeColour;
            ComboParent.color = OrangeColour;
            //mayhem
        }
        if (Combo == 9)
        {
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
        }
        if (Combo == 12 && !showkillstreaks)
        {
            Transform temp = KillStreakGO.Spawn(transform.position, transform.rotation) as Transform;
            temp.GetComponentInChildren<Image>().sprite = Killstreaks[2];
            showkillstreaks = true;
            ComboText.color = OrangeColour;
            ComboParent.color = OrangeColour;
            //manslaughter
        }
        if (Combo == 13)
        {
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
        }
        if (Combo == 16 && !showkillstreaks)
        {
            Transform temp = KillStreakGO.Spawn(transform.position, transform.rotation) as Transform;
            temp.GetComponentInChildren<Image>().sprite = Killstreaks[3];
            showkillstreaks = true;
            ComboText.color = OrangeColour;
            ComboParent.color = OrangeColour;
            //bloodbath
        }
        if (Combo == 17)
        {
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
        }
        if (Combo == 20 && !showkillstreaks)
        {
            Transform temp = KillStreakGO.Spawn(transform.position, transform.rotation) as Transform;
            temp.GetComponentInChildren<Image>().sprite = Killstreaks[4];
            showkillstreaks = true;
            ComboText.color = OrangeColour;
            ComboParent.color = OrangeColour;
            //massacre
        }
        if (Combo == 21)
        {
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
        }
        if (Combo == 25 && !showkillstreaks)
        {
            Transform temp = KillStreakGO.Spawn(transform.position, transform.rotation) as Transform;
            temp.GetComponentInChildren<Image>().sprite = Killstreaks[5];
            showkillstreaks = true;
            ComboText.color = OrangeColour;
            ComboParent.color = OrangeColour;
            //are you fucking serious
        }
        if (Combo == 26)
        {
            ComboText.color = Color.white;
            ComboParent.color = Color.white;
            showkillstreaks = false;
        }
    }


    void PlayerModelHandler()
    {
        if (playermodel.EnemySpawnAmount >= 35)
        {
            playermodel.EnemySpawnAmount = 35;
        }
        if (playermodel.EnemySpawnAmount <= 2)
        {
            playermodel.EnemySpawnAmount = 2;
        }

        if (playermodel.Health >= 100)
        {
            playermodel.Health = 100;
        }
    }

    void BurstFireHandler()
    {
        if (shotsfired < 3 && ammo.MachineGunClipLeft > 0)
        {
            if (bursttimer > 0)
            {
                bursttimer -= 2f * Time.deltaTime;
            }
            if (bursttimer <= 0)
            {
                Transform temp = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward));
                temp.GetComponent<bullet>().damage = Damage;
                temp.GetComponent<bullet>().DeathPosition = Aiming.transform.position;

                if (StaticVariables.MovementMultiply == 1)
                {
                    shootTime = Time.time + shotInterval;
                }
                else
                {
                    shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                }

                playermodel.bulletsUsed += 1;
                ammo.MachineGunClipLeft -= 1;

                WSanim.SetTrigger("Shoot");
                GeneralMuzzle.SetTrigger("Shoot");

                BulletCasing[chosenCasing].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                shotsfired += 1;
                bursttimer = 0.2f;

            }
        }
        else
        {
            shotsfired = 0;
            shootBurst = false;
        }
    }

    float ShieldRegenDelay;
    void RegenerateHealth()
    {
        if (StaticVariables.RegenHealth && Health < startingHealth || zombie)
        {
            if (Health < HealthStarting)
            {
                Health += 1;
            }
            else
            {
                Health = startingHealth;
            }
        }

        if (Shield.valueCurrent < Shield.valueMax)
        {
            if (ShieldRegenDelay <= 0)
            {
                Shield.valueCurrent++;
            }
            else
            {
                ShieldRegenDelay -= Time.deltaTime;
            }
        }
    }

    [Button]
    public void Stun()
    {
        if (StunDelay > 0)
            return;
        stunned = true;
        anim.SetBool("Stunned", true);
        StartCoroutine(ShowShock(ShockScreen));
        StunDelay = 4;
    }

    IEnumerator ShowShock(GameObject shockscreen)
    {
        shockscreen.SetActive(true);
        Invoke("RemoveStun", 2);
        yield return new WaitForSeconds(1);
        shockscreen.SetActive(false);
    }

    void TypeOfWeaponHandler()
    {
        WeaponChangeHandler();

        // if type of weapon:
        if (TypeofWeapon == Weapon.Fist)
        {
            WeaponID = 0;
            DamageToShow = "5 - 15";
            Damage = Random.Range(5, 15);
            Aiming.transform.localPosition = new Vector3(0, 0.3f, 0);
        }
        if (TypeofWeapon == Weapon.Pipe)
        {
            WeaponID = 1;
            DamageToShow = "10 - 15";
            Damage = Random.Range(10, 15);
            Aiming.transform.localPosition = new Vector3(0, 0.3f, 0);
        }
        if (TypeofWeapon == Weapon.Knife)
        {
            WeaponID = 2;
            DamageToShow = "15 - 20";
            Damage = Random.Range(15, 20);
            Aiming.transform.localPosition = new Vector3(0, 0.3f, 0);
        }

        if (TypeofWeapon == Weapon.Pistol)
        {
            chosenCasing = 0;
            WeaponID = 3;
            shotInterval = 0.5f;
            DamageToShow = "15 - 20";
            Damage = Random.Range(15, 20);
            WSanim = WeaponShoot[0];
            GeneralMuzzle = animMuzzle[3];
            Aiming.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        if (TypeofWeapon == Weapon.MachineGun)
        {
            chosenCasing = 1;
            WeaponID = 4;
            shotInterval = 0.3f;
            DamageToShow = "15 - 25";
            Damage = Random.Range(15, 25);
            WSanim = WeaponShoot[1];
            GeneralMuzzle = animMuzzle[4];
            Aiming.transform.localPosition = new Vector3(0, 2f, 0);
        }
        if (TypeofWeapon == Weapon.Shotgun)
        {
            chosenCasing = 2;
            WeaponID = 5;
            shotInterval = 1.0f;
            DamageToShow = "25 - 30";
            Damage = Random.Range(25, 30);
            WSanim = WeaponShoot[2];
            GeneralMuzzle = animMuzzle[5];
            Aiming.transform.localPosition = new Vector3(0, 1.25f, 0);
        }
        if (TypeofWeapon == Weapon.Sniper)
        {
            chosenCasing = 3;
            WeaponID = 6;
            shotInterval = 1.0f;
            DamageToShow = "40 - 50";
            Damage = Random.Range(40, 50);
            WSanim = WeaponShoot[3];
            GeneralMuzzle = animMuzzle[6];
            Aiming.transform.localPosition = new Vector3(0, 3f, 0);
        }
        if (TypeofWeapon == Weapon.Minigun)
        {
            chosenCasing = 4;
            WeaponID = 7;
            shotInterval = 0.05f;
            DamageToShow = "15 - 20";
            Damage = Random.Range(15, 20);
            WSanim = WeaponShoot[4];
            GeneralMuzzle = animMuzzle[7];
            Aiming.transform.localPosition = new Vector3(0, 1.8f, 0);
        }
        if (TypeofWeapon == Weapon.ZombieHand)
        {
            WeaponID = 8;
            DamageToShow = "15 - 20";
            Damage = Random.Range(15, 20);
        }
        if (TypeofWeapon == Weapon.Cannon)
        {
            chosenCasing = 5;
            WeaponID = 9;
            shotInterval = 1f;
            DamageToShow = "50 - 60";
            Damage = Random.Range(50, 60);
            WSanim = WeaponShoot[5];
            GeneralMuzzle = animMuzzle[8];
        }
        if (TypeofWeapon == Weapon.Revolver)
        {
            chosenCasing = 6;
            WeaponID = 10;
            shotInterval = 0.3f;
            DamageToShow = "30 - 60";
            Damage = Random.Range(30, 60);
            WSanim = WeaponShoot[6];
            GeneralMuzzle = animMuzzle[9];
            RevolverSecondGun.SetActive(true);
        }
        if (TypeofWeapon == Weapon.PistolAgent)
        {
            chosenCasing = 7;
            WeaponID = 11;
            shotInterval = 0.7f;
            DamageToShow = "40 - 60";
            Damage = Random.Range(40, 60);
            WSanim = WeaponShoot[7];
            GeneralMuzzle = animMuzzle[10];
        }
        if (TypeofWeapon == Weapon.Sword)
        {
            WeaponID = 12;
            DamageToShow = "30 - 40"; // add critical hit chance
            Damage = Random.Range(30, 40);
        }

        WeaponGUI = WeaponUIParent[WeaponID];
        // WeaponGUI.sprite = CurrentWeapon[WeaponID];
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (i != WeaponID)
            {
                if (Weapons[i].activeInHierarchy)
                    Weapons[i].SetActive(false);

                if (WeaponUIParent[i].gameObject.activeInHierarchy)
                    WeaponUIParent[i].gameObject.SetActive(false);

            }
            if (!Weapons[WeaponID].activeInHierarchy)
                Weapons[WeaponID].SetActive(true);
            if (!WeaponUIParent[WeaponID].gameObject.activeInHierarchy)
                WeaponUIParent[WeaponID].gameObject.SetActive(true);
        }


    }


    public void KnifePickup()
    {
        if (!ShowKnife)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Knife";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            ShowKnife = true;
            Weapons[2].GetComponent<PickedUpCheck>().pickedUp = true;
            WeaponManager.WM.SetCurrentWeaponIndex(2);
            WeaponStoreManager.WSM.ShowText("+ Knife");
        }
    }

    public void PipePickup()
    {
        if (!ShowPipe)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Pipe";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            ShowPipe = true;
            Weapons[1].GetComponent<PickedUpCheck>().pickedUp = true;
            WeaponManager.WM.SetCurrentWeaponIndex(1);
            WeaponStoreManager.WSM.ShowText("+ Pipe");
        }
    }

    public void PistolPickup(bool purchased, bool pickup)
    {
        if (!purchased)
        {
            ammo.PistolClipLeft += Random.Range(2, 15);
        }
        else
        {
            ammo.PistolClipLeft += 15;
        }
        if (!pickup)
        {
            tempAmmoCount = 150;
        }
        else
        {
            tempAmmoCount = 15;
        }
        ammo.PistolMaxAmmo += tempAmmoCount;
        if (!ShowPistol)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Pistol";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //                    WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[3].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowPistol = true;
            WeaponManager.WM.SetCurrentWeaponIndex(3);
            WeaponStoreManager.WSM.ShowText("+ Pistol");
        }
        else
        {
            Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
            Destroy(temp.gameObject, 1);
            Transform temp2 = PickUpGUI[0].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp2.SetParent(PlayerCanvas);
            temp.SetParent(PlayerCanvas);
            temp2.localScale = new Vector2(1, 1);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            WeaponStoreManager.WSM.ShowText("+ Pistol Ammo");
        }
    }

    public void MachineGunPickup(bool purchased, bool pickup)
    {
        if (!purchased)
        {
            ammo.MachineGunClipLeft += Random.Range(5, 30);
        }
        else
        {
            ammo.MachineGunClipLeft += 30;
        }
        if (!pickup)
        {
            tempAmmoCount = 100;
        }
        else
        {
            tempAmmoCount = 45;
        }
        ammo.MachineGunMaxAmmo += tempAmmoCount;
        if (!ShowMachineGun)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Assault Rifle";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[4].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowMachineGun = true;
            WeaponManager.WM.SetCurrentWeaponIndex(4);
            WeaponStoreManager.WSM.ShowText("+ Assault Rifle");
        }
        else
        {

            Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
            Destroy(temp.gameObject, 1);
            Transform temp2 = PickUpGUI[3].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp2.SetParent(PlayerCanvas);
            temp.SetParent(PlayerCanvas);
            temp2.localScale = new Vector2(1, 1);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            WeaponStoreManager.WSM.ShowText("+ Assault Rifle Ammo");
        }
    }

    public void ShotgunPickup(bool purchased, bool pickup)
    {
        if (!purchased)
        {
            ammo.ShotgunClipLeft += Random.Range(1, 7);
        }
        else
        {
            ammo.ShotgunClipLeft += 7;
        }
        if (!pickup)
        {
            tempAmmoCount = 50;
        }
        else
        {
            tempAmmoCount = 20;
        }
        ammo.ShotgunMaxAmmo += tempAmmoCount;
        if (!ShowShotgun)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Shotgun";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[5].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowShotgun = true;
            WeaponManager.WM.SetCurrentWeaponIndex(5);
            WeaponStoreManager.WSM.ShowText("+ Shotgun");
        }
        else
        {

            Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
            Destroy(temp.gameObject, 1);
            Transform temp2 = PickUpGUI[1].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp2.SetParent(PlayerCanvas);
            temp.SetParent(PlayerCanvas);
            temp2.localScale = new Vector2(1, 1);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            WeaponStoreManager.WSM.ShowText("+ Shotgun Ammo");
        }
    }

    public void SniperPickup(bool purchased, bool pickup)
    {
        if (!purchased)
        {
            ammo.SniperClipLeft += Random.Range(2, 10);
        }
        else
        {
            ammo.SniperClipLeft += 10;
        }
        if (!pickup)
        {
            tempAmmoCount = 50;
        }
        else
        {
            tempAmmoCount = 15;
        }
        ammo.SniperMaxAmmo += tempAmmoCount;
        if (!ShowSniper)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Sniper Rifle";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[6].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowSniper = true;
            WeaponManager.WM.SetCurrentWeaponIndex(6);
            WeaponStoreManager.WSM.ShowText("+ Sniper");
        }
        else
        {

            Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
            Destroy(temp.gameObject, 1);
            Transform temp2 = PickUpGUI[2].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp2.SetParent(PlayerCanvas);
            temp.SetParent(PlayerCanvas);
            temp2.localScale = new Vector2(1, 1);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            WeaponStoreManager.WSM.ShowText("+ Sniper Ammo");
        }
    }

    public void MiniGunPickup(bool purchased, bool pickup)
    {
        if (!purchased)
        {
            ammo.MinigunClipLeft += Random.Range(80, 100);
        }
        else
        {
            ammo.MinigunClipLeft += 100;
        }
        if (!pickup)
        {
            tempAmmoCount = 300;
        }
        else
        {
            tempAmmoCount = 100;
        }
        ammo.MinigunMaxAmmo += tempAmmoCount;
        if (!ShowMinigun)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ MiniGun";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 15, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[7].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowMinigun = true;
            WeaponManager.WM.SetCurrentWeaponIndex(7);
            WeaponStoreManager.WSM.ShowText("+ Minigun");
        }
        else
        {

            Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
            Destroy(temp.gameObject, 1);
            //            Transform temp2 = PickUpGUI[3].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
            //            temp2.SetParent(PlayerCanvas);
            //            temp.SetParent(PlayerCanvas);
            //            temp2.localScale = new Vector2(1, 1);
            //            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
            WeaponStoreManager.WSM.ShowText("+ Minigun Ammo");
        }
    }

    void ShootingHandler()
    {
        if (Dead && PauseMenu.isPaused)
            return;

        if (!stunned)
        {
            if (CanShoot)
            {
                if (Time.time >= shootTime)
                {
                    if (!ammo.reloading)
                    {

                        if (TypeofWeapon == Weapon.Pistol)
                        {
                            if (ammo.PistolClipLeft > 0)
                            {
                                Transform temp = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation);
                                temp.GetComponent<bullet>().damage = Damage;
                                temp.GetComponent<bullet>().DeathPosition = Aiming.transform.position;
                                if (StaticVariables.MovementMultiply == 1)
                                {
                                    shootTime = Time.time + shotInterval;
                                }
                                else
                                {
                                    shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                                }

                                playermodel.bulletsUsed += 1;
                                ammo.PistolClipLeft -= 1;
                                WSanim.SetTrigger("Shoot");
                                GeneralMuzzle.SetTrigger("Shoot");
                                BulletCasing[chosenCasing].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));

                            }
                            else
                                EmptyClipSound();

                        }
                        if (TypeofWeapon == Weapon.MachineGun)
                        {
                            if (ammo.MachineGunClipLeft > 0)
                            {
                                shotsfired = 0;
                                shootBurst = true;

                            }
                            else
                                EmptyClipSound();
                        }
                        if (TypeofWeapon == Weapon.Shotgun)
                        {
                            if (ammo.ShotgunClipLeft > 0)
                            {
                                int offset = -16;
                                for(int i = 0; i < 5; i++)
                                {
                                    Transform tempBullet = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(offset, Vector3.forward));
                                    tempBullet.GetComponent<bullet>().damage = Damage;
                                    tempBullet.GetComponent<bullet>().DeathPosition = Aiming.transform.position;
                                    offset += 8;
                                }
                              
                                if (StaticVariables.MovementMultiply == 1)
                                {
                                    shootTime = Time.time + shotInterval;
                                }
                                else
                                {
                                    shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                                }
                                playermodel.bulletsUsed += 1;
                                ammo.ShotgunClipLeft -= 1;

                                WSanim.SetTrigger("Shoot");
                                GeneralMuzzle.SetTrigger("Shoot");

                                BulletCasing[chosenCasing].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));

                            }
                            else
                                EmptyClipSound();
                        }
                        if (TypeofWeapon == Weapon.Sniper)
                        {
                            if (ammo.SniperClipLeft > 0)
                            {

                                Transform tempBullet = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward)); // 0 on z
                                tempBullet.GetComponent<bullet>().damage = Damage;
                                tempBullet.GetComponent<bullet>().SniperBullet = true;
                                tempBullet.GetComponent<bullet>().DeathPosition = Aiming.transform.position;
                                if (StaticVariables.MovementMultiply == 1)
                                {
                                    shootTime = Time.time + shotInterval;
                                }
                                else
                                {
                                    shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                                }
                                playermodel.bulletsUsed += 1;
                                ammo.SniperClipLeft -= 1;

                                WSanim.SetTrigger("Shoot");
                                GeneralMuzzle.SetTrigger("Shoot");

                                BulletCasing[chosenCasing].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));

                            }
                            else
                                EmptyClipSound();

                        }
                        if (TypeofWeapon == Weapon.Minigun)
                        {

                            if (ammo.MinigunClipLeft > 0)
                            {
                                Transform temp = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation);
                                temp.GetComponent<bullet>().damage = Damage;
                                temp.GetComponent<bullet>().DeathPosition = Aiming.transform.position;

                                if (StaticVariables.MovementMultiply == 1)
                                {
                                    shootTime = Time.time + shotInterval;
                                }
                                else
                                {
                                    shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                                }

                                playermodel.bulletsUsed += 1;
                                ammo.MinigunClipLeft -= 1;

                                WSanim.SetTrigger("Shoot");
                                GeneralMuzzle.SetTrigger("Shoot");
                                BulletCasing[chosenCasing].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));

                            }
                        }
                        if (TypeofWeapon == Weapon.Cannon)
                        {
                            Transform tempBullet = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward)); // 0 on z
                            tempBullet.GetComponent<bullet>().damage = Damage;
                            tempBullet.GetComponent<bullet>().DeathPosition = Aiming.transform.position;

                            if (StaticVariables.MovementMultiply == 1)
                            {
                                shootTime = Time.time + shotInterval;
                            }
                            else
                            {
                                shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                            }
                            playermodel.bulletsUsed += 1;

                            WSanim.SetTrigger("Shoot");
                            GeneralMuzzle.SetTrigger("Shoot");
                        }
                        if (TypeofWeapon == Weapon.Revolver)
                        {
                            Transform tempBullet = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward)); // 0 on z
                            tempBullet.GetComponent<bullet>().damage = Damage;
                            tempBullet.GetComponent<bullet>().DeathPosition = Aiming.transform.position;

                            if (StaticVariables.MovementMultiply == 1)
                            {
                                shootTime = Time.time + shotInterval;
                            }
                            else
                            {
                                shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                            }
                            playermodel.bulletsUsed += 1;

                            WSanim.SetTrigger("Shoot");
                            GeneralMuzzle.SetTrigger("Shoot");



                            RevolverSecondGun.gameObject.SetActive(true);
                            Transform tempBullet2 = bulletPrefab.Spawn(RevolverSecondGun.transform.GetChild(1).transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(180, Vector3.forward)); // 0 on z
                            tempBullet2.GetComponent<bullet>().damage = Damage;
                            tempBullet2.GetComponent<bullet>().DeathPosition = Aiming.transform.position;

                            RevolverSecondGun.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");
                        }
                        if (TypeofWeapon == Weapon.PistolAgent)
                        {
                            Transform tempBullet = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward)); // 0 on z
                            tempBullet.GetComponent<bullet>().SniperBullet = true;
                            tempBullet.GetComponent<bullet>().DeathPosition = Aiming.transform.position;

                            if (StaticVariables.MovementMultiply == 1)
                            {
                                shootTime = Time.time + shotInterval;
                            }
                            else
                            {
                                shootTime = Time.time + shotInterval / StaticVariables.MovementMultiply;
                            }
                            playermodel.bulletsUsed += 1;

                            WSanim.SetTrigger("Shoot");
                            GeneralMuzzle.SetTrigger("Shoot");
                        }
                    }
                }

            }
        }
        else
        {
            Shake.shake = 1;
            allowMovement = false;

        }

    }

    void EmptyClipSound()
    {

    }

    IEnumerator IncrementStamina()
    {
        if (!Dead && !PauseMenu.isPaused)
        {
            if (!stunned)
            {
                if (Stamina < 100)
                {
                    //if (Stamina < 10)
                    //{
                    //    yield return new WaitForSeconds(2f);
                    //}
                    if (Stamina == 33)
                    {
                        yield return new WaitForSeconds(2f);

                    }
                    if (Stamina == 66)
                    {
                        yield return new WaitForSeconds(2f);
                    }
                    if (Stamina < 99)
                    {
                        Stamina+= Time.deltaTime*60;
                    }

                }
            }
        }
        yield return new WaitForSeconds(0.05f);

        StartCoroutine(IncrementStamina());
    }

    IEnumerator Death()
    {
        PostScores();

        MainPauseParent.SetActive(false);
        cg.alpha = 0;
        if (!PauseMenu.isPaused)
        {
            GetComponent<Collider2D>().enabled = false;
            //            GOEnemyShoots.GetComponent<Collider2D>().enabled = false;
            Dead = true;
            BloodSplat.SetActive(true);
            Health = startingHealth;
            //			PlayerModel.LivesLost += 10;
            PlayerPrefs.SetInt("LivesLost", PlayerModel.LivesLost += 10);
            Lives -= 1;
            AimingGO.SetActive(false);
            Shadow.SetActive(false);
            showgrey = true;
            anim.SetBool("Walk", false);
            anim.SetBool("FastWalk", false);
            yield return new WaitForSeconds(1);
            if (Lives == 0)
            {
                ActuallyDead();
            }
        }
    }

    void PostScores()
    {
        if (!Special)
        {
            if (WaveManager.WM.ZombieMode)
            {
                if (Social.localUser.authenticated)
                    Social.ReportScore((int)Score, "CgkI9OO2ssgEEAIQAQ", (bool success) => { });
                // post score to zombie scoreboard
                if (Score > HighScoreZombie)
                {
                    HighScoreZombie = Score;
                    // post

                    PlayerPrefs.SetFloat("HighScoreZombie", HighScoreZombie);
                }
            }
            else
            {
                if (Social.localUser.authenticated)
                    Social.ReportScore((int)Score, "CgkI9OO2ssgEEAIQAA", (bool success) => { });
                // post score to normal scoreboard
                if (Score > HighScoreNormal)
                {
                    HighScoreNormal = Score;
                    // post

                    PlayerPrefs.SetFloat("HighScoreNormal", HighScoreNormal);
                }
            }
        }
    }

    void ActuallyDead()
    {
        GamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        Application.LoadLevelAdditive("DeathScreen");
        GamesPlayed += 1;

        if (GamesPlayed == 5)
        {
            Application.LoadLevelAdditive("RateMe");
            GamesPlayed++;
        }
        PlayerPrefs.SetInt("GamesPlayed", GamesPlayed);
    }

    public static int GamesPlayed;
    void RemoveStun()
    {
        stunned = false;
        allowMovement = true;
        anim.SetBool("Stunned", false);
    }

    void WeaponChangeHandler()
    {
        switch (WhichWeapon)
        {
            case 0:
                TypeofWeapon = Weapon.Fist;
                break;
            case 1:
                if (ShowPipe)
                {
                    TypeofWeapon = Weapon.Pipe;
                }
                break;
            case 2:
                if (ShowKnife)
                {
                    TypeofWeapon = Weapon.Knife;
                }
                break;
            case 3:
                if (ShowPistol)
                {
                    TypeofWeapon = Weapon.Pistol;
                }
                break;
            case 4:
                if (ShowMachineGun)
                {
                    TypeofWeapon = Weapon.MachineGun;
                }
                break;
            case 5:
                if (ShowShotgun)
                {
                    TypeofWeapon = Weapon.Shotgun;
                }
                break;
            case 6:
                if (ShowSniper)
                {
                    TypeofWeapon = Weapon.Sniper;
                }
                break;
            case 7:
                if (ShowMinigun)
                {
                    TypeofWeapon = Weapon.Minigun;
                }
                break;
            case 8:
                TypeofWeapon = Weapon.ZombieHand;
                break;
            case 9:
                TypeofWeapon = Weapon.Cannon;
                break;
            case 10:
                TypeofWeapon = Weapon.Revolver;
                break;
            case 11:
                TypeofWeapon = Weapon.PistolAgent;
                break;
            case 12:
                TypeofWeapon = Weapon.Sword;
                break;

        }
    }

    private Vector3 leftJoystickInput;
    // holds the input of the Left Joystick
    public Vector3 rightJoystickInput;
    // hold the input of the Right Joystick

    public LeftJoystick leftJoystick;
    // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick;
    // the game object containing the RightJoystick script

    [HideInInspector]
    public bool CanShoot;

    void MoveForward()
    {
        if (!Dead && allowMovement)
        {
            TestAnalogs();
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }

            if (StaticVariables.MovementMultiply == 1)
            {
                anim.SetBool("FastWalk", false);
                animGun.SetBool("FastWalk", false);
            }

            // Stamina
            StaminaGO.GetComponent<EnergyBar>().valueCurrent = (int)Stamina;
            if (StaticVariables.infStamina)
            {
                Stamina = 99;

            }
            if (!evading && cooldownTimer <= 0)
            {
                if (!DodgeButton.interactable)
                    DodgeButton.interactable = true;
            }
            else
            {
                if (DodgeButton.interactable)
                    DodgeButton.interactable = false;
            }

        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("FastWalk", false);
            animGun.SetBool("Walk", false);
            animGun.SetBool("FastWalk", false);
        }
    }

    void TestAnalogs()
    {
        float GeneralSpeed = samurai ? PlayerSettings.MoveSpeed * 3 : PlayerSettings.MoveSpeed;

        vel = r.velocity;
        if (r.velocity.magnitude > maxVel)
        {
            r.velocity = Vector3.ClampMagnitude(r.velocity, maxVel);
            vel = Vector3.ClampMagnitude(vel, maxVel);
        }

        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01    

        float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

        xAmount = xMovementRightJoystick;

        if (leftJoystickInput != Vector3.zero)
        {
            smokeTimer += Time.deltaTime;
            if (smokeTimer > 0.1f)
            {
                Transform temp = DodgeHorizontal.Spawn(new Vector2(transform.position.x, transform.position.y - 0.4f), transform.rotation) as Transform;
                smokeTimer = 0;
            }
        }
        else
        {
            smokeTimer = 0;
        }

        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, zMovementLeftJoystick, 0);
            leftJoystickInput = transform.TransformDirection(-leftJoystickInput);
            leftJoystickInput *= GeneralSpeed * StaticVariables.MovementMultiply;

            if (!evading)
            {

                if (StaticVariables.MovementMultiply == 1)
                {
                    anim.SetBool("Walk", true);
                    animGun.SetBool("Walk", true);
                }
                else if (StaticVariables.MovementMultiply == 2)
                {
                    anim.SetBool("FastWalk", true);
                    animGun.SetBool("FastWalk", true);
                }
            }

            if (WeaponID != 0 && WeaponID != 1 && WeaponID != 2)
                AimingGO.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            else
            {
                AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
            }

            // move the player

            r.AddForce(leftJoystickInput * Time.deltaTime * (MoveForce * 1000));
            //            r.velocity = new Vector2(Mathf.Clamp(r.velocity.x, -10, 10), Mathf.Clamp(r.velocity.y, -10, 10));
            //            print(r.velocity);
            //            transform.Translate(leftJoystickInput * Time.deltaTime  * MoveForce);
        }

        // if there is only input from the right joystick
        if (leftJoystickInput == Vector3.zero && rightJoystickInput != Vector3.zero)
        {
            if (ControlType == 1)
            {
                //            CanShoot = true;
                // calculate the player's direction based on angle
                float tempAngle = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
                xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
                zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));
                zMovementRightJoystick = -zMovementRightJoystick;

                // Joystick Aiming
                //          var x = Input.GetAxis("Xbox360ControllerRightX");
                var x = xMovementRightJoystick;
                //          var y = Input.GetAxis("Xbox360ControllerRightY");
                var y = zMovementRightJoystick;
                if (x != 0.0f || y != 0.0f)
                {
                    var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                    AimingGO.transform.rotation = Quaternion.AngleAxis(-90.0f - angle, Vector3.forward);
                }
                else
                {
                    AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
                }

            }

        }

        // if there is input from both joysticks (Left And Right)
        if (leftJoystickInput != Vector3.zero && rightJoystickInput != Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngleInputRightJoystick = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
            xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngleInputRightJoystick));
            zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngleInputRightJoystick));
            zMovementRightJoystick = -zMovementRightJoystick;

            //          var x = Input.GetAxis("Xbox360ControllerRightX");
            var x = xMovementRightJoystick;
            //          var y = Input.GetAxis("Xbox360ControllerRightY");
            var y = zMovementRightJoystick;
            if (Tutorial.allowAim)
            {
                if (x != 0.0f || y != 0.0f)
                {
                    var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                    AimingGO.transform.rotation = Quaternion.AngleAxis(-90.0f - angle, Vector3.forward);
                }
                else
                {
                    AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
                }
            }

            // calculate the player's direction based on angle
            float tempAngleLeftJoystick = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngleLeftJoystick));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngleLeftJoystick));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, zMovementLeftJoystick, 0);
            leftJoystickInput = transform.TransformDirection(-leftJoystickInput);
            leftJoystickInput *= GeneralSpeed * StaticVariables.MovementMultiply;

            if (!evading)
            {

                if (StaticVariables.MovementMultiply == 1)
                {
                    anim.SetBool("Walk", true);
                    animGun.SetBool("Walk", true);
                }
                else if (StaticVariables.MovementMultiply == 2)
                {
                    anim.SetBool("FastWalk", true);
                    animGun.SetBool("FastWalk", true);
                }
            }

            // move the player
            r.AddForce(leftJoystickInput * Time.deltaTime * (MoveForce * 1000));

            //            print(r.velocity);
            //            transform.Translate(leftJoystickInput * Time.deltaTime * MoveForce);
        }

        if (leftJoystickInput == Vector3.zero && rightJoystickInput == Vector3.zero)
        {
            if (WeaponID != 0 && WeaponID != 1 && WeaponID != 2)
                AimingGO.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            else
            {
                AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
            }
        }

        if (ControlType == 1)
        {
            if (rightJoystickInput == Vector3.zero)
            {
                //            Debug.Log("Cant Shoot " + rightJoystickInput);
                //Aiming.enabled = false;
                CanShoot = false;
            }
            else
            {
                //Aiming.enabled = true;
                CanShoot = true;
            }
        }
        if (leftJoystickInput == Vector3.zero)
        {
            if (StaticVariables.MovementMultiply == 1)
            {
                anim.SetBool("Walk", false);
                animGun.SetBool("Walk", false);
            }
            else if (StaticVariables.MovementMultiply == 2)
            {
                anim.SetBool("FastWalk", false);
                animGun.SetBool("FastWalk", false);
            }
        }
    }

    public void ShootButton(bool pushed)
    {
        CanShoot = pushed;
    }

    public Text ControlChoiceText;

    public void ControlChoice(int i)
    {
        ControlType = i;
        PlayerPrefs.SetInt("Controls", ControlType);

        if (ControlType == 0)
        {
            ControlChoiceText.text = "Aim Controls: Button";
            Controls[0].SetActive(true);
            Controls[1].SetActive(false);
        }
        else
        {
            ControlChoiceText.text = "Aim Controls: Analog";
            Controls[0].SetActive(false);
            Controls[1].SetActive(true);
        }
    }

    public void Dodge()
    {
        if (Stamina >= 33)
        {
            if (cooldownTimer <= 0)
            {

                if (!evading)
                {
                    cooldownTimer = 2;
                    evading = true;
                    evadeTimer = 0.3f;
                }

                if (!StaticVariables.infStamina)
                {
                    Stamina -= 33;
                }
            }
        }
        if (Stamina < 0)
        {
            Stamina = 0;
        }
    }

    void ProcessEvasion()
    {
        float GeneralSpeed = samurai ? PlayerSettings.MoveSpeed * 3 : PlayerSettings.MoveSpeed;
        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01    

        if (evading)
        {

            anim.SetBool("Walk", false);
            animGun.SetBool("Walk", false);

            float tempAngleLeftJoystick = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngleLeftJoystick));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngleLeftJoystick));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, zMovementLeftJoystick, 0);
            leftJoystickInput = transform.TransformDirection(-leftJoystickInput);
            leftJoystickInput *= GeneralSpeed * 2;

            transform.Translate(leftJoystickInput * Time.fixedDeltaTime);

            Transform temp = DodgeHorizontal.Spawn(new Vector2(transform.position.x, transform.position.y - 0.4f), transform.rotation) as Transform;
            temp.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

            if (evadeTimer <= 0)
            {
                evading = false;
                evadeTimer = 0;
            }
            else
            {
                evadeTimer -= Time.deltaTime;
            }
        }
    }

    void CheckEnemies()
    {
        playermodel.checkingEnemiesInList = true;

    }
    bool DamageShield()
    {
        return cyborg && Shield.valueCurrent > 0;
    }

    public void Damaged(float dmg)
    {
        if (DamageShield())
        {
            Shield.valueCurrent -= (int)dmg;
        }
        else
        {
            Health -= dmg;

        }

        if (cyborg)
            ShieldRegenDelay = 5;

        SpriteGO.GetComponent<Animator>().SetTrigger("GetHurt");
    }

    public static void AddScore(int score)
    {
        Score += score;
    }
}