using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class attackPlayer : MonoBehaviour
{
    public Enemy EnemyType;
    public float MoveSpeed;
    public Vector3 targetPosition = new Vector3();
    public Animator anim;
    public bool seenPlayer;

    [HideInInspector]
    public float health = 100;
    float shootTime = 0.0f;
    public Transform[] ShootFrom;
    public int SpawnWeapon;
    public int WeaponChanceMax;
    public GameObject[] WeaponOfChoice;
    public int chooseEnemyAtStartUp;

    public Transform childRandom;
    public Transform ChildSprite;
    public Sprite[] EnemySprite;
    float Damage;
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


    void Awake()
    {
        ChildSpriteRenderer = ChildSprite.GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
        rg2d = GetComponent<Rigidbody2D>();

        ChildCol = ChildSprite.GetComponent<Collider2D>();
        Col = GetComponent<Collider2D>();

        ChildAnimator = ChildSprite.GetComponent<Animator>();

        if (!CharacterStats.CS)
            return;

        CSScoreCollected = CharacterStats.CS.ScoreCollectedText.GetComponent<Text>();
        hitSpawnText = CharacterStats.CS.hittext.GetComponent<Text>();
        ScoreGO = CharacterStats.CS.ScoreGO.GetComponent<Text>();
        ScoreGOAnim = CharacterStats.CS.ScoreGO.GetComponent<Animator>();
    }

    void OnEnable()
    {
        ResetEnemy();
        BossIcon.SetActive(EnemyType.enemyType == Enemy.EnemyType.Boss);

        FindClosestPlayer();
        RandomSwingTime = Random.Range(1.6f, 2.5f);
        // transform.SetParent(GameObject.Find("EnemiesParent").transform);

        // if (Target != null)
        // {
        //     targetPosition = Target.transform.position;
        // }
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
        Damage = EnemyType.Damage + Random.Range((EnemyType.Damage - EnemyType.Damage / 10), (EnemyType.Damage + EnemyType.Damage / 10)) * WaveManager.DamageMultiplier;
        ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemyType.EnemySprite;

        if (EnemyType.name == "StandardGuard")
        {
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[Random.Range(0, 3)];
            if (EnemyType.enemyAttackType == Enemy.EnemyAttackType.Melee)
            {
                WeaponHandler(0);
            }
            else if (EnemyType.enemyAttackType == Enemy.EnemyAttackType.Gun)
            {
                WeaponHandler(1);
                WSanim = WeaponShoot[0];
                WMFanim = WeaponMuzzleFlash[0];
            }
        }
        else if (EnemyType.name == "StandardGuardBrute")
        {
            ChildSprite.GetComponent<SpriteRenderer>().sprite = EnemySprite[Random.Range(0, 3)];
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            ChildSprite.transform.position = new Vector2(ChildSprite.transform.position.x, ChildSprite.transform.position.y + 0.2f);
            chooseEnemyAtStartUp = Random.Range(0, 2);

            if (EnemyType.enemyAttackType == Enemy.EnemyAttackType.Melee)
            {
                WeaponHandler(0);
            }
            else if (EnemyType.enemyAttackType == Enemy.EnemyAttackType.Gun)
            {
                WeaponHandler(1);
                WSanim = WeaponShoot[0];
                WMFanim = WeaponMuzzleFlash[0];
            }
        }
        else if (EnemyType.name == "Captain")
        {

            WeaponHandler(3);
            WSanim = WeaponShoot[1];
            WMFanim = WeaponMuzzleFlash[1];
        }
        else if (EnemyType.name == "CaptainBrute")
        {
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            WeaponHandler(3);
            WSanim = WeaponShoot[1];
            WMFanim = WeaponMuzzleFlash[1];
        }
        else if (EnemyType.name == "Riot")
        {
            WeaponHandler(6);
        }
        else if (EnemyType.name == "RiotStun")
        {
            WeaponHandler(7);
        }
        else if (EnemyType.name == "RiotBrute")
        {
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            WeaponHandler(6);
        }
        else if (EnemyType.name == "Swat")
        {
            WeaponHandler(2);
            WSanim = WeaponShoot[2];
            WMFanim = WeaponMuzzleFlash[2];
        }
        else if (EnemyType.name == "SwatDemolition")
        {
        }
        else if (EnemyType.name == "SwatBrute")
        {
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.3f;
            WeaponHandler(2);
            WSanim = WeaponShoot[2];
            WMFanim = WeaponMuzzleFlash[2];
        }
        else if (EnemyType.name == "Sniper")
        {
            WeaponHandler(4);
            WSanim = WeaponShoot[3];
            WMFanim = WeaponMuzzleFlash[3];
        }
        else if (EnemyType.name == "Warden")
        {
            WeaponHandler(5);
            WSanim = WeaponShoot[4];
            WMFanim = WeaponMuzzleFlash[4];
        }
        else if (EnemyType.enemyType == Enemy.EnemyType.Zombie)
        {
            WeaponHandler(0);
        }
    }

    void WeaponHandler(int ID)
    {
        foreach (GameObject weapons in WeaponOfChoice)
        {
            WeaponInt.Add(WeaponOfChoice[ID]);
            weapons.SetActive(false);
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
        if (!CharacterStats.CS)
            return;

        if (!CharacterStats.CS.Dead)
        {
            if (!PauseMenu.isPaused && CharacterStats.allowMovement)
            {
                ChildSpriteRenderer.material.SetFloat("_GrayScale", greyAmount);
                if (EnemyType.enemyType == Enemy.EnemyType.Dog)
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

                    if (!Dead)
                    {
                        CheckAttackDistance();
                        Shoot();
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
        CheckifAlive();
    }


    void ResetEnemy()
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

        if (EnemyType.enemyType == Enemy.EnemyType.Dog)
            Tail.GetComponent<SpriteRenderer>().material.SetFloat("_EffectAmount", 0);

        Dead = false;
    }

    void CheckAttackDistance()
    {
        anim.SetBool("Walk", Vector2.Distance(transform.position, targetPosition) > 1);
        if (Vector2.Distance(transform.position, targetPosition) > 1)
        {
            if (Random.Range(0, 3) == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemyType.MoveSpeed * Time.deltaTime);
            }
        }
        if (Vector2.Distance(transform.position, Target.transform.position) > EnemyType.AttackDistance)
        {
            // transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, EnemyType.MoveSpeed * Time.deltaTime);
        }
        else
        {
            shootTime += 1 * Time.deltaTime;

            if (EnemyType.enemyAttackType == Enemy.EnemyAttackType.Melee)
            {
                if (shootTime >= RandomSwingTime)
                {
                    MeleeAttack();
                }
            }
            else if (EnemyType.enemyAttackType == Enemy.EnemyAttackType.Gun)
            {
                // if gun enemy
                if (shootTime >= EnemyType.ShootRate && !reload)
                {
                    if (EnemyType.name == "StandardGuard" || EnemyType.name == "StandardGuardBrute")
                    {
                        PistolAttack();
                    }

                    if (EnemyType.name == "Captain" || EnemyType.name == "CaptainBrute")
                    {
                        ShotgunAttack();
                    }

                    if (EnemyType.name == "Swat" || EnemyType.name == "SwatBrute")
                    {
                        MachineGunAttack();
                    }

                    if (EnemyType.name == "SwatDemolition")
                    {
                        GrenadeAttack();
                    }

                    if (EnemyType.name == "Sniper")
                    {
                        SniperAttack();
                    }

                    if (EnemyType.name == "Boss")
                    {
                        MiniGunAttack();
                    }

                    shootTime = 0;
                    audiosource.Play();
                    bulletsused += 1;
                }

            }
        }
    }

    void PistolAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom[0].transform.position, ShootFrom[0].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));

        temp.transform.SetParent(null);
        temp.gameObject.SetActive(true);
        temp.Damage = Damage;
        temp.spriterenderer.sprite = EnemyBullets[0];
        audiosource.clip = fire[0];

        EnemyBulletCasing[0].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
    }

    void ShotgunAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-60, Vector3.forward)); // -30 on z
        EnemyBullet temp2 = EnemyType.bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-75, Vector3.forward)); // -30 on z
        EnemyBullet temp3 = EnemyType.bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward)); // 0 on z
        EnemyBullet temp4 = EnemyType.bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-105, Vector3.forward)); // 15 on z
        EnemyBullet temp5 = EnemyType.bulletPrefab.Spawn(ShootFrom[1].transform.position, ShootFrom[1].transform.rotation * Quaternion.AngleAxis(-120, Vector3.forward)); // 30 on z
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

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
        audiosource.clip = fire[1];
    }

    void MachineGunAttack()
    {
        audiosource.clip = fire[2];
        shotsfired = 0;
        shootBurst = true;
        Shoot();
    }

    void GrenadeAttack()
    {
        EnemyBullet temp = Grenade.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + 0.2f, transform.position.z), transform.rotation);
        temp.Grenade = true;
        temp.Damage = Damage;
    }

    void SniperAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom[3].transform.position, ShootFrom[3].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
        temp.Damage = Damage;
        temp.spriterenderer.sprite = EnemyBullets[3];
        EnemyBulletCasing[3].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
        audiosource.clip = fire[3];
    }

    void MiniGunAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom[4].transform.position, ShootFrom[4].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
        temp.Damage = Damage;
        temp.spriterenderer.sprite = EnemyBullets[4];
        EnemyBulletCasing[3].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
        audiosource.clip = fire[4];
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
                        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom[2].transform.position, ShootFrom[2].transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward));
                        temp.Damage = EnemyType.Damage;
                        temp.spriterenderer.sprite = EnemyBullets[2];
                        audiosource.Play();
                        EnemyBulletCasing[2].Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
                        shotsfired += 1;

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
        if (Dead)
        {
            if (!flipped)
            {
                if (CharacterAngle < 90)
                    CharacterAngle += 90 * Time.deltaTime * 2;
                else
                    BloodPool.SetActive(true);
            }
            else
            {
                if (CharacterAngle > -90)
                    CharacterAngle -= 90 * Time.deltaTime * 2;
                else
                    BloodPool.SetActive(true);
            }

            Tail.enabled = EnemyType.enemyType != Enemy.EnemyType.Dog;
        }

        if (health < 0)
        {
            if (EnemyType.enemyType != Enemy.EnemyType.Dog)
            {
                BossIcon.SetActive(BossIcon.activeInHierarchy);
            }
            rg2d.isKinematic = true;
            StartCoroutine(Death());
            WaveManager.WM.RemoveEnemyFromList();
            return;
        }
    }

    IEnumerator Death()
    {
        if (EnemyType.enemyType == Enemy.EnemyType.Boss)
        {
            Stats.BossesKilled += 1;
            CharacterStats.CS.Cash += 30;
        }
        else if (EnemyType.enemyType == Enemy.EnemyType.MiniBoss)
        {
            Stats.BrutesKilled += 1;
            CharacterStats.CS.Cash += 10;
        }

        foreach (GameObject weapons in WeaponOfChoice)
            weapons.SetActive(false);

        AimingGO.SetActive(false);

        if (EnemyType.AllowWeaponDrop)
        {
            SpawnWeapon = Random.Range(0, WeaponChanceMax);
            if (SpawnWeapon == 2)
            {
                EnemyType.WeaponToSpawn.Spawn(transform.position, transform.rotation);
            }
        }

        PlayerComboHandler();
        ColliderHandler();

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

    void ColliderHandler()
    {
        Col.enabled = false;
        ChildCol.enabled = false;
    }

    void PlayerComboHandler()
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

    IEnumerator Reload()
    {
        reload = true;
        yield return new WaitForSeconds(EnemyType.reloadtime);
        reload = false;

    }

    GameObject FindClosestPlayer()
    {

        GameObject gos;

        gos = GameObject.FindGameObjectWithTag("Player");

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
                hitSpawnText.color = Color.white;
            else
                hitSpawnText.color = Color.yellow;
        }
        else
        {
            hitSpawnText.text = "CRITICAL\n " + dmg.ToString();
            hitSpawnText.color = Color.red;
        }
    }
}
