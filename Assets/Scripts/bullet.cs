using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
	public GameObject bulletImpact;
	public GameObject bulletImpact2;
	float timer;
	public Sprite[] BulletSprite;
	int criticalHit;
	public Transform hittext;
	public bool SniperBullet;
	int peopleHit;
	public SpriteRenderer spriterenderer;

	void Awake()
	{
		spriterenderer = GetComponent<SpriteRenderer>();
		hittext.CreatePool();
		CharacterStats.CS.ScoreCollectedText.CreatePool();
		bulletImpact2.transform.CreatePool();
		bulletImpact.transform.CreatePool();
	}

    void OnEnable(){
        Invoke("removego",2);
    }

	// Use this for initialization
	void Start()
	{
		GameObject audiosource = new GameObject();
		audiosource.AddComponent<AudioSource>();
        audiosource.AddComponent<destroyafterseconds>();

		if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Pistol)
		{
			spriterenderer.sprite = BulletSprite[0];
			audiosource.GetComponent<AudioSource>().clip = CharacterStats.CS.fire[0];
			audiosource.GetComponent<AudioSource>().Play();

		}
		else if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Shotgun)
		{
			spriterenderer.sprite = BulletSprite[1];
			audiosource.GetComponent<AudioSource>().clip = CharacterStats.CS.fire[2];
			audiosource.GetComponent<AudioSource>().Play();
		}
		else if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.MachineGun)
		{
			spriterenderer.sprite = BulletSprite[2];
			audiosource.GetComponent<AudioSource>().clip = CharacterStats.CS.fire[1];
			audiosource.GetComponent<AudioSource>().Play();
		}
		else if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Sniper)
		{
			spriterenderer.sprite = BulletSprite[3];
			audiosource.GetComponent<AudioSource>().clip = CharacterStats.CS.fire[3];
			audiosource.GetComponent<AudioSource>().Play();
		}
        else if (CharacterStats.CS.TypeofWeapon == CharacterStats.Weapon.Minigun)
        {
            spriterenderer.sprite = BulletSprite[4];
            audiosource.GetComponent<AudioSource>().clip = CharacterStats.CS.fire[4];
            audiosource.GetComponent<AudioSource>().Play();
        }

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		


		
	}

    void removego(){
        this.Recycle();
    }

	void Update()
	{
        if (peopleHit >= 3)
        {
            this.Recycle();
        }
        transform.Translate(Vector3.up * 30f * Time.deltaTime);
		//RaycastHit2D hitInfo;
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Enemy"));
		if (hitInfo.collider != null)
		{
			criticalHit = Random.Range(1, 10);
			float criticaldmg = CharacterStats.CS.Damage * Random.Range(2, 3);
			float dmg = CharacterStats.CS.Damage;

            if (hitInfo.transform.CompareTag("Enemy"))
			{
				attackPlayer EnemyScript = hitInfo.transform.GetComponent<attackPlayer>();
				EnemyScript.seenPlayer = true;
				if (EnemyScript.health > 0)
				{
					bulletImpact2.transform.Spawn(transform.position, hitInfo.transform.rotation);
				}
                EnemyScript.Target = CharacterStats.CS.gameObject;

				if (criticalHit == 1)
				{
					Stats.Criticals += 1;
					if (EnemyScript.health < 1)
					{
						Time.timeScale = 0.3f;
					}

					EnemyScript.DamagedByPlayer(criticaldmg, true, false);

				}
				else
				{

					EnemyScript.DamagedByPlayer(dmg, false, false);

				}


				if (!SniperBullet)
				{
                    this.Recycle();
				}
				else
				{
					peopleHit += 1;
				}
				if (EnemyScript.health <= 0)
				{

					if (this.transform.position.x < hitInfo.transform.position.x)
					{
						EnemyScript.flipped = true;
					}
				}
			}

           

            if (hitInfo.transform.CompareTag("Shield"))
			{
				this.Recycle();
				bulletImpact.transform.Spawn(transform.position, transform.rotation);
			}

		}

	}



	void OnCollisionEnter2D(Collision2D other)
	{

		if (!other.gameObject.CompareTag("Bullet")
			&& !other.gameObject.CompareTag("Player"))
		{
			timer = 0;
			this.Recycle();
			bulletImpact.transform.Spawn(transform.position, transform.rotation);

		}

	}
}
