using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class attackPlayer : MonoBehaviour
{
    public Enemy EnemyType;
    [HideInInspector]
    public Vector3 targetPosition = new Vector3();
    public Animator anim;
    [HideInInspector]
    public bool seenPlayer = true;

    [HideInInspector]

    public float health = 100;
    [HideInInspector]
    float shootTime = 0.0f;
    Transform ShootFrom;
    public int WeaponChanceMax;
    public GameObject ChosenWeapon;
    public Transform ChildSprite;
    float Damage;
    [HideInInspector]
    public bool reload;
    [HideInInspector]
    public int bulletsused;
    public float bursttimer = 0.2f;
    bool shootBurst;
    int shotsfired;
    int WeaponID;
    Animator WSanim;
    Collider2D MeleeCollider;
    Animator WMFanim;
    public GameObject Shadow;
    bool showgrey;
    public GameObject DyingGO;
    public GameObject BloodSplat;
    [HideInInspector]
    public bool flipped;
    [HideInInspector]
    public float greyAmount;
    [HideInInspector]
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
    [HideInInspector]
    public float flipLook;
    Rigidbody2D rg2d;
    [HideInInspector]
    public SpriteRenderer ChildSpriteRenderer;
    public LayerMask ObstacleLayer;
    EnergyBar healthBar;

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
        hitSpawnText = CharacterStats.CS.hittext.GetComponentInChildren<Text>();
        ScoreGO = CharacterStats.CS.ScoreGO.GetComponent<Text>();
        ScoreGOAnim = CharacterStats.CS.ScoreGO.GetComponent<Animator>();

        WSanim = ChosenWeapon.GetComponent<Animator>();
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<EnergyBar>();
        healthBar.valueMax = (int)(EnemyType.Health * WaveManager.HealthMultiplier);
        healthBar.valueCurrent = (int)(EnemyType.Health * WaveManager.HealthMultiplier);
    }

    void OnEnable()
    {
        ResetEnemy();
        if (BossIcon)
            BossIcon.SetActive(EnemyType.enemyType == Enemy.EnemyType.Boss);

        FindClosestPlayer();
        RandomSwingTime = Random.Range(1.6f, 2.5f);

        transform.position = new Vector3(transform.position.x + Random.Range(3, -3), transform.position.y + Random.Range(3, -3), transform.position.z);
        shootTime = 0;

        ChooseEnemyType();

        AimingGO.GetComponent<LookTowardsPlayer>().Target = Target;

    }

    void ChooseEnemyType()
    {
        health = EnemyType.Health * WaveManager.HealthMultiplier;
        Damage = EnemyType.Damage + Random.Range((EnemyType.Damage - EnemyType.Damage / 10), (EnemyType.Damage + EnemyType.Damage / 10)) * WaveManager.DamageMultiplier;
        ChildSpriteRenderer.sprite = EnemyType.EnemySprite[Random.Range(0, EnemyType.EnemySprite.Length)];
        DyingGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ChildSpriteRenderer.sprite;
        WMFanim = ChosenWeapon.GetComponent<Animator>();
        ShootFrom = EnemyType.enemyAttackType == Enemy.EnemyAttackType.Melee || EnemyType.enemyType == Enemy.EnemyType.Demolition
        || EnemyType.name == "StandardGuardBrute" || EnemyType.name == "SwatDemolition" || EnemyType.name == "RiotBrute" ? null : ChosenWeapon.transform.GetChild(1);

        if (EnemyType.name == "StandardGuardMelee")
        {
            AssignMeleeCollider(0);
        }
        else if (EnemyType.name == "StandardGuardBrute")
        {
            AssignMeleeCollider(0);
        }
        else if (EnemyType.name == "Riot")
        {
            AssignMeleeCollider(6);
        }
        else if (EnemyType.name == "RiotStun")
        {
            AssignMeleeCollider(7);
        }
        else if (EnemyType.enemyType == Enemy.EnemyType.Dog)
        {
            AssignMeleeCollider(0);
        }

        if (EnemyType.enemyType == Enemy.EnemyType.MiniBoss)
        {
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.2f;
            ChildSprite.transform.position = new Vector2(ChildSprite.transform.position.x, ChildSprite.transform.position.y + 0.2f);
        }
        if (EnemyType.enemyType == Enemy.EnemyType.Boss)
        {
            ChildSprite.transform.localScale = ChildSprite.transform.localScale * 1.5f;
            ChildSprite.transform.position = new Vector2(ChildSprite.transform.position.x, ChildSprite.transform.position.y + 0.3f);
        }
    }
    void AssignMeleeCollider(int i)
    {
        MeleeCollider = ChosenWeapon.GetComponent<Collider2D>();
        ChosenWeapon.GetComponent<MeleeWeapon>().isStunWand = EnemyType.enemyType == Enemy.EnemyType.StunWand;
    }

    // Use this for initialization
    void Start()
    {
        StaticVariables.EnemySpeed = 1;
    }

    void ChooseRandomPosition()
    {
        float rad = EnemyType.AttackDistance;

        //summon the enemies around this central GameObject
        // float radian = i * Mathf.PI / (5 / 2);
        Vector3 ePosition = new Vector3(rad * Mathf.Cos(2), rad * Mathf.Sin(2), transform.position.z);
        // EnemiesSpawned[i].position = ePosition;
        // print(ePosition);
        if (!System.Single.IsNaN(ePosition.x) && !System.Single.IsNaN(ePosition.y))
        {
            // if (Random.Range(0, 10) == 0)
            // {
            targetPosition = Target.transform.position + ePosition;
            // }
        }
    }

    void FixedUpdate()
    {

        if (!CharacterStats.CS)
            return;

        CheckifAlive();

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

                    if (!Dead || health > 0)
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
        Col.enabled = true;
        ChildCol.enabled = true;
        Shadow.SetActive(true);
        showgrey = false;

        ChildSpriteRenderer.material.SetFloat("_EffectAmount", 0);

        if (EnemyType.enemyType == Enemy.EnemyType.Dog)
            Tail.GetComponent<SpriteRenderer>().material.SetFloat("_EffectAmount", 0);

        Dead = false;
    }

    void CheckObstacle(bool inPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (transform.position - Target.transform.position), 5, ObstacleLayer);
        if (inPosition)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            if (hit.transform != null)
            {
                if (hit.collider.CompareTag("Wall"))
                    anim.SetBool("Walk", false);
            }
            else
                anim.SetBool("Walk", true);
        }
    }
    public bool InPosition;
    float randomMoveTimer = 0;
    void CheckAttackDistance()
    {
        if (randomMoveTimer > 0)
        {
            randomMoveTimer -= Time.deltaTime;
        }
        else
        {
            randomMoveTimer = 2;
            ChooseRandomPosition();
        }

        InPosition = Vector2.Distance(transform.position, targetPosition) <= 0.2f;
        if (Vector2.Distance(transform.position, targetPosition) > 0.2f)
        {
            float moveSpeed = Vector2.Distance(transform.position, Target.transform.position) > 16 ? EnemyType.MoveSpeed * 5 : EnemyType.MoveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            CheckObstacle(false);
        }
        else
            CheckObstacle(true);

        if (Vector2.Distance(transform.position, Target.transform.position) <= (EnemyType.AttackDistance + 1.1f))
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

                    if (EnemyType.name == "Warden")
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

    void MeleeAttack()
    {
        MeleeWeapon meleeScript = ChosenWeapon.GetComponent<MeleeWeapon>();
        meleeScript.Attack();
        shootTime = 0;
    }

    void PistolAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));

        temp.transform.SetParent(null);
        temp.gameObject.SetActive(true);
        temp.Damage = Damage;
        temp.spriterenderer.sprite = EnemyType.BulletSprite;
        audiosource.clip = EnemyType.AttackSound;

        EnemyType.BulletCasing.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
    }

    void ShotgunAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(60, Vector3.forward)); // -30 on z
        EnemyBullet temp2 = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(75, Vector3.forward)); // -30 on z
        EnemyBullet temp3 = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward)); // 0 on z
        EnemyBullet temp4 = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(105, Vector3.forward)); // 15 on z
        EnemyBullet temp5 = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(120, Vector3.forward)); // 30 on z
        temp.Damage = Damage;
        temp2.Damage = Damage;
        temp3.Damage = Damage;
        temp4.Damage = Damage;
        temp5.Damage = Damage;
        temp.spriterenderer.sprite = EnemyType.BulletSprite;
        temp2.spriterenderer.sprite = EnemyType.BulletSprite;
        temp3.spriterenderer.sprite = EnemyType.BulletSprite;
        temp4.spriterenderer.sprite = EnemyType.BulletSprite;
        temp5.spriterenderer.sprite = EnemyType.BulletSprite;
        EnemyType.BulletCasing.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
        audiosource.clip = EnemyType.AttackSound;
    }

    void MachineGunAttack()
    {
        audiosource.clip = EnemyType.AttackSound;
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
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
        temp.Damage = Damage;
        temp.spriterenderer.sprite = EnemyType.BulletSprite;
        EnemyType.BulletCasing.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
        audiosource.clip = EnemyType.AttackSound;
    }

    void MiniGunAttack()
    {
        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
        temp.Damage = Damage;
        temp.spriterenderer.sprite = EnemyType.BulletSprite;
        EnemyType.BulletCasing.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);

        WSanim.SetTrigger("Shoot");
        WMFanim.SetTrigger("Shoot");
        audiosource.clip = EnemyType.AttackSound;
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
                        EnemyBullet temp = EnemyType.bulletPrefab.Spawn(ShootFrom.transform.position, ShootFrom.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
                        temp.Damage = EnemyType.Damage;
                        temp.spriterenderer.sprite = EnemyType.BulletSprite;
                        audiosource.Play();
                        EnemyType.BulletCasing.Spawn(new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation);
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

    bool startDeath;

    void CheckifAlive()
    {

        healthBar.valueCurrent = (int)health;
        healthBar.gameObject.SetActive(healthBar.valueCurrent < healthBar.valueMax && health > 0);
        if (Dead)
        {
            if (!flipped)
            {
                if (CharacterAngle < 90)
                    CharacterAngle += 90 * Time.deltaTime * 2;

            }
            else
            {
                if (CharacterAngle > -90)
                    CharacterAngle -= 90 * Time.deltaTime * 2;

            }
            if (EnemyType.enemyType == Enemy.EnemyType.Dog)
                Tail.gameObject.SetActive(false);
        }
    }

    IEnumerator Death()
    {
        if (Random.Range(0, 10) == 1 )
            EnemyType.DroppedItem.Spawn(transform.position + (Vector3)Random.insideUnitCircle * 2);
        CharacterStats.CS.Cash += EnemyType.CashToGive;
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

        AimingGO.SetActive(false);

        if (EnemyType.AllowWeaponDrop && !CharacterStats.CS.Special)
        {
            if (Random.Range(0, WeaponChanceMax) == 2 )
            {
                EnemyType.WeaponToSpawn.Spawn(transform.position, transform.rotation);
            }
        }

        PlayerComboHandler();
        ColliderHandler();

        BloodSplat.SetActive(true);
        BloodSplat.GetComponentInChildren<SpriteRenderer>().sortingOrder = ChildSpriteRenderer.sortingOrder - 1;
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

        CharacterStats.Combo++;
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
        if (ChildAnimator)
            ChildAnimator.SetTrigger("GetHurt");
        if (WorldCanvas == null)
        {
            WorldCanvas = transform.GetChild(0).gameObject;
        }
        else
        {
            Transform temp = CharacterStats.CS.hittext.Spawn(new Vector3(WorldCanvas.transform.position.x + Random.Range(-5f, 5f), WorldCanvas.transform.position.y + 0.8f, WorldCanvas.transform.position.z), transform.rotation);
            temp.SetParent(WorldCanvas.transform, false);
            temp.localPosition = Vector3.zero;
        }

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

        if (health <= 0)
        {
            if (EnemyType.enemyType != Enemy.EnemyType.Dog)
            {
                if (BossIcon != null)
                {
                    BossIcon.SetActive(BossIcon.activeInHierarchy);
                }
            }
            if (WaveManager.WM.ZombieMode)
            {
                StatManager.Instance.ZombieKills += 1;
                PlayerPrefs.SetInt("ZombiesKilled", StatManager.Instance.ZombieKills);
            }
            Dead = true;
            rg2d.isKinematic = true;
            StartCoroutine(Death());
            WaveManager.WM.RemoveEnemyFromList();
            return;
        }
    }
    bool zombie;
}