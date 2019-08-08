using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBullet : MonoBehaviour
{
    public GameObject bulletImpact;
    public float timer;
    //	EnemyManager eg;
    Transform Player;
    Vector3 Target;
    public float Damage;
    public bool Grenade;
    public Transform Blast;
    bool spawnblast;
    Vector3 halfDist;
    Vector3 Dist;
    bool flip;
    public Transform Shooter;
    public SpriteRenderer spriterenderer;
    Animator animator;
    Rigidbody2D rb2;

    void Awake()
    {
        Blast.CreatePool();
        bulletImpact.transform.CreatePool();
    }

    void OnEnable()
    {
        timer = 0;
    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        Player = CharacterStats.CS.transform;
        Target = Player.position;
        spriterenderer = GetComponent<SpriteRenderer>();
//        transform.rotation = transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward);
        if (Grenade)
        {
            halfDist = Target - transform.position / 2;
            Dist = Target - transform.position;
            if (Dist.x > halfDist.x)
            {
                rb2.AddForce(Vector2.up * 350);
            }
            else
            {
                rb2.AddForce(Vector2.up * 350);
            }
            if (transform.position.x < Target.x)
            {
                flip = true;
            }
            else
            {
                flip = false;
            }
        } 
    }


    void Update()
    {
        CollisionsDetection();

    }

    void CollisionsDetection()
    {
        if (Vector2.Distance(Player.position, transform.position) < CharacterStats.CS.PlayerSettings.ColliderSize)
        {
            this.Recycle();
            CharacterStats.CS.Health -= Damage;
            
            if (CharacterStats.CS.Health > CharacterStats.CS.HealthStarting / 7)
            {
                Invoke("ShowHitEffect", 0.3f);
                CharacterStats.CS.SteroidEffect.SetActive(true);
            }
            if (CharacterStats.CS.Health <= 0)
            {
                if (this.transform.position.x > Player.position.x)
                {
                    Time.timeScale = 0.3f;
                    CharacterStats.flipped = true;
                }
            }
            
            bulletImpact.transform.Spawn(transform.position, transform.rotation);
        }



        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, 1 << LayerMask.NameToLayer("Collisions"));
        if (hitInfo.collider != null)
        {
            if (!Grenade)
            {
                if (hitInfo.transform.tag == "Player")
                {
//					this.Recycle();
//					CS.Health -= Damage;
//
//					if (CS.Health > CS.HealthStarting / 7)
//					{
//						Invoke("ShowHitEffect", 0.3f);
//						CS.SteroidEffect.SetActive(true);
//					}
//					if (CS.Health <= 0)
//					{
//						if (this.transform.position.x > hitInfo.transform.position.x)
//						{
//							Time.timeScale = 0.3f;
//							CharacterStats.flipped = true;
//						}
//					}
//
//					bulletImpact.transform.Spawn(transform.position, transform.rotation);

                }

            }

        }
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {

//		transform.position = Vector3.MoveTowards (transform.position, Target, 10 * Time.deltaTime);

        if (!Grenade)
        {
            if (timer > 1f)
            {
                this.Recycle();
                //					timer =0;
            }
            else
            {
                timer += 1 * Time.deltaTime;
            }
            transform.Translate(Vector3.up * -0.2f);
        }
        else
        {
            if (transform.position.y < Target.y - 0.5f)
            {
                rb2.isKinematic = true;
            }


            if (Mathf.Abs(transform.position.x - Target.x) < 0.01f && Mathf.Abs(transform.position.y - Target.y) < 0.01f)
            {
			
//				animator.SetTrigger ("Drop");
//				rb2.isKinematic = true;
                if (timer > 3.3f)
                {
					
                    this.Recycle();
//					timer =0;
                }
                else
                {
                    timer += 1 * Time.deltaTime;
                }
//				Destroy (this.gameObject, 3.3f);
                Invoke("SpawnBlast", 3);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, 6 * Time.deltaTime);
                if (flip)
                {
                    transform.Rotate(0, 0, -10);
                }
                else
                {
                    transform.Rotate(0, 0, 10);
                }
            }
        }

    }

    void SpawnBlast()
    {
        Shake.shake = 1;
        if (!spawnblast)
        {
            Blast.Spawn(transform.position, transform.rotation);
//			temp.Damage = Damage;

            spawnblast = true;
        }
    }



    public void ShowHitEffect()
    {

        CharacterStats.CS.SteroidEffect.SetActive(false);

	
    }

    private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
    {
        // calculate vectors
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0f;
		
        // calculate xz and y
        float y = toTarget.z;
        float xz = toTargetXZ.magnitude;
		
        // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
        // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
        // so xz = v0xz * t => v0xz = xz / t
        // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
        float t = timeToTarget;
        float v0y = y / t + 0.5f * 9 * t;
        float v0xz = xz / t;
		
        // create result vector for calculated starting speeds
        Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
        result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
        result.y = v0y;                                // set y to v0y (starting speed of y plane)
		
        return result;
    }

	
}
