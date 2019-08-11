using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeWeapon : MonoBehaviour {
    public int Damage;
    public Transform Hit;
    public Transform KnifeHit;

    //pipe animator
    public Animator animPipe;
    //  public Transform RotationObject;
    public float MeleeInterval = 0.5f;
    private float MeleeTime = 0.0f;
    public AudioClip[] MeleeSounds;
    public AudioSource audioSource;
    public enum WhichHolder {
        Enemy,
        Player
    }

    public WhichHolder whichholder;
    public GameObject ParentGO;
    attackPlayer ParentGOScript;
    public Transform hittext;
    public static int DamageBoost = 1;
    int swingAttack;
    [HideInInspector]
    public bool isStunWand;
    [HideInInspector]
    public Collider2D[] selfCollider;
    public SpriteRenderer[] selfSprite;
    Collider2D ParentCollider;

    AudioClip attackSound;

    void Awake () {
        Hit.CreatePool ();
        KnifeHit.CreatePool ();
        hittext.CreatePool ();

    }

    // Use this for initialization
    void Start () {
        CharacterStats.CS.ScoreCollectedText.CreatePool ();
        if (transform.root.tag == "Enemy")
            whichholder = WhichHolder.Enemy;

        if (GetComponents<Collider2D> ().Length > 0)
            selfCollider = GetComponents<Collider2D> ();
        else
            selfCollider = GetComponentsInChildren<Collider2D> ();

        if (GetComponents<SpriteRenderer> ().Length > 0)
            selfSprite = GetComponents<SpriteRenderer> ();
        else
            selfSprite = GetComponentsInChildren<SpriteRenderer> ();

        animPipe = GetComponent<Animator> ();
        audioSource = GetComponent<AudioSource> ();
        ParentGO = transform.parent.parent.gameObject;
        ParentCollider = ParentGO.GetComponent<Collider2D> ();

        if (whichholder == WhichHolder.Enemy) {
            ParentGOScript = ParentGO.GetComponent<attackPlayer> ();

            isStunWand = ParentGOScript.EnemyType.enemyType == Enemy.EnemyType.StunWand;

        }

        attackSound = CharacterStats.CS.impact[0];
        MeleeSounds = CharacterStats.CS.impact;
    }

    bool leftPunch;
    public void Attack () {
        if (PauseMenu.isPaused)
            return;
        if (CharacterStats.CS.Dead)
            return;

        switch (whichholder) {
            case WhichHolder.Enemy:

                // audioSource = GetComponent<AudioSource>();
                audioSource.clip = attackSound;
                audioSource.Play (); // swoosh sound
                animPipe.SetTrigger ("Melee");
                CollisionDetectionEnemy ();
                break;

            case WhichHolder.Player:
                if (CharacterStats.CS.stunned || !CharacterStats.CS.CanShoot)
                    return;

                if (Time.time >= MeleeTime) {
                    leftPunch = !leftPunch;
                    swingAttack = Random.Range (1, 3);
                    CollisionDetection ();
                    audioSource.clip = MeleeSounds[Random.Range (4, 5)];
                    audioSource.Play ();

                    // attack here
                    if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Fist) {
                        CharacterStats.CS.Stamina -= 5;
                        if (CharacterStats.CS.Stamina > 5) {

                            if (StaticVariables.MovementMultiply == 1) {
                                MeleeInterval = 0.2f;
                            } else {
                                MeleeInterval = 0.2f / StaticVariables.MovementMultiply;
                            }
                        } else {
                            MeleeInterval = 1f;
                        }
                        switch (leftPunch) {

                            case true:
                                animPipe.SetTrigger ("rightHook");
                                break;
                            case false:
                                animPipe.SetTrigger ("leftHook");
                                break;
                                // case 3:
                                //     animPipe.SetTrigger("finalPunch");

                                //     break;

                        }
                    }

                    if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Pipe) {
                        CharacterStats.CS.Stamina -= 10;
                        if (CharacterStats.CS.Stamina > 5) {
                            if (StaticVariables.MovementMultiply == 1) {
                                MeleeInterval = 0.4f;
                            } else {
                                MeleeInterval = 0.4f / StaticVariables.MovementMultiply;
                            }
                        } else {
                            MeleeInterval = 1.2f;
                        }
                        animPipe.SetTrigger ("Melee");
                    }
                    if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Knife) {
                        CharacterStats.CS.Stamina -= 10;
                        if (StaticVariables.MovementMultiply == 1) {
                            MeleeInterval = 0.4f;
                        } else {
                            MeleeInterval = 0.4f / StaticVariables.MovementMultiply;
                        }
                        animPipe.SetTrigger ("Melee");
                    }
                    MeleeTime = Time.time + MeleeInterval;
                }

                break;

        }

    }

    int hits;

    private void Update () {
    }

    public void CollisionDetectionEnemy () {
        ////Debug.Log("hits " + hits);
        Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, 1f, 1 << LayerMask.NameToLayer ("Player"));

        foreach (Collider2D target in colliders) {

            if (target == null)
                return;

            // Guards ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (whichholder == WhichHolder.Enemy) {

                //RaycastHit2D target = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, 1 << LayerMask.NameToLayer("Collisions"));
                if (target.transform.tag == "Player") {
                    foreach (Collider2D col in selfCollider)
                        col.enabled = false;

                    Shake.shake = 0.1f;
                    Transform soundTemp = CharacterStats.CS.ImpactSound.Spawn (transform.position, transform.rotation);
                    soundTemp.GetComponent<AudioSource> ().clip = MeleeSounds[Random.Range (0, 3)];
                    soundTemp.GetComponent<AudioSource> ().Play ();
                    Invoke ("ShowHitEffect", 0.3f);
                    CharacterStats.CS.SteroidEffect.SetActive (true);

                    if (!isStunWand) {
                        CharacterStats.CS.Damaged (ParentGOScript.EnemyType.Damage * WaveManager.DamageMultiplier);
                        Hit.Spawn (target.transform.position, target.transform.rotation); // hit effect
                    } else {
                        CharacterStats.CS.Health -= 10;
                        CharacterStats.CS.stunned = true;
                        CharacterStats.CS.anim.SetTrigger ("Stunned");
                    }

                    if (CharacterStats.CS.Health <= 0) {
                        if (this.transform.position.x < target.transform.position.x) {
                            CharacterStats.flipped = true;
                        }
                    }
                    // Time.timeScale = 0.3f;
                }
            }

        }

    }

    public void CollisionDetection () {
        ////Debug.Log("hits " + hits);
        Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, 1f, 1 << LayerMask.NameToLayer ("Enemy"));

        foreach (Collider2D target in colliders) {

            if (target == null)
                return;

            if (target == ParentCollider)
                return;

            Transform soundTemp = CharacterStats.CS.ImpactSound.Spawn (transform.position, transform.rotation);
            soundTemp.GetComponent<AudioSource> ().clip = MeleeSounds[Random.Range (0, 3)];
            soundTemp.GetComponent<AudioSource> ().Play ();
            //RaycastHit2D[] target = Physics2D.RaycastAll(transform.position, Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Collisions"));

            criticalHit = Random.Range (1, 10);

            float criticaldmg = CharacterStats.CS.Damage * Random.Range (2, 3) * DamageBoost;
            float criticaldmgLowStamina = (CharacterStats.CS.Damage / 10) * Random.Range (2, 3) * DamageBoost;
            float dmg = CharacterStats.CS.Damage * DamageBoost;
            float dmgLowStamina = (CharacterStats.CS.Damage / 10) * DamageBoost;

            if (target.transform.tag == "Enemy") {
                attackPlayer enemyScript = target.transform.GetComponent<attackPlayer> ();
                Shake.shake = 0.1f;

                foreach (Collider2D col in selfCollider)
                    col.enabled = false;

                criticalHit = Random.Range (1, 10);
                if (criticalHit == 1) {
                    Stats.Criticals += 1;
                    if (enemyScript.health < 1) {
                        // Time.timeScale = 0.3f;
                        //Debug.Log("SLOW MOTION");
                    }
                    if (CharacterStats.CS.Stamina > 5) {

                        if (DamageBoost < 2) {
                            enemyScript.DamagedByPlayer (criticaldmg, true, false);
                        } else {
                            enemyScript.DamagedByPlayer (criticaldmg, true, true);
                        }
                    } else {

                        if (DamageBoost < 2) {
                            enemyScript.DamagedByPlayer ((criticaldmg / 5), true, false);
                        } else {
                            enemyScript.DamagedByPlayer ((criticaldmg / 5), true, true);
                        }
                    }

                } else {
                    if (CharacterStats.CS.Stamina > 5) {
                        if (DamageBoost < 2) {
                            enemyScript.DamagedByPlayer (dmg, false, false);
                        } else {
                            enemyScript.DamagedByPlayer (dmg, false, true);
                        }
                    } else {

                        if (DamageBoost < 2) {
                            enemyScript.DamagedByPlayer ((dmg / 5), false, false);
                        } else {
                            enemyScript.DamagedByPlayer ((dmg / 5), false, true);
                        }
                    }
                    Hit.Spawn (new Vector3 (target.transform.position.x + Random.Range (-0.2f, 0.2f), target.transform.position.y + Random.Range (0.3f, 0.8f), target.transform.position.z), target.transform.rotation); // hit effect

                }

                if (enemyScript.health < 1) {

                    if (this.transform.position.x < target.transform.position.x) {
                        enemyScript.flipped = true;
                    }
                }
            }

        }

    }
    int criticalHit;

    public void ShowHitEffect () {
        CharacterStats.CS.SteroidEffect.SetActive (false);
    }

}