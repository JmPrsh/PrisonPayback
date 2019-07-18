using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class attackPlayer : MonoBehaviour
{
    public float MoveSpeed;
    public Enemy EnemyProperties;
    public enum EnemyClass
    {
        StandardGuard,
        StandardGuardBoss,
        Captain,
        CaptainBoss,
        Riot,
        RiotBoss,
        Swat,
        SwatBoss,
        Sniper,
        Warden,
        Dog,
        SwatDemolition,
        RiotStun,
        Zombie,
        ZombieBrute}
    ;

    public EnemyClass enemyclass;
    Vector3 targetPosition = new Vector3();
    public Animator anim;
    public bool seenPlayer;
    int randomIntMove;
    public float health = 100;
    public EnemyBullet bulletPrefab;
    public float shotInterval = 1.0f;
    public float shootTime = 0.0f;
    public Transform[] ShootFrom;
    public int SpawnWeapon;
    public int WeaponChanceMax;
    public Transform[] WeaponToSpawn;
    public GameObject Warning;

    public enum TypeOfEnemy
    {
        Melee,
        Pistol,
        Machinegun,
        Shotgun,
        Sniper,
        Minigun,
        DogBite,
        Demolition,
    }
    ;

    public TypeOfEnemy typeofenemy;
    public GameObject[] WeaponOfChoice;
    public int chooseEnemyAtStartUp;
    public Transform childRandom;
    public Transform ChildSprite;
    public Transform[] DeadGuard;
    public Sprite[] EnemySprite;
    public Sprite[] InmateSprite;
    public float Damage;
    public List<GameObject> WeaponInt;
    public Sprite[] EnemyBullets;
    public Transform[] EnemyBulletCasing;
    public bool reload;
    float reloadtime;
    public int bulletsused;
    int bulletsinclip;
    public float bursttimer = 0.2f;
    bool shootBurst;
    int shotsfired;
    public AudioClip[] fire;
    public Animator[] WeaponShoot;
    Animator WSanim;
    public Animator[] WeaponMuzzleFlash;
    Animator WMFanim;
    public GameObject Shadow;
    bool showgrey;
    public GameObject DyingGO;
    public GameObject BloodSplat;
    public GameObject BloodPool;
    public bool flipped;
    public float greyAmount;
    public bool Dead = false;
    float CharacterAngle;
    public GameObject AimingGO;
    public Animator Tail;
    public Transform BloodStain;
    public EnemyBullet Grenade;
    public GameObject Target;
    AudioSource audiosource;
    public GameObject BossIcon;
    float EnemyDistance;
    public bool CanSwing;
    public GameObject WorldCanvas;
    Text ScoreGO;
    Animator ScoreGOAnim;
    Animator ChildAnimator;
    Text hitSpawnText;
    Collider2D Col;
    Collider2D ChildCol;
    Text CSScoreCollected;
    Rigidbody2D rg2d;
    SpriteRenderer ChildSpriteRenderer;
    bool isBrute;
    bool isBoss;
//    qpFollowMouseObject


    void Awake()
    {
        ChildSpriteRenderer = ChildSprite.GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
        rg2d = GetComponent<Rigidbody2D>();
        CSScoreCollected = CharacterStats.CS.ScoreCollectedText.GetComponent<Text>();
        ChildCol = ChildSprite.GetComponent<Collider2D>();
        Col = GetComponent<Collider2D>();
        hitSpawnText = CharacterStats.CS.hittext.GetComponent<Text>();
        ChildAnimator = ChildSprite.GetComponent<Animator>();
        ScoreGO = CharacterStats.CS.ScoreGO.GetComponent<Text>();
        ScoreGOAnim = CharacterStats.CS.ScoreGO.GetComponent<Animator>();

        foreach (Transform EBC in EnemyBulletCasing)
        {
            EBC.CreatePool();
        }
        foreach (Transform W in WeaponToSpawn)
        {
            W.CreatePool();
        }

//        chooseEnemyAtStartUp = Random.Range(0, 2);



    }

    void OnEnable()
    {
        isBrute = false;
        isBoss = false;
        FindClosestPlayer();
        RandomSwingTime = Random.Range(1.6f, 2.5f);
        Dead = false;
        transform.SetParent(GameObject.Find("EnemiesParent").transform);
//		Target = GameObject.FindGameObjectWithTag ("Player");
		
		
        if (Target != null)
        {
            targetPosition = Target.transform.position;
        }
        transform.position = new Vector3(transform.position.x + Random.Range(3, -3), transform.position.y + Random.Range(3, -3), transform.position.z);
        shootTime = 0;

        Quaternion randomRot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        childRandom.transform.rotation = randomRot;
        if (enemyclass == EnemyClass.StandardGuard)
        {
            MoveSpeed = 6;
            health = 100 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[Random.Range(0, 3)];
            if (typeofenemy == TypeOfEnemy.Melee)
            {
                this.name = "Standard Guard Melee";
                Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
                shotInterval = 1f;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[0]);
                    weapons.SetActive(false);
                }
                EnemyDistance = 1.5f;
            }
            if (typeofenemy == TypeOfEnemy.Pistol)
            {
                this.name = "Standard Guard Pistol";
                Damage = Random.Range(35, 45) * WaveManager.DamageMultiplier;
                shotInterval = 3f;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[1]);
                    weapons.SetActive(false);
                }
                bulletsinclip = 7;
                reloadtime = 5;
                WSanim = WeaponShoot[0];
                WMFanim = WeaponMuzzleFlash[0];
                EnemyDistance = 7f;
            }
        }
        if (enemyclass == EnemyClass.StandardGuardBoss)
        {
            MoveSpeed = 3;
            isBrute = true;
            BossIcon.SetActive(true);
            health = 2000 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[Random.Range(0, 3)];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            ChildSprite.transform.position = new Vector2(ChildSprite.transform.position.x, ChildSprite.transform.position.y + 0.2f);
            chooseEnemyAtStartUp = Random.Range(0, 2);
            switch (chooseEnemyAtStartUp)
            {
                case 0:
                    typeofenemy = TypeOfEnemy.Melee;
                    break;
                case 1:
                    typeofenemy = TypeOfEnemy.Pistol;
                    break;
            
            }
            if (typeofenemy == TypeOfEnemy.Melee)
            {
                this.name = "Standard Boss Guard Melee";
                Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
                shotInterval = 1f;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[0]);
                    weapons.SetActive(false);
                }
                reloadtime = 1;
                bulletsinclip = 1;
                EnemyDistance = 1.5f;
            }
            if (typeofenemy == TypeOfEnemy.Pistol)
            {
                this.name = "Standard Boss Guard Pistol";
                Damage = Random.Range(35, 45) * WaveManager.DamageMultiplier;
                ;
                shotInterval = 3f;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[1]);
                    weapons.SetActive(false);
                }
                bulletsinclip = 7;
                reloadtime = 5;
                WSanim = WeaponShoot[0];
                WMFanim = WeaponMuzzleFlash[0];
                EnemyDistance = 6f;
            }
        }
        if (enemyclass == EnemyClass.Captain)
        {
            MoveSpeed = 5;
            this.name = "Captain";
            health = 200 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[3];
            typeofenemy = TypeOfEnemy.Shotgun;
            Damage = Random.Range(45, 55) * WaveManager.DamageMultiplier;
            shotInterval = 5f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[3]);
                weapons.SetActive(false);
            }
            bulletsinclip = 5;
            reloadtime = 6;
            WSanim = WeaponShoot[1];
            WMFanim = WeaponMuzzleFlash[1];
            EnemyDistance = 6f;
        }
        if (enemyclass == EnemyClass.CaptainBoss)
        {
            MoveSpeed = 3;
            isBrute = true;
            this.name = "Captain Boss";
            BossIcon.SetActive(true);
            health = 2500 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[3];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            typeofenemy = TypeOfEnemy.Shotgun;
            Damage = Random.Range(45, 55) * WaveManager.DamageMultiplier;
            shotInterval = 5f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[3]);
                weapons.SetActive(false);
            }
            bulletsinclip = 5;
            reloadtime = 6;
            WSanim = WeaponShoot[1];
            WMFanim = WeaponMuzzleFlash[1];
            EnemyDistance = 6f;
        }
        if (enemyclass == EnemyClass.Riot)
        {
            MoveSpeed = 4;
            this.name = "Riot Guard";
            health = 600 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[4];
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
            shotInterval = 2;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                WeaponInt.Add(WeaponOfChoice[6]);
                weapons.SetActive(false);
            }
            bulletsinclip = 1;
            reloadtime = 1;
            EnemyDistance = 3f;
        }
        if (enemyclass == EnemyClass.RiotStun)
        {
            MoveSpeed = 4;
            this.name = "Riot Guard Stun";
            health = 450 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[4];
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
            shotInterval = 8;
            foreach (GameObject weapons in WeaponOfChoice)
            {

                //				WeaponInt.Add (WeaponOfChoice [6]);
                WeaponInt.Add(WeaponOfChoice[7]);
                weapons.SetActive(false);
            }
            bulletsinclip = 1;
            reloadtime = 1;
            EnemyDistance = 3f;
        }
        if (enemyclass == EnemyClass.RiotBoss)
        {
            MoveSpeed = 3;
            isBrute = true;
            this.name = "Riot Guard Boss";
            BossIcon.SetActive(true);
            health = 3000 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[4];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
            shotInterval = 2;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                WeaponInt.Add(WeaponOfChoice[6]);
                weapons.SetActive(false);
            }
            reloadtime = 1;
            bulletsinclip = 1;
            EnemyDistance = 3f;
        }
        if (enemyclass == EnemyClass.Swat)
        {
            MoveSpeed = 7;
            this.name = "Swat";
            health = 400 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[5];
            typeofenemy = TypeOfEnemy.Machinegun;
            Damage = Random.Range(25, 35) * WaveManager.DamageMultiplier;
            shotInterval = 3f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[2]);
                weapons.SetActive(false);
            }
            bulletsinclip = 15;
            reloadtime = 4;
            WSanim = WeaponShoot[2];
            WMFanim = WeaponMuzzleFlash[2];
            EnemyDistance = 7f;
        }
        if (enemyclass == EnemyClass.SwatDemolition)
        {
            MoveSpeed = 7;
            this.name = "Swat Demolition";
            health = 350 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[10];

            typeofenemy = TypeOfEnemy.Demolition;
            Damage = Random.Range(75, 85) * WaveManager.DamageMultiplier;
            shotInterval = 6f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                weapons.SetActive(false);
            }
            bulletsinclip = 1;
            reloadtime = 0;
            EnemyDistance = 6f;
        }
        if (enemyclass == EnemyClass.SwatBoss)
        {
            MoveSpeed = 5;
            isBrute = true;
            this.name = "Swat Boss";
            BossIcon.SetActive(true);
            health = 3500 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[5];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            typeofenemy = TypeOfEnemy.Machinegun;
            Damage = Random.Range(25, 35) * WaveManager.DamageMultiplier;
            shotInterval = 3f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[2]);
                weapons.SetActive(false);
            }
            bulletsinclip = 15;
            reloadtime = 4;
            WSanim = WeaponShoot[2];
            WMFanim = WeaponMuzzleFlash[2];
            EnemyDistance = 8f;
        }
        if (enemyclass == EnemyClass.Sniper)
        {
            MoveSpeed = 4;
            this.name = "Sniper Guard";
            health = 1000 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[7];
            typeofenemy = TypeOfEnemy.Sniper;
            Damage = Random.Range(135, 145) * WaveManager.DamageMultiplier;
            shotInterval = 4f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[4]);
                weapons.SetActive(false);
            }
            bulletsinclip = 5;
            reloadtime = 6;
            WSanim = WeaponShoot[3];
            WMFanim = WeaponMuzzleFlash[3];
            EnemyDistance = 10f;
        }
        if (enemyclass == EnemyClass.Warden)
        {
            MoveSpeed = 3;
            isBoss = true;
            this.name = "Warden";
            health = 5000 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[6];
            typeofenemy = TypeOfEnemy.Minigun;
            Damage = Random.Range(5, 15) * WaveManager.DamageMultiplier;
            shotInterval = 0.05f;

            // adds weapon of choice to list to be active and turns off other weapons
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[5]);
                weapons.SetActive(false);
            }
            bulletsinclip = 200;
            reloadtime = 10;
            WSanim = WeaponShoot[4];
            WMFanim = WeaponMuzzleFlash[4];
            EnemyDistance = 10f;
        }
        if (enemyclass == EnemyClass.Dog)
        {
            MoveSpeed = 10;
            this.name = "Dog";
            health = 50 * WaveManager.HealthMultiplier;
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[8];
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(10, 20) * WaveManager.DamageMultiplier;
            shotInterval = 3f;
            reloadtime = 1;
            bulletsinclip = 1;
            EnemyDistance = 1.4f;
        }

        if (enemyclass == EnemyClass.StandardGuard || enemyclass == EnemyClass.StandardGuardBoss)
        {
            switch (chooseEnemyAtStartUp)
            {
                case 0:
                    typeofenemy = TypeOfEnemy.Melee;
                    break;
                case 1:
                    typeofenemy = TypeOfEnemy.Pistol;
                    break;

            }
        }
        if (enemyclass == EnemyClass.Zombie)
        {
            MoveSpeed = 6;
            health = 100 * WaveManager.HealthMultiplier;
//            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[Random.Range(0, 3)];
           
            this.name = "Zombie";
            Damage = Random.Range(50, 100) * WaveManager.DamageMultiplier;
            shotInterval = 1f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                weapons.SetActive(false);
            }
            EnemyDistance = 1.5f;
        }
        if (enemyclass == EnemyClass.ZombieBrute)
        {
            MoveSpeed = 3;
            isBrute = true;
            BossIcon.SetActive(true);
            health = 500 * WaveManager.HealthMultiplier;
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            ChildSprite.transform.position = new Vector2(ChildSprite.transform.position.x, ChildSprite.transform.position.y + 0.2f);

          
            this.name = "Zombie Brute";
            Damage = Random.Range(100, 150) * WaveManager.DamageMultiplier;
            shotInterval = 1f;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                weapons.SetActive(false);
            }
            EnemyDistance = 1.5f;
        }

        for (var i = 0; i < WeaponInt.Count; i++)
        {
            WeaponInt[i].SetActive(true);
        }

        AimingGO.GetComponent<LookTowardsPlayer>().Target = Target;

    }

    // Use this for initialization
    void Start()
    {
        StaticVariables.EnemySpeed = 1;
    }

    // Update is called once per frame
    bool RemovedSelf;

    void FixedUpdate()
    {
        if (!CharacterStats.CS.Dead)
        {
            if (!PauseMenu.isPaused && CharacterStats.allowMovement)
            {
                ChildSpriteRenderer.material.SetFloat("_GrayScale", greyAmount);
                if (enemyclass == EnemyClass.Dog)
                {
                    Tail.GetComponent<SpriteRenderer>().material.SetFloat("_GrayScale", greyAmount);
                }
                if (showgrey)
                {
                    if (greyAmount > 0)
                    {
                        greyAmount -= 1 * Time.deltaTime * 2;
                    }
                }else{
                    if (greyAmount < 1)
                    {
                        greyAmount += 1 * Time.deltaTime * 2;
                    }
                }

                if (Target != null)
                {
					
                    DyingGO.transform.rotation = Quaternion.AngleAxis(CharacterAngle, Vector3.forward);

                    if (bulletsused >= bulletsinclip)
                    {
                        bulletsused = 0;
                        StartCoroutine(Reload());
                    }

                    CheckifAlive();
                    if (!Dead)
                    {
                        CheckDistance();
                        Shoot();
                        Reset();

                    }
                    else
                    {
                        anim.SetBool("Walk", false);
                    }

                }

               
            }
        }
        else
        {
            anim.SetBool("Walk", false);

        }
    }



    void Reset()
    {
        CharacterAngle = 0;
        startDeath = false;
        AimingGO.SetActive(true);
        BloodSplat.SetActive(false);
        BloodPool.SetActive(false);
        Col.enabled = true;
        ChildCol.enabled = true;
        Shadow.SetActive(true);
        showgrey = false;
        ChildSpriteRenderer.material.SetFloat("_EffectAmount", 0);
        if (enemyclass == EnemyClass.Dog)
        {
            Tail.GetComponent<SpriteRenderer>().material.SetFloat("_EffectAmount", 0);
        }
    }
    public float MoveTimer;
