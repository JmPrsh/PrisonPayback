using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats CS;
    public List<GameObject> WeaponsPickedUp;
    public GameObject ScoreGO;
    //    public bool isEnemyInFront;
    //    public bool Debugging;
    public float Health;
    float startingHealth;
    public float Damage;
    public float HealthStarting;
    public GameObject SteroidEffect;
    public GameObject MainCameraGO;

    // GAME ELEMENTS
    //    public int BossSpawnChance;
    //    public static bool spawnBoss;
    //    public int EnemySpeed;
    //    public int EnemyFrequency;
    //    public int EnemyTotal;
    //    public int AmmoCount;
    //    public int HealthPack;
    // END GAME ELEMENTS

    // Buff Variables
    //	public static int MovementMultiply = 1;
    //	public static bool infStamina;
    //	public static bool RegenHealth;
    //    public int Deaths;

    public Transform hittext;

    // Ammo Script
    public AmmoScript ammo;

    // player movement speed
    //    public float playerSpeed = 10f;
    //speed player moves

    // Time it takes between shooting each bullet
    public float shotInterval = 0.5f;
    private float shootTime = 0.0f;

    // bullet prefab we instantiate when shooting
    public Transform bulletPrefab;

    // bullet spawn positions
    GameObject bulletSpawn;

    // sound files
    public AudioClip[] fire;
    public AudioClip[] impact;

    // if the boss has already been spawned, dont spawn again
    //    public static bool bossAlreadySpawned;

    // how fast the player rotates using mobile controls
    public float RotateSpeed = 10.0F;


    // if we have picked up the weapons, show them on screen so we can click on them to use
    public bool ShowMinigun;
    public bool ShowSniper;
    public bool ShowShotgun;
    public bool ShowMachineGun;
    public bool ShowPistol;
    public bool ShowKnife;
    public bool ShowPipe;
    //	public float Damage;

    // Players Animator Controller
    Animator anim;
    Animator animGun;

    // Sprite Animator
   
    // Muzzleflash Animator
    public Animator[] animMuzzle;

    public GameObject[] Muzzleflashes;

    // DDA Manager GameObject's Script
    PlayerModel playermodel;

    //	public Rigidbody2D Bullet;
    Rigidbody2D r;
    public bool Dead;
    SpriteRenderer SpriteGORenderer;
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
        Minigun}
    ;
    //set the original weapon ( can be set in the inspector )
    public Weapon TypeofWeapon;
    public GameObject[] Weapons;

    // Enemies
    //	public List<Transform> Enemies;

    // HUD
    public int Stamina;
    public GameObject StaminaGO;
    public GameObject LivesGO;
    EnergyBar LivesGOEB;
    public GameObject AimingGO;
    public Joystick LeftJoystick;
    public Joystick RightJoystick;
    int Lives;

    // Dodge
    public Transform DodgeHorizontal;
    public Transform DodgeVertical;
    public Transform Feet;

    // Sprites
    public Sprite[] CharacterSprites;
    public Sprite DeadSprite;
    public GameObject SpriteGO;
    public Transform AmmoCollectedText;
    public Transform HealthCollectedText;
    public Transform ScoreCollectedText;

    public enum Ethnic
    {
        White,
        Hispanic,
        Brown,
        DarkerBrown}
    ;
    //set the original weapon ( can be set in the inspector )
    public Ethnic EthnicCharacter;
    public Sprite[] CurrentWeapon;
    public Image WeaponGUI;
    public bool Blur;
    public RectTransform[] PickUpGUI;
    public Animator[] WeaponShoot;
    public Animator WSanim;
    public Animator GeneralMuzzle;
    public Transform[] BulletCasing;
    int chosenCasing;
    public float bursttimer = 0.2f;
    bool shootBurst;
    int shotsfired;
    GameObject Shadow;
    bool showgrey;
    public GameObject DyingGO;
    public GameObject BloodSplat;
    public GameObject BloodPool;
    public static bool flipped;
    public float greyAmount;
    float CharacterAngle;
    DPadButtons DPB;
    public static int CharacterSpriteID;
    public static int Score;
    public float Cash;
    public Text CashText;
    public static int Combo;
    int highestCombo;
    public float HighScoreNormal;
    public float HighScoreZombie;
    // this one we save after calculation and devide into contraband
    public float ComboTimer;
    public Text ScoreText;
    public Text ComboText;
    public EnergyBar ComboTimerEB;
    public EnergyBar HealthBar;
    public bool isPaused;
    // make event system gameobject be set to target object... then set target object as the options in pause
    public GameObject TargetPauseButton;
    bool showkillstreaks;
    public Transform KillStreakGO;
    public Sprite[] Killstreaks;
    public Color OrangeColour;
    public Text ComboParent;
    public GameObject ComboGO;
    public GameObject PlayerHUD;
    CanvasGroup cg;
    public string DamageToShow;
    public static int WeaponID;
    public GameObject EnemyCanvas;
    public Animator SBAnim;
    AudioSource music;
    public static bool allowMovement;
    public static bool Finished;
    public static string ScoringSceneLevelToLoad;
    public string ScoringSceneLevelToLoadForLoadingScreen;
    public bool stunned;
    public GameObject ShockScreen;

    // Dodge
    public float evadeTimer;
    // this tells us how long the evade takes
    public float evadeDistance = 10;
    // this tells us how far player will evade
    public float cooldownTimer;
    public bool evading;
    Vector3 moveDirection;
    public float moveSpeed;
    public float evadeTime;

    public Transform ImpactSound;
    public static float timeplayed;
    Transform PlayerCanvas;
    public SpriteRenderer Aiming;
    public Button DodgeButton;

    public static float EnemyDelayTimer;

    void Awake()
    {
        CS = this;
        r = GetComponent<Rigidbody2D>();
        HighScoreNormal = PlayerPrefs.GetFloat("HighScoreNormal");
        HighScoreZombie = PlayerPrefs.GetFloat("HighScoreZombie");


        anim = GetComponent<Animator>();
        animGun = AimingGO.GetComponent<Animator>();

        playermodel = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerModel>();
        ammo = GetComponent<AmmoScript>();
        Music.whichMusic = 2;
        Music.playMusic = true;
        cg = PlayerHUD.GetComponent<CanvasGroup>();
        SpriteGORenderer = SpriteGO.GetComponent<SpriteRenderer>();
        LivesGOEB = LivesGO.GetComponent<EnergyBar>();
        PlayerCanvas = GameObject.Find("PlayerCanvas").transform;
        if (PlayerPrefs.HasKey("Controls"))
        {
            ControlType = PlayerPrefs.GetInt("Controls");
        }else{
            ControlType = 1;
        }
        if (ControlType == 0)
        {
            ControlChoiceText.text = "Aim Controls: Button";
        }
        else
        {
            ControlChoiceText.text = "Aim Controls: Analog";
        }
    }

    void Start()
    {
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
        if (!Tutorial.isTutorial)
        {
            Invoke("AllowToMove", 2);
        }
        allowMovement = false;
        HealthStarting = Health;
        startingHealth = Health;
        //		Stamina = 99;
        playermodel.TimeLeft = 100;
        playermodel.timePlayed = 0;
        playermodel.Health = 100;
        Dead = false;
//        bossAlreadySpawned = false;
//		EnemyManager.EnemySpawnPositionID = 0;
        //		EnemyManager.spawn = true;
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
    }

    void AllowToMove()
    {
        allowMovement = true;

    }

    void DisAllowToMove()
    {
        allowMovement = false;

    }

    public GameObject[] Controls;

    void FixedUpdate()
    {
        HealthBar.valueCurrent = (int)Health;
        HealthBar.valueMax = (int)HealthStarting;

        if (EnemyDelayTimer > 1)
            EnemyDelayTimer = 0;
        else 
            EnemyDelayTimer += Time.deltaTime;

        ScoringSceneLevelToLoad = ScoringSceneLevelToLoadForLoadingScreen;

//        if (WaveManager.WM.ZombieMode)
        CashText.text = Cash.ToString("000000");

        if (ControlType == 0)
        {
            Controls[0].SetActive(true);
            Controls[1].SetActive(false);
        }
        else
        {
            Controls[0].SetActive(false);
            Controls[1].SetActive(true);
        }

        if (!PauseMenu.isPaused)
        {

            PickUpCollisionDetections();
            RegenerateHealth();

            if (Stamina < 0)
            {
                Stamina = 0;
            }

            if (!Dead)
            {
                if (!stunned)
                {
                    ProcessEvasion();
                    MoveForward(); // Player Movement
                    timeplayed += Time.deltaTime;
                }
            }

            if (TypeofWeapon != Weapon.Fist && TypeofWeapon != Weapon.Pipe && TypeofWeapon != Weapon.Knife)
            {
                Shooting();
            }

            ScoreText.text = Score.ToString("000000");
            ComboHandler();

            CharacterSpriteID = PlayerPrefs.GetInt("SGLIB_CURRENT_CHARACTER");

            SpriteGORenderer.sprite = CharacterSprites[CharacterSpriteID];
            SpriteGORenderer.material.SetFloat("_GrayScale", greyAmount);
          
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

            CheckDead();
           
            EnemyCanvas.SetActive(!Dead);

            DyingGO.transform.rotation = Quaternion.AngleAxis(CharacterAngle - 90, Vector3.forward);

            if (Time.timeScale < 1)
                Time.timeScale += 1f * Time.deltaTime / 4;

            if (Blur)
                PlayerHUD.SetActive(false);

            if (!bulletSpawn)
                bulletSpawn = GameObject.FindGameObjectWithTag("bulletSpawn");
           
            WeaponChange();

            PlayerModelHandler();

            LivesGOEB.valueCurrent= Lives * 33;


            // Respawning function
            if (Health <= 0)
            {
                StartCoroutine(Death());

            }
            if (Health < HealthStarting / 7)
            {
                SteroidEffect.SetActive(true);
            }

            TypeOfWeaponHandler();

            if (shootBurst)
            {
                BurstFireHandler();
            }
        }
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

        if (ComboTimer > 0)
        {
            ComboGO.SetActive(true);
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

    void CheckDead()
    {
        if (Dead)
        {
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

                //                        animMuzzle[WeaponID].SetTrigger("Shoot");

                Transform temp = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward));
                //                        temp.rotation = AimingGO.transform.rotation;

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

    void RegenerateHealth()
    {
        if (StaticVariables.RegenHealth && Health < startingHealth)
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
    }

    void TypeOfWeaponHandler()
    {
        // if type of weapon:
        if (TypeofWeapon == Weapon.Fist)
        {
            WeaponID = 0;
            DamageToShow = "5 - 15";
            Damage = Random.Range(5, 15);
            WeaponGUI.sprite = CurrentWeapon[0];
        }
        if (TypeofWeapon == Weapon.Pipe)
        {
            WeaponID = 1;
            DamageToShow = "20 - 30";
            Damage = Random.Range(20, 30);
            WeaponGUI.sprite = CurrentWeapon[1];
        }
        if (TypeofWeapon == Weapon.Knife)
        {
            WeaponID = 2;
            DamageToShow = "35 - 45";
            Damage = Random.Range(35, 45);
            WeaponGUI.sprite = CurrentWeapon[2];
        }
        if (TypeofWeapon == Weapon.Pistol)
        {
            chosenCasing = 0;
            WeaponID = 3;
            shotInterval = 0.2f;
            DamageToShow = "15 - 25";
            Damage = Random.Range(15, 25);
            WeaponGUI.sprite = CurrentWeapon[3];
            WSanim = WeaponShoot[0];
            GeneralMuzzle = animMuzzle[3];
        }
        if (TypeofWeapon == Weapon.MachineGun)
        {
            chosenCasing = 1;
            WeaponID = 4;
            shotInterval = 0.3f;
            DamageToShow = "45 - 55";
            Damage = Random.Range(45, 55);
            WeaponGUI.sprite = CurrentWeapon[4];
            WSanim = WeaponShoot[1];
            GeneralMuzzle = animMuzzle[4];
        }
        if (TypeofWeapon == Weapon.Shotgun)
        {
            chosenCasing = 2;
            WeaponID = 5;
            shotInterval = 1.0f;
            DamageToShow = "25 - 35";
            Damage = Random.Range(25, 35);
            WeaponGUI.sprite = CurrentWeapon[5];
            WSanim = WeaponShoot[2];
            GeneralMuzzle = animMuzzle[5];
        }
        if (TypeofWeapon == Weapon.Sniper)
        {
            chosenCasing = 3;
            WeaponID = 6;
            shotInterval = 1.0f;
            DamageToShow = "135 - 145";
            Damage = Random.Range(135, 145);
            WeaponGUI.sprite = CurrentWeapon[6];
            WSanim = WeaponShoot[3];
            GeneralMuzzle = animMuzzle[6];
        }
        if (TypeofWeapon == Weapon.Minigun)
        {
            chosenCasing = 4;
            WeaponID = 7;
            shotInterval = 0.05f;
            DamageToShow = "30 - 45";
            Damage = Random.Range(30, 45);
            WeaponGUI.sprite = CurrentWeapon[7];
            WSanim = WeaponShoot[4];
            GeneralMuzzle = animMuzzle[7];

        }

        for (int i = 0; i < Weapons.Length; i++)
        {
            //                int index = Weapons.IndexOf(WeaponID);

            if (i != WeaponID)
            {
                if(Weapons[i].activeInHierarchy)
                    Weapons[i].SetActive(false);
                if(!Weapons[WeaponID].activeInHierarchy)
                    Weapons[WeaponID].SetActive(true);
            }
        }
    }

    void PickUpCollisionDetections()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Pickups"));
        if (hitInfo.collider != null)
        {

            if (hitInfo.transform.CompareTag("Cigs"))
            {
                playermodel.PlayerCigs += 10;
                hitInfo.transform.Recycle();
            }

            if (hitInfo.transform.CompareTag("Pipe"))
            {
                PipePickup();
                hitInfo.transform.Recycle();
            }
            if (hitInfo.transform.CompareTag("Knife"))
            {
                KnifePickup();
                hitInfo.transform.Recycle();
            }
            if (hitInfo.transform.CompareTag("Pistol"))
            {
                PistolPickup(false, true);
                hitInfo.transform.Recycle();

            }
            if (hitInfo.transform.CompareTag("Shotgun"))
            {
                ShotgunPickup(false, true);
                hitInfo.transform.Recycle();
            }
            if (hitInfo.transform.CompareTag("Sniper"))
            {
                SniperPickup(false, true);
                hitInfo.transform.Recycle();
            }
            if (hitInfo.transform.CompareTag("MachineGun"))
            {

                MachineGunPickup(false, true);
                hitInfo.transform.Recycle();
            }
            if (hitInfo.transform.CompareTag("MiniGun"))
            {

                MiniGunPickup(false, true);
                hitInfo.transform.Recycle();
            }

            // if collided with ammo box give ammo then destroy ammo box
            // if collided with ammo box give ammo then destroy ammo box
            if (hitInfo.transform.tag == "PistolAmmo")
            {
                int tempAmmoCount = Random.Range(5, 50);
                ammo.PistolMaxAmmo += tempAmmoCount;
                Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
                hitInfo.transform.Recycle();
                Destroy(temp.gameObject, 1);
                Transform temp2 = PickUpGUI[0].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(PlayerCanvas);
                temp.SetParent(PlayerCanvas);
                temp2.localScale = new Vector2(1, 1);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
            }
            if (hitInfo.transform.tag == "ShotgunAmmo")
            {
                int tempAmmoCount = Random.Range(5, 25);
                ammo.ShotgunMaxAmmo += tempAmmoCount;
                Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
                hitInfo.transform.Recycle();
                Destroy(temp.gameObject, 1);
                Transform temp2 = PickUpGUI[1].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(PlayerCanvas);
                temp.SetParent(PlayerCanvas);
                temp2.localScale = new Vector2(1, 1);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
            }
            if (hitInfo.transform.tag == "SniperAmmo")
            {
                int tempAmmoCount = Random.Range(5, 20);
                ammo.SniperMaxAmmo += tempAmmoCount;
                Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
                hitInfo.transform.Recycle();
                Destroy(temp.gameObject, 1);
                Transform temp2 = PickUpGUI[2].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(PlayerCanvas);
                temp.SetParent(PlayerCanvas);
                temp2.localScale = new Vector2(1, 1);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);

            }
            if (hitInfo.transform.tag == "MachineGunAmmo")
            {
                int tempAmmoCount = Random.Range(5, 20);
                ammo.MachineGunMaxAmmo += tempAmmoCount;
                Transform temp = AmmoCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp.GetComponentInChildren<Text>().text = "+" + tempAmmoCount;
                hitInfo.transform.Recycle();
                Destroy(temp.gameObject, 1);
                Transform temp2 = PickUpGUI[3].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)) as Transform;
                temp2.SetParent(PlayerCanvas);
                temp.SetParent(PlayerCanvas);
                temp2.localScale = new Vector2(1, 1);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
            }

            if (hitInfo.transform.tag == "HealthPack")
            {
                if (Health < HealthStarting)
                {
                    SteroidEffect.SetActive(false);
                    //				int HealthAdd = Random.Range (100, 300);
                    Health = HealthStarting;
                    Transform temp = HealthCollectedText.Spawn(new Vector2(transform.position.x, transform.position.y - 6), transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp.GetComponentInChildren<Text>().text = "Health ++";

                    hitInfo.transform.Recycle();
                    Destroy(temp.gameObject, 1);
                    Transform temp2 = PickUpGUI[9].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp2.SetParent(PlayerCanvas);
                    temp2.localScale = new Vector2(1, 1);
                    temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
                }
            }
            if (hitInfo.transform.tag == "Milk")
            {
                ObjectiveMarker.PickedUpItem = true;
                if (DPB.Milk < 5)
                {
                    DPB.Milk += 1;
                    Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp.GetComponentInChildren<Text>().text = "+1";

                    hitInfo.transform.Recycle();
                    Destroy(temp.gameObject, 1);
                    Transform temp2 = PickUpGUI[8].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp2.SetParent(PlayerCanvas);
                    temp2.localScale = new Vector2(1, 1);
                    temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
                }
            }
            if (hitInfo.transform.tag == "Needle")
            {
                ObjectiveMarker.PickedUpItem = true;
                if (DPB.Needles < 5)
                {
                    DPB.Needles += 1;
                    Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp.GetComponentInChildren<Text>().text = "+1";

                    hitInfo.transform.Recycle();
                    Destroy(temp.gameObject, 1);
                    Transform temp2 = PickUpGUI[8].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp2.SetParent(PlayerCanvas);
                    temp2.localScale = new Vector2(1, 1);
                    temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
                }
            }
            if (hitInfo.transform.tag == "Pills")
            {
                ObjectiveMarker.PickedUpItem = true;
                if (DPB.Pills < 5)
                {
                    DPB.Pills += 1;
                    Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp.GetComponentInChildren<Text>().text = "+1";

                    hitInfo.transform.Recycle();
                    Destroy(temp.gameObject, 1);
                    Transform temp2 = PickUpGUI[8].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp2.SetParent(PlayerCanvas);
                    temp2.localScale = new Vector2(1, 1);
                    temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
                }
            }
            if (hitInfo.transform.tag == "Powder")
            {
                ObjectiveMarker.PickedUpItem = true;
                if (DPB.Powder < 5)
                {
                    DPB.Powder += 1;
                    Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp.GetComponentInChildren<Text>().text = "+1";

                    hitInfo.transform.Recycle();
                    Destroy(temp.gameObject, 1);
                    Transform temp2 = PickUpGUI[8].Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                    temp2.SetParent(PlayerCanvas);
                    temp2.localScale = new Vector2(1, 1);
                    temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
                }
            }

				
        }


    }

    public void KnifePickup()
    {
        if (!ShowKnife)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Knife";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
//            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            TypeofWeapon = Weapon.Knife;
            ShowKnife = true;
            Weapons[2].GetComponent<PickedUpCheck>().pickedUp = true;

        }
    }

    public void PipePickup()
    {
        if (!ShowPipe)
        {
            Transform temp = AmmoCollectedText.Spawn(transform.position, transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            temp.GetComponentInChildren<Text>().text = "+ Pipe";
            temp.SetParent(PlayerCanvas);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
//            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            TypeofWeapon = Weapon.Pipe;
            ShowPipe = true;
            Weapons[1].GetComponent<PickedUpCheck>().pickedUp = true;

        }
    }

    int tempAmmoCount;

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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
            //                    WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[3].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowPistol = true;

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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
//            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[4].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowMachineGun = true;
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
//            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[5].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowShotgun = true;
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
//            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[6].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowSniper = true;
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
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
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, PickUpGUIy, 0);
            //            WeaponsPickedUp.Add(hitInfo.transform.gameObject);
            Weapons[7].GetComponent<PickedUpCheck>().pickedUp = true;
            ShowMinigun = true;
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
        }
    }

    void Shooting()
    {
        if (!Dead && !PauseMenu.isPaused)
        {

            if (!stunned)
            {
                
                    
                // if can shoot

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
                                        
//                                        Debug.Log(bulletSpawn.transform.rotation);
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation);

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
                                {
                                    // play empty gun sound effect
                                    //								GetComponent<AudioSource> ().clip = fire;
                                    //								GetComponent<AudioSource> ().Play ();
                                }

                            }
                            if (TypeofWeapon == Weapon.MachineGun)
                            {
                                if (ammo.MachineGunClipLeft > 0)
                                {
                                    shotsfired = 0;
                                    shootBurst = true;

                                }
                                else
                                {
                                    // play empty gun sound effect
                                    //								GetComponent<AudioSource> ().clip = fire;
                                    //								GetComponent<AudioSource> ().Play ();
                                }
                            }
                            if (TypeofWeapon == Weapon.Shotgun)
                            {
                                if (ammo.ShotgunClipLeft > 0)
                                {
                                        
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(-16, Vector3.forward)); // -30 on z
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(-7, Vector3.forward)); // -30 on z
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward)); // 0 on z
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(7, Vector3.forward)); // 15 on z
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(16, Vector3.forward)); // 30 on z
                                    //										
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
                                {
                                    // play empty gun sound effect
                                    //								GetComponent<AudioSource> ().clip = fire;
                                    //								GetComponent<AudioSource> ().Play ();
                                }
                            }
                            if (TypeofWeapon == Weapon.Sniper)
                            {
                                if (ammo.SniperClipLeft > 0)
                                {
                                        
                                    Transform tempBullet = bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation * Quaternion.AngleAxis(1, Vector3.forward)); // 0 on z

                                    tempBullet.GetComponent<bullet>().SniperBullet = true;

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
                                {
                                    // play empty gun sound effect
                                    //								GetComponent<AudioSource> ().clip = fire;
                                    //								GetComponent<AudioSource> ().Play ();
                                }
                            }
                            if (TypeofWeapon == Weapon.Minigun)
                            {
                                    
                                if (ammo.MinigunClipLeft > 0)
                                {
                                    bulletPrefab.Spawn(bulletSpawn.transform.position, AimingGO.transform.rotation);

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
                                else
                                {
                                    // play empty gun sound effect
                                    //                              GetComponent<AudioSource> ().clip = fire;
                                    //                              GetComponent<AudioSource> ().Play ();
                                }
                            }
                        }
                    }


                }
            }
            else if (stunned)
            {
                Shake.shake = 1;
                allowMovement = false;
                Invoke("RemoveStun", 2);
            }

        }
    }

    IEnumerator IncrementStamina()
    {
        if (!Dead && !PauseMenu.isPaused)
        {
            if (!stunned)
            {
                if (Stamina < 100)
                {

                    if (Stamina == 33)
                    {
//                        Debug.Log("First Stamina Filled");
                        yield return new WaitForSeconds(2f);

                    }
                    if (Stamina == 66)
                    {
//                        Debug.Log("Second Stamina Filled");
                        yield return new WaitForSeconds(2f);

                    }
                    if (Stamina < 99)
                    {
                        Stamina++;
                    }

                }
            }
        }
        yield return new WaitForSeconds(0.05f);

        StartCoroutine(IncrementStamina());
    }

    public GameObject GOEnemyShoots;
    public GameObject MainPauseParent;


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
        if (WaveManager.WM.ZombieMode)
        {
            // post score to zombie scoreboard
            if (Score > HighScoreZombie)
            {
                HighScoreZombie = Score;
                // post
                Social.ReportScore((int)HighScoreZombie, "CgkI9OO2ssgEEAIQAQ", (bool success) =>
                    {
                    });
                PlayerPrefs.SetFloat("HighScoreZombie", HighScoreZombie);
            }
        }
        else
        {
            // post score to normal scoreboard
            if (Score > HighScoreNormal)
            {
                HighScoreNormal = Score;
                // post
                Social.ReportScore((int)HighScoreZombie, "CgkI9OO2ssgEEAIQAA", (bool success) =>
                    {
                    });
                PlayerPrefs.SetFloat("HighScoreNormal", HighScoreNormal);
            }
        }
    }

    void ActuallyDead()
    {
        GamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        Application.LoadLevelAdditive("DeathScreen");
        GamesPlayed += 1;

        if(GamesPlayed ==5){
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
    }

    public int WhichWeapon;

    void WeaponChange()
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
            StaminaGO.GetComponent<EnergyBar>().valueCurrent= Stamina;
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

    float shootdelay = 0;
    public int ControlType;
    public static float xAmount;
    public float MoveForce;
    public Vector2 vel;
    public float maxVel;
    void TestAnalogs()
    {
        vel = r.velocity;
        if(r.velocity.magnitude > maxVel){
            r.velocity = Vector3.ClampMagnitude(r.velocity, maxVel);
            vel = Vector3.ClampMagnitude(vel, maxVel);
        }

        #if UNITY_EDITOR
        moveSpeed = 3;
        #endif

        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01    

        float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

        xAmount = xMovementRightJoystick;


        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {
            // calculate the player's direction based on angle
            float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
            xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
            zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

            leftJoystickInput = new Vector3(xMovementLeftJoystick, zMovementLeftJoystick, 0);
            leftJoystickInput = transform.TransformDirection(-leftJoystickInput);
            leftJoystickInput *= moveSpeed;

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
       
            if(WeaponID != 0 && WeaponID != 1 && WeaponID != 2)
                AimingGO.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            else{
                AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
            }

            // move the player

            r.AddForce(leftJoystickInput * Time.deltaTime * (MoveForce*1000));
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
            leftJoystickInput *= moveSpeed;

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
            r.AddForce(leftJoystickInput * Time.deltaTime * (MoveForce*1000));
           
//            print(r.velocity);
//            transform.Translate(leftJoystickInput * Time.deltaTime * MoveForce);
        }

        if (leftJoystickInput == Vector3.zero && rightJoystickInput == Vector3.zero)
        {
            if(WeaponID != 0 && WeaponID != 1 && WeaponID != 2)
            AimingGO.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            else{
                AimingGO.transform.rotation = Quaternion.AngleAxis(-180.0f, Vector3.forward);
            }
        }

        if (ControlType == 1)
        {
            if (rightJoystickInput == Vector3.zero)
            {
//            Debug.Log("Cant Shoot " + rightJoystickInput);
                Aiming.enabled = false;
                CanShoot = false;
                shootdelay = 0;
            }
            else
            {
                Aiming.enabled = true;
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
        }
        else
        {
            ControlChoiceText.text = "Aim Controls: Analog";
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
    }

    void ProcessEvasion()
    {
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
            leftJoystickInput *= moveSpeed;

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

    public int PickUpGUIy;

    void CheckEnemies()
    {
        playermodel.checkingEnemiesInList = true;

    }

    public void Damaged(float dmg)
    {
        Health -= dmg;
        SpriteGO.GetComponent<Animator>().SetTrigger("GetHurt");
    }

    public static void AddScore(int score)
    {
        Score += score;
    }


    //    public float hEnergyBarvalueCurrent= 1.0F;
    //    void OnGUI() {
    ////        hEnergyBarvalueCurrent= GUI.HorizontalEnergyBar(new Rect(25, 100, 200, 25), hEnergyBarValue, 1.0F, 10.0F);
    //        GUI.Label(new Rect(25, 150, 200, 25),"Can Shoot = " +CanShoot);
    //    }
}
