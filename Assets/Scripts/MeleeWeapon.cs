using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MeleeWeapon : MonoBehaviour
{

    public Transform Hit;
    public Transform KnifeHit;

    //pipe animator
    public Animator animPipe;
    //  public Transform RotationObject;
    public float MeleeInterval = 0.5f;
    private float MeleeTime = 0.0f;
    AudioClip[] MeleeSounds;
    AudioSource audioSource;

    public enum WhichHolder
    {
        Enemy,
        Player}
    ;

    public WhichHolder whichholder;
    public GameObject ParentGO;
    attackPlayer ParentGOScript;
    public bool CanSwing;

    public Transform hittext;
    public static int DamageBoost = 1;
    int swingAttack;
    public bool isStunWand;
    Collider2D selfCollider;
    Collider2D ParentCollider;

    void Awake()
    {
        Hit.CreatePool();
        KnifeHit.CreatePool();
        hittext.CreatePool();



    }

    // Use this for initialization
    void Start()
    {
        CharacterStats.CS.ScoreCollectedText.CreatePool();
        if (transform.root.tag == "Enemy")
        {
            whichholder = WhichHolder.Enemy;
        }

        selfCollider = GetComponent<Collider2D>();
        animPipe = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (whichholder == WhichHolder.Player)
        {
            ParentCollider = ParentGO.GetComponent<Collider2D>();
        }
        if (whichholder == WhichHolder.Enemy)
        {
            ParentCollider = ParentGO.GetComponent<Collider2D>();
            ParentGOScript = ParentGO.GetComponent<attackPlayer>();
            if (ParentGOScript.enemyclass == attackPlayer.EnemyClass.RiotStun)
            {
                isStunWand = true;
            }
        }
        MeleeSounds = CharacterStats.CS.impact;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {

            if (selfCollider.enabled)
            {
                Invoke("turnoffcollider", 0.5f);
            }
            switch (whichholder)
            {
                case WhichHolder.Enemy:
                    if (ParentGOScript.CanSwing == true)
                    {
                        CollisionDetectionEnemy();
                        animPipe.SetTrigger("Melee");
                        selfCollider.enabled = true;
                        audioSource.clip = MeleeSounds[Random.Range(4, 5)];
                        audioSource.Play();
                        if (!isStunWand)
                        {
                            GetComponent<TrailRenderer>().enabled = true;
                            Invoke("RemoveTrail", 0.3f);
                        }
                        ParentGOScript.CanSwing = false;
                    }
                    break;
                    
                case WhichHolder.Player:
                    if (CharacterStats.CS.Dead == false && !CharacterStats.CS.stunned)
                    {
                        if (CharacterStats.CS.CanShoot)
                        {
                            if (Tutorial.AllowAttack)
                            {

                                if (Time.time >= MeleeTime)
                                {
                                    swingAttack = Random.Range(1, 4);
                                    CollisionDetection();
                                    audioSource.clip = MeleeSounds[Random.Range(4, 5)];
                                    audioSource.Play();

                                    selfCollider.enabled = true;
                                    MeleeTime = Time.time + MeleeInterval;

                                    // attack here
                                    if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Fist)
                                    {
                                        CharacterStats.CS.Stamina -= 5;
                                        if (CharacterStats.CS.Stamina > 5)
                                        {

                                            if (StaticVariables.MovementMultiply == 1)
                                            {
                                                MeleeInterval = 0.2f;
                                            }
                                            else
                                            {
                                                MeleeInterval = 0.2f / StaticVariables.MovementMultiply;
                                            }
                                        }
                                        else
                                        {
                                            MeleeInterval = 1f;
                                        }
                                        switch (swingAttack)
                                        {

                                            case 1:
                                                animPipe.SetTrigger("rightHook");
                                                GetComponent<TrailRenderer>().enabled = true;
                                                Invoke("RemoveTrail", 0.3f);
                                                break;
                                            case 2:
                                                animPipe.SetTrigger("leftHook");
                                                break;
                                            case 3:
                                                animPipe.SetTrigger("finalPunch");

                                                break;

                                        }
                                        selfCollider.enabled = true;
                                        MeleeTime = Time.time + MeleeInterval;
                                    }

                                    if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Pipe)
                                    {
                                        CharacterStats.CS.Stamina -= 10;
                                        if (CharacterStats.CS.Stamina > 5)
                                        {
                                            GetComponent<TrailRenderer>().enabled = true;
                                            Invoke("RemoveTrail", 0.3f);
                                            if (StaticVariables.MovementMultiply == 1)
                                            {
                                                MeleeInterval = 0.4f;
                                            }
                                            else
                                            {
                                                MeleeInterval = 0.4f / StaticVariables.MovementMultiply;
                                            }
                                        }
                                        else
                                        {
                                            MeleeInterval = 1.2f;
                                        }
                                        animPipe.SetTrigger("Melee");
                                        selfCollider.enabled = true;
                                        MeleeTime = Time.time + MeleeInterval;
                                    }
                                    if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Knife)
                                    {
                                        CharacterStats.CS.Stamina -= 10;
                                        GetComponent<TrailRenderer>().enabled = true;
                                        Invoke("RemoveTrail", 0.3f);
                                        if (StaticVariables.MovementMultiply == 1)
                                        {
                                            MeleeInterval = 0.4f;
                                        }
                                        else
                                        {
                                            MeleeInterval = 0.4f / StaticVariables.MovementMultiply;
                                        }
                                        animPipe.SetTrigger("Melee");
                                        selfCollider.enabled = true;
                                        MeleeTime = Time.time + MeleeInterval;
                                    }

                                }
                            }
                        }
                    }
                    break;

            }


        }
    }

    int hits;

    public void CollisionDetectionEnemy()
    {
        ////Debug.Log("hits " + hits);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f, 1 << LayerMask.NameToLayer("Player"));

        foreach (Collider2D target in colliders)
        {

            if (target != null)
            {
               
                if (target != ParentCollider)
                {

                    // Guards ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (whichholder == WhichHolder.Enemy)
                    {

                        //RaycastHit2D target = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, 1 << LayerMask.NameToLayer("Collisions"));
                        if (target.transform.tag == "Player")
                        {
                            Shake.shake = 0.1f;
                            Transform soundTemp = CharacterStats.CS.ImpactSound.Spawn(transform.position, transform.rotation);
                            soundTemp.GetComponent<AudioSource>().clip = MeleeSounds[Random.Range(0, 3)];
                            soundTemp.GetComponent<AudioSource>().Play();
                            selfCollider.enabled = false;
                            Invoke("ShowHitEffect", 0.3f);
                            CharacterStats.CS.SteroidEffect.SetActive(true);

                            if (!isStunWand)
                            {
                                CharacterStats.CS.Damaged(ParentGOScript.Damage);
                                Hit.Spawn(target.transform.position, target.transform.rotation); // hit effect
                            }
                            else
                            {
                                CharacterStats.CS.Health -= 10;
                                CharacterStats.CS.stunned = true;
                                CharacterStats.CS.GetComponent<Animator>().SetTrigger("Stunned");
                            }

                            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Health <= 0)
                            {
                                if (this.transform.position.x < target.transform.position.x)
                                {
                                    CharacterStats.flipped = true;
                                }
                            }
                            Time.timeScale = 0.3f;
                        }
                    }

                   
                }
            }
        }

    }

    public void CollisionDetection()
    {
        ////Debug.Log("hits " + hits);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f, 1 << LayerMask.NameToLayer("Collisions"));

        foreach (Collider2D target in colliders)
        {

            if (target != null)
            {

                if (target != ParentCollider)
                {

                    // Player /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (whichholder == WhichHolder.Player)
                    {

                        Transform soundTemp = CharacterStats.CS.ImpactSound.Spawn(transform.position, transform.rotation);
                        soundTemp.GetComponent<AudioSource>().clip = MeleeSounds[Random.Range(0, 3)];
                        soundTemp.GetComponent<AudioSource>().Play();
                        //RaycastHit2D[] target = Physics2D.RaycastAll(transform.position, Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Collisions"));

                        criticalHit = Random.Range(1, 10);

                        float criticaldmg = CharacterStats.CS.Damage * Random.Range(2, 3) * DamageBoost;
                        float criticaldmgLowStamina = (CharacterStats.CS.Damage / 10) * Random.Range(2, 3) * DamageBoost;
                        float dmg = CharacterStats.CS.Damage * DamageBoost;
                        float dmgLowStamina = (CharacterStats.CS.Damage / 10) * DamageBoost;

                        if (target.transform.tag == "Enemy")
                        {
                            Shake.shake = 0.1f;
                            selfCollider.enabled = false;
                            target.transform.GetComponent<attackPlayer>().Target = GameObject.FindGameObjectWithTag("Player");

                            criticalHit = Random.Range(1, 10);
                            if (criticalHit == 1)
                            {
                                Stats.Criticals += 1;
                                if (target.transform.GetComponent<attackPlayer>().health < 1)
                                {
                                    Time.timeScale = 0.3f;
                                    //Debug.Log("SLOW MOTION");
                                }
                                if (CharacterStats.CS.Stamina > 5)
                                {

                                    if (DamageBoost < 2)
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer(criticaldmg, true, false);
                                    }
                                    else
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer(criticaldmg, true, true);
                                    }
                                }
                                else
                                {

                                    if (DamageBoost < 2)
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer((criticaldmg / 5), true, false);
                                    }
                                    else
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer((criticaldmg / 5), true, true);
                                    }
                                }

                            }
                            else
                            {
                                if (CharacterStats.CS.Stamina > 5)
                                {
                                    if (DamageBoost < 2)
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer(dmg, false, false);
                                    }
                                    else
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer(dmg, false, true);
                                    }
                                }
                                else
                                {

                                    if (DamageBoost < 2)
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer((dmg / 5), false, false);
                                    }
                                    else
                                    {
                                        target.transform.GetComponent<attackPlayer>().DamagedByPlayer((dmg / 5), false, true);
                                    }


                                }

                                target.transform.GetComponent<attackPlayer>().shootTime = 0;
                                Hit.Spawn(new Vector3(target.transform.position.x + Random.Range(-0.2f, 0.2f), target.transform.position.y + Random.Range(0.3f, 0.8f), target.transform.position.z), target.transform.rotation); // hit effect
                                if (target.transform.GetComponentInParent<attackPlayer>().health <= 0)
                                {

                                    if (this.transform.position.x < target.transform.position.x)
                                    {
                                        target.transform.GetComponent<attackPlayer>().flipped = true;
                                    }
                                }
                            }


                            if (target.transform.GetComponent<attackPlayer>().health < 1)
                            {

                                if (this.transform.position.x < target.transform.position.x)
                                {
                                    target.transform.GetComponent<attackPlayer>().flipped = true;
                                }
                            }
                        }

                        
                    }
                    
                    // Guards ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (whichholder == WhichHolder.Enemy)
                    {

                        //RaycastHit2D target = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, 1 << LayerMask.NameToLayer("Collisions"));
                        if (target.transform.tag == "Player")
                        {
                            Shake.shake = 0.1f;
                            Transform soundTemp = CharacterStats.CS.ImpactSound.Spawn(transform.position, transform.rotation);
                            soundTemp.GetComponent<AudioSource>().clip = MeleeSounds[Random.Range(0, 3)];
                            soundTemp.GetComponent<AudioSource>().Play();
                            selfCollider.enabled = false;
                            Invoke("ShowHitEffect", 0.3f);
                            CharacterStats.CS.SteroidEffect.SetActive(true);

                            if (!isStunWand)
                            {
                                CharacterStats.CS.Damaged(ParentGOScript.Damage);
                                Hit.Spawn(target.transform.position, target.transform.rotation); // hit effect
                            }
                            else
                            {
                                CharacterStats.CS.Health -= 10;
                                CharacterStats.CS.stunned = true;
                                CharacterStats.CS.GetComponent<Animator>().SetTrigger("Stunned");
                            }

                            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().Health <= 0)
                            {
                                if (this.transform.position.x < target.transform.position.x)
                                {
                                    CharacterStats.flipped = true;
                                }
                            }
                            Time.timeScale = 0.3f;
                        }
                    }

                }
            }
        }

    }

    public void RemoveTrail()
    {
        GetComponent<TrailRenderer>().enabled = false;
    }

    public void turnoffcollider()
    {
        if (selfCollider.enabled)
            selfCollider.enabled = false;

    }

    int criticalHit;

    public void ShowHitEffect()
    {
        CharacterStats.CS.SteroidEffect.SetActive(false);
    }

}