//    public List<Vector2> path;
    float timer;

    void CheckDistance()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > EnemyDistance)
        {
            transform.position = Vector3.MoveTowards( transform.position, Target.transform.position, MoveSpeed*Time.deltaTime);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
            if (typeofenemy == TypeOfEnemy.Melee)
            {

                if (shootTime >= RandomSwingTime)
                {
                    CanSwing = true;
                    shootTime = 0;
                }
                else
                {
                    CanSwing = false;
                    shootTime += 1 * Time.deltaTime;
                    // play melee animation
                }
            }
            else
            {
                if (shootTime >= shotInterval && !reload)
                {

                    if (enemyclass == EnemyClass.StandardGuard || enemyclass == EnemyClass.StandardGuardBoss)
                    {
                        if (typeofenemy != TypeOfEnemy.Melee)
                        {
                            EnemyBullet temp = bulletPrefab.Spawn(ShootFrom[0].transform.position, ShootFrom[0].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
//                    temp.name = "BulletToCheck";
                            temp.transform.SetParent(null);
                            temp.gameObject.SetActive(true);
                            temp.Damage = Damage;
                            temp.spriterenderer.sprite = EnemyBullets[0];
                            audiosource.clip = fire[0];
                            audiosource.Play();
                            EnemyBulletCasing[0].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
                            bulletsused += 1;
                            WSanim.SetTrigger("Shoot");
                            WMFanim.SetTrigger("Shoot");
                        }
                    }

                    if (enemyclass == EnemyClass.Captain || enemyclass == EnemyClass.CaptainBoss)
                    {
                        EnemyBullet temp = bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-60, Vector3.forward)); // -30 on z
                        EnemyBullet temp2 = bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-75, Vector3.forward)); // -30 on z
                        EnemyBullet temp3 = bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward)); // 0 on z
                        EnemyBullet temp4 = bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-105, Vector3.forward)); // 15 on z
                        EnemyBullet temp5 = bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-120, Vector3.forward)); // 30 on z
                        temp.Damage = Damage;
                        temp2.Damage = Damage;
                        temp3.Damage = Damage;
                        temp4.Damage = Damage;
                        temp5.Damage = Damage;
                        temp.spriterenderer.sprite = EnemyBullets[1];
                        temp2.spriterenderer.sprite = EnemyBullets[1];
                        temp3.spriterenderer.sprite = EnemyBullets[1];
                        temp4.spriterenderer.sprite = EnemyBullets[1];
                        temp5.spriterenderer.sprite = EnemyBullets[1];
                        EnemyBulletCasing[1].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
                        bulletsused += 1;
                        WSanim.SetTrigger("Shoot");
                        WMFanim.SetTrigger("Shoot");
                        audiosource.clip = fire[1];
                        audiosource.Play();
                    }

                    if (enemyclass == EnemyClass.Swat || enemyclass == EnemyClass.SwatBoss)
                    {
                        audiosource.clip = fire[2];
                        shotsfired = 0;
                        shootBurst = true;
                    }

                    if (enemyclass == EnemyClass.SwatDemolition)
                    {
                        EnemyBullet temp = Grenade.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + 0.2f, transform.position.z), transform.rotation);
                        temp.Grenade = true;
                        temp.Damage = Damage;
                        bulletsused += 1;
                    }

                    if (enemyclass == EnemyClass.Sniper)
                    {
                        EnemyBullet temp = bulletPrefab.Spawn(ShootFrom[3].transform.position, ShootFrom[3].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
                        temp.Damage = Damage;
                        temp.spriterenderer.sprite = EnemyBullets[3];
                        EnemyBulletCasing[3].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
                        bulletsused += 1;
                        WSanim.SetTrigger("Shoot");
                        WMFanim.SetTrigger("Shoot");
                        audiosource.clip = fire[3];
                        audiosource.Play();
                    }

                    if (enemyclass == EnemyClass.Warden)
                    {
                        EnemyBullet temp = bulletPrefab.Spawn(ShootFrom[4].transform.position, ShootFrom[4].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
                        temp.Damage = Damage;
                        temp.spriterenderer.sprite = EnemyBullets[4];
                        EnemyBulletCasing[3].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
                        bulletsused += 1;
                        WSanim.SetTrigger("Shoot");
                        WMFanim.SetTrigger("Shoot");
                        audiosource.clip = fire[4];
                        audiosource.Play();

                    }
                    shootTime = 0;
                }
                else
                {
                    shootTime += 1 * Time.deltaTime;
                    // play melee animation
                }

            }
        }				
    }

    void UpdatePathFinding()
    {
//        path = NavMesh2D.GetSmoothedPath(transform.position, Target.transform.position);
    }

    public float RandomSwingTime;

    void Shoot()
    {
        if (!Dead)
        {
            if (shootBurst)
            {
                if (shotsfired < 3)
                {
                    if (bursttimer > 0)
                    {
                        bursttimer -= 2f * Time.deltaTime;
                    }
                    if (bursttimer <= 0)
                    {
                        EnemyBullet temp = bulletPrefab.Spawn(ShootFrom[2].transform.position, ShootFrom[2].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
                        temp.Damage = Damage;
                        temp.spriterenderer.sprite = EnemyBullets[2];
                        audiosource.Play();
                        EnemyBulletCasing[2].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
                        shotsfired += 1;
                        bulletsused += 1;
                        bursttimer = 0.2f;
                        WSanim.SetTrigger("Shoot");
                        WMFanim.SetTrigger("Shoot");
                    }
                }
                else
                {
                    shootBurst = false;
                }
            }
        }

    }

    bool startDeath;

    void CheckifAlive()
    {
        if (health < 0)
        {
            if (enemyclass != EnemyClass.Dog)
            {
                if (BossIcon.activeInHierarchy)
                {
                    BossIcon.SetActive(false);
                }

            }
            if (!startDeath)
            {
                rg2d.isKinematic = true;
                StartCoroutine(Death());
                startDeath = true;
                WaveManager.WM.RemoveEnemyFromList();
            }
        }

        if (Dead)
        {

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
            if (enemyclass == EnemyClass.Dog)
            {
                Tail.enabled = false;
            }

        }

    }

    IEnumerator Death()
    {
        if (isBoss)
        {
            Stats.BossesKilled += 1;
            CharacterStats.CS.Cash += 30;
        }
        if (isBrute)
        {
            Stats.BrutesKilled += 1;
            CharacterStats.CS.Cash += 10;
        }

        foreach (GameObject weapons in WeaponOfChoice)
        {
            weapons.SetActive(false);
        }
        //		health = 0;
        AimingGO.SetActive(false);
        Warning.SetActive(false);
        SpawnWeapon = Random.Range(0, WeaponChanceMax);
        if (SpawnWeapon == 2)
        {
            if (enemyclass == EnemyClass.StandardGuard)
            {
                WeaponToSpawn[1].Spawn(transform.position, transform.rotation);
            }
            if (enemyclass == EnemyClass.StandardGuardBoss)
            {
                WeaponToSpawn[1].Spawn(transform.position, transform.rotation);
            }
            if (enemyclass == EnemyClass.Captain)
            {
                WeaponToSpawn[3].Spawn(transform.position, transform.rotation);
            }
            if (enemyclass == EnemyClass.Swat)
            {
                WeaponToSpawn[2].Spawn(transform.position, transform.rotation);
            }
        }

        if (Tutorial.AllowCombo)
        {
            CharacterStats.CS.ComboTimer = 40;

            CharacterStats.Combo += 1;
            float ScoreGiven = (10 * CharacterStats.Combo) * WaveManager.ScoreMultiplier;
            Transform Scoretemp = CharacterStats.CS.ScoreCollectedText.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + 0.4f, transform.position.z), transform.rotation) as Transform;
            CSScoreCollected.text = ScoreGiven.ToString();
            CSScoreCollected.color = Color.white;
            Scoretemp.SetParent(GameObject.Find("PlayersHUD").transform);
            Scoretemp.transform.localScale = new Vector2(1f, 1f);
            CharacterStats.Score += (int)ScoreGiven;
            CharacterStats.CS.Cash += 1;
        }

        Col.enabled = false;
        ChildCol.enabled = false;
        Dead = true;
        BloodSplat.SetActive(true);
        Shadow.SetActive(false);
        showgrey = true;
        Transform temp = BloodStain.Spawn(transform.position, transform.rotation) as Transform;
        temp.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
        temp.localScale = temp.localScale * Random.Range(0.5f, 1);
        Stats.Kills += 1;
        yield return new WaitForSeconds(5);
        this.Recycle();
    }

    IEnumerator Reload()
    {
        reload = true;
        // play animation here
        //		Debug.Log (this.name + " is reloading");
        yield return new WaitForSeconds(reloadtime);
        //		Debug.Log (this.name + " has finished reloading");
        //stop animation here
        reload = false;

    }

    IEnumerator Warned()
    {
        Warning.SetActive(true);
        yield return new WaitForSeconds(2);
        Warning.SetActive(false);
    }

    public float EnemyMovement;

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("ShotFire"))
        //{
        //    seenPlayer = true;
        //}

    }

    GameObject FindClosestPlayer()
    {

        GameObject gos;

        gos = CharacterStats.CS.gameObject;

        float distance = float.MaxValue;
        Vector3 position = transform.position;


        Vector3 diff = gos.transform.position - position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < distance)
        {

            Target = gos;
            distance = curDistance;
        }
				
        return Target;

    }

    public void DamagedByPlayer(float dmg, bool crit, bool dmgBoost)
    {

        health -= dmg;
        ChildAnimator.SetTrigger("GetHurt");

        Transform temp = CharacterStats.CS.hittext.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + 0.8f, transform.position.z), transform.rotation);
        temp.SetParent(WorldCanvas.transform,false);

        if (!crit)
        {
            hitSpawnText.text = dmg.ToString();
            if (!dmgBoost)
            {
                hitSpawnText.color = Color.white;
            }
            else
            {
                hitSpawnText.color = Color.yellow;
            }
        }
        else
        {
            hitSpawnText.text = "CRITICAL\n " + dmg.ToString();
            hitSpawnText.color = Color.red;
        }

//        if (health < 0)
//        {
//            if (Tutorial.AllowCombo)
//            {
//                CharacterStats.CS.ComboTimer = 40;
//                CharacterStats.Combo += 1;
//                int ScoreGiven = 10 * CharacterStats.Combo;
////				Transform Scoretemp = CS.ScoreCollectedText.Spawn (new Vector3 (transform.position.x + Random.Range (-0.2f, 0.2f), transform.position.y + 0.4f, transform.position.z), transform.rotation) as Transform;
//                ScoreGOAnim.SetTrigger("Score");
//                ScoreGO.text = ScoreGiven.ToString();
//                ScoreGO.color = Color.white;
////				Scoretemp.SetParent (GameObject.Find ("PlayersHUD").transform);
////				Scoretemp.transform.localScale = new Vector2 (1f, 1f);
//                CharacterStats.Score += ScoreGiven;
//            }
//        }
    }
}
