using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class attackPlayer : MonoBehaviour
{
    public Enemy EnemyType;
    public float MoveSpeed;
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
        ZombieBrute
    }
    ;

    public EnemyClass enemyclass;
    Vector3 targetPosition = new Vector3();
    public Animator anim;
    public bool seenPlayer;

    [HideInInspector]
    public float health = 100;
    public EnemyBullet bulletPrefab;
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
    public Sprite[] EnemySprite;
    public float Damage;
    public List<GameObject> WeaponInt;
    public Sprite[] EnemyBullets;
    public Transform[] EnemyBulletCasing;
    public bool reload;
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
    }

    void OnEnable()
    {
        BossIcon.SetActive(EnemyType.Boss);


        FindClosestPlayer();
        RandomSwingTime = Random.Range(1.6f, 2.5f);
        Dead = false;
        transform.SetParent(GameObject.Find("EnemiesParent").transform);

        if (Target != null)
        {
            targetPosition = Target.transform.position;
        }
        transform.position = new Vector3(transform.position.x + Random.Range(3, -3), transform.position.y + Random.Range(3, -3), transform.position.z);
        shootTime = 0;

        Quaternion randomRot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        childRandom.transform.rotation = randomRot;

        ChooseEnemyType();



        for (var i = 0; i < WeaponInt.Count; i++)
        {
            WeaponInt[i].SetActive(true);
        }

        AimingGO.GetComponent<LookTowardsPlayer>().Target = Target;

    }

    void ChooseEnemyType()
    {
        health = EnemyType.Health * WaveManager.HealthMultiplier;
        if (enemyclass == EnemyClass.StandardGuard)
        {

            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[Random.Range(0, 3)];
            if (typeofenemy == TypeOfEnemy.Melee)
            {
                this.name = "Standard Guard Melee";
                Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[0]);
                    weapons.SetActive(false);
                }
            }
            if (typeofenemy == TypeOfEnemy.Pistol)
            {
                this.name = "Standard Guard Pistol";
                Damage = Random.Range(35, 45) * WaveManager.DamageMultiplier;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[1]);
                    weapons.SetActive(false);
                }
                WSanim = WeaponShoot[0];
                WMFanim = WeaponMuzzleFlash[0];
            }
        }
        else if (enemyclass == EnemyClass.StandardGuardBoss)
        {

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
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[0]);
                    weapons.SetActive(false);
                }
            }
            if (typeofenemy == TypeOfEnemy.Pistol)
            {
                this.name = "Standard Boss Guard Pistol";
                Damage = Random.Range(35, 45) * WaveManager.DamageMultiplier;
                foreach (GameObject weapons in WeaponOfChoice)
                {
                    WeaponInt.Add(WeaponOfChoice[1]);
                    weapons.SetActive(false);
                }
                WSanim = WeaponShoot[0];
                WMFanim = WeaponMuzzleFlash[0];
            }
        }
        else if (enemyclass == EnemyClass.Captain)
        {
            this.name = "Captain";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[3];
            typeofenemy = TypeOfEnemy.Shotgun;
            Damage = Random.Range(45, 55) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[3]);
                weapons.SetActive(false);
            }
            WSanim = WeaponShoot[1];
            WMFanim = WeaponMuzzleFlash[1];
        }
        else if (enemyclass == EnemyClass.CaptainBoss)
        {
            this.name = "Captain Boss";
            BossIcon.SetActive(true);
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[3];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            typeofenemy = TypeOfEnemy.Shotgun;
            Damage = Random.Range(45, 55) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[3]);
                weapons.SetActive(false);
            }
            WSanim = WeaponShoot[1];
            WMFanim = WeaponMuzzleFlash[1];
        }
        else if (enemyclass == EnemyClass.Riot)
        {
            this.name = "Riot Guard";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[4];
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                WeaponInt.Add(WeaponOfChoice[6]);
                weapons.SetActive(false);
            }
        }
        else if (enemyclass == EnemyClass.RiotStun)
        {
            this.name = "Riot Guard Stun";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[4];
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[7]);
                weapons.SetActive(false);
            }
        }
        else if (enemyclass == EnemyClass.RiotBoss)
        {
            this.name = "Riot Guard Boss";
            BossIcon.SetActive(true);
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[4];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(15, 25) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                WeaponInt.Add(WeaponOfChoice[6]);
                weapons.SetActive(false);
            }
        }
        else if (enemyclass == EnemyClass.Swat)
        {
            this.name = "Swat";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[5];
            typeofenemy = TypeOfEnemy.Machinegun;
            Damage = Random.Range(25, 35) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[2]);
                weapons.SetActive(false);
            }
            WSanim = WeaponShoot[2];
            WMFanim = WeaponMuzzleFlash[2];
        }
        else if (enemyclass == EnemyClass.SwatDemolition)
        {
            this.name = "Swat Demolition";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[10];

            typeofenemy = TypeOfEnemy.Demolition;
            Damage = Random.Range(75, 85) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                weapons.SetActive(false);
            }
        }
        else if (enemyclass == EnemyClass.SwatBoss)
        {
            this.name = "Swat Boss";
            BossIcon.SetActive(true);
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[5];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            typeofenemy = TypeOfEnemy.Machinegun;
            Damage = Random.Range(25, 35) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[2]);
                weapons.SetActive(false);
            }
            WSanim = WeaponShoot[2];
            WMFanim = WeaponMuzzleFlash[2];
        }
        else if (enemyclass == EnemyClass.Sniper)
        {
            this.name = "Sniper Guard";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[7];
            typeofenemy = TypeOfEnemy.Sniper;
            Damage = Random.Range(135, 145) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[4]);
                weapons.SetActive(false);
            }
            WSanim = WeaponShoot[3];
            WMFanim = WeaponMuzzleFlash[3];
        }
        else if (enemyclass == EnemyClass.Warden)
        {
            isBoss = true;
            this.name = "Warden";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[6];
            typeofenemy = TypeOfEnemy.Minigun;
            Damage = Random.Range(5, 15) * WaveManager.DamageMultiplier;

            // adds weapon of choice to list to be active and turns off other weapons
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[5]);
                weapons.SetActive(false);
            }
            WSanim = WeaponShoot[4];
            WMFanim = WeaponMuzzleFlash[4];
        }
        else if (enemyclass == EnemyClass.Dog)
        {
            this.name = "Dog";
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[8];
            typeofenemy = TypeOfEnemy.Melee;
            Damage = Random.Range(10, 20) * WaveManager.DamageMultiplier;
        }

        else if (enemyclass == EnemyClass.StandardGuard || enemyclass == EnemyClass.StandardGuardBoss)
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
        else if (enemyclass == EnemyClass.Zombie)
        {
            this.name = "Zombie";
            Damage = Random.Range(50, 100) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                weapons.SetActive(false);
            }
        }
        else if (enemyclass == EnemyClass.ZombieBrute)
        {
            BossIcon.SetActive(true);
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            ChildSprite.transform.position = new Vector2(ChildSprite.transform.position.x, ChildSprite.transform.position.y + 0.2f);


            this.name = "Zombie Brute";
            Damage = Random.Range(100, 150) * WaveManager.DamageMultiplier;
            foreach (GameObject weapons in WeaponOfChoice)
            {
                WeaponInt.Add(WeaponOfChoice[0]);
                weapons.SetActive(false);
            }
        }
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
                }
                else
                {
                    if (greyAmount < 1)
                    {
                        greyAmount += 1 * Time.deltaTime * 2;
                    }
                }

                if (Target != null)
                {

                    DyingGO.transform.rotation = Quaternion.AngleAxis(CharacterAngle, Vector3.forward);

                    if (bulletsused >= EnemyType.bulletsinclip)
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

    void dmg(int amt)
    {
        health -= amt;
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
        anim.SetBool("Walk", Vector2.Distance(transform.position, Target.transform.position) > EnemyType.AttackDistance);

        if (Vector2.Distance(transform.position, Target.transform.position) > EnemyType.AttackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, EnemyType.MoveSpeed * Time.deltaTime);
        }
        else
        {
            if (EnemyType.enemyType == Enemy.TypeOfEnemy.Melee)
            {
                shootTime += 1 * Time.deltaTime;
                if (shootTime >= RandomSwingTime)
                {
                    MeleeAttack();
                }
            }
            else
            {
                if (shootTime >= EnemyType.ShootRate && !reload)
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

    void MeleeAttack()
    {

        // do melee function here;

        shootTime = 0;

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
        yield return new WaitForSeconds(EnemyType.reloadtime);
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
        temp.SetParent(WorldCanvas.transform, false);

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
