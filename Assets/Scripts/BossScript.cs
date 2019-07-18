using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BossScript : MonoBehaviour
{
	public float MoveSpeed;
	public enum EnemyClass
	{
		Warden
		
	}
	;
	public EnemyClass enemyclass;
	public Animator anim;
	public bool seenPlayer;
	public int health = 100;
	public EnemyBullet bulletPrefab;
	public float shotInterval = 1.0f;
	float shootTime = 0.0f;
	public Transform[] ShootFrom;
	public GameObject Warning;
	CharacterStats CS;
	public enum TypeOfEnemy
	{
		Melee,
		Pistol,
		Machinegun,
		Shotgun,
		Sniper,
		Minigun,
		DogBite,
		Demolition

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
	public int Damage;
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

	void Awake ()
	{

		foreach (Transform EBC in EnemyBulletCasing) {
			EBC.CreatePool ();
		}

		chooseEnemyAtStartUp = Random.Range (0, 2);

		switch (chooseEnemyAtStartUp) {
		case 0:
			typeofenemy = TypeOfEnemy.Melee;
			break;
		case 1:
			typeofenemy = TypeOfEnemy.Pistol;
			break;
				
		}
			
	}

	void OnEnable ()
	{

		Dead = false;
		transform.SetParent (GameObject.Find ("EnemiesParent").transform);
		audiosource = GetComponent<AudioSource> ();
		CS = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterStats> ();
		

		transform.position = new Vector3 (transform.position.x + Random.Range (3, -3), transform.position.y + Random.Range (3, -3), transform.position.z);
		shootTime = 0;
		
		Quaternion randomRot = Quaternion.Euler (0, 0, Random.Range (0, 360));
		childRandom.transform.rotation = randomRot;
	
		if (enemyclass == EnemyClass.Warden) {
			this.name = "Warden";
			health = 5000;
			ChildSprite.GetComponent<SpriteRenderer> ().sprite = EnemySprite [6];
			typeofenemy = TypeOfEnemy.Minigun;
			Damage = 10;
			shotInterval = 0.05f;
			
			// adds weapon of choice to list to be active and turns off other weapons
			foreach (GameObject weapons in WeaponOfChoice) {
				WeaponInt.Add (WeaponOfChoice [5]);
				weapons.SetActive (false);
			}
			bulletsinclip = 200;
			reloadtime = 10;
			WSanim = WeaponShoot [4];
			WMFanim = WeaponMuzzleFlash [4];
			EnemyDistance = 6f;
		}
	

	}
		
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	bool RemovedSelf;

	void Update ()
	{
		if (!CS.Dead) {
			if (!PauseMenu.isPaused && CharacterStats.allowMovement) {
		
				if (Target != null) {
					DyingGO.transform.rotation = Quaternion.AngleAxis (CharacterAngle, Vector3.forward);
					AimingGO.GetComponent<LookTowardsPlayer> ().Target = Target;
			
					for (var i = 0; i < WeaponInt.Count; i++) {
						WeaponInt [i].SetActive (true);
					}

					if (bulletsused >= bulletsinclip) {
						bulletsused = 0;
						StartCoroutine (Reload ());
					} 


					CheckifAlive ();
					if (!Dead) {
						CheckDistance ();
						Shoot ();
						Reset ();
						if (showgrey) {
							if (greyAmount < 1) {
								greyAmount += 1 * Time.deltaTime * 2;
							}
						}
					} else {
						ChildSprite.GetComponent<SpriteRenderer> ().material.SetFloat ("_EffectAmount", greyAmount);
						if (showgrey) {
							if (greyAmount < 1) {
								greyAmount += 1 * Time.deltaTime * 2;
							}
						}
						anim.SetBool ("Walk", false);
					}

				} 
		

			} 
		} else {
			anim.SetBool ("Walk", false);
			
		}
	}

	void Reset ()
	{
		CharacterAngle = 0;
		startDeath = false;
		AimingGO.SetActive (true);
		BloodSplat.SetActive (false);
		BloodPool.SetActive (false);
		GetComponent<Collider2D> ().enabled = true;
		ChildSprite.GetComponent<Collider2D> ().enabled = true;
		Shadow.SetActive (true);
		showgrey = false;
		ChildSprite.GetComponent<SpriteRenderer> ().material.SetFloat ("_EffectAmount", 0);
	}

	void CheckDistance ()
	{
		if (Vector3.Distance (Target.transform.position, transform.position) < 5) {
			anim.SetBool ("Walk", true);							
		}
	
		if (Vector3.Distance (Target.transform.position, transform.position) < EnemyDistance) {
			shootTime += 1 * Time.deltaTime;
			if (shootTime >= (shotInterval / StaticVariables.EnemySpeed) && !reload) {
			
				if (enemyclass == EnemyClass.Warden) {
					EnemyBullet temp = bulletPrefab.Spawn (ShootFrom [4].transform.position, ShootFrom [4].transform.rotation);
						
					temp.Damage = Damage;
						
					temp.spriterenderer.sprite = EnemyBullets [4];
						
					EnemyBulletCasing [3].Spawn (new Vector3 (transform.position.x + Random.Range (-0.2f, 0.2f), transform.position.y + Random.Range (-0.2f, 0.2f), transform.position.z), transform.rotation);
					bulletsused += 1;
					WSanim.SetTrigger ("Shoot");
					WMFanim.SetTrigger ("Shoot");
					audiosource.clip = fire [4];
					audiosource.Play ();
						
				}

				shootTime = 0;
					
					
			} else {
				CanSwing = false;
				shootTime += 1 * Time.deltaTime;
				// play melee animation
					
			}

			if (typeofenemy == TypeOfEnemy.Melee) {
				float RandomSwingTime = Random.Range (0.9f, 1.1f);
				if (shootTime >= (RandomSwingTime / StaticVariables.EnemySpeed) && Target.activeInHierarchy) {
					CanSwing = true;
					shootTime = 0;
					
				} else {
					CanSwing = false;
					shootTime += 1 * Time.deltaTime;
					// play melee animation
					
				}
			}

			anim.SetBool ("Walk", false);
		} else {
			float step = MoveSpeed * Time.deltaTime * StaticVariables.EnemySpeed;
			transform.position = Vector3.MoveTowards (transform.position, Target.transform.position, step);
			anim.SetBool ("Walk", true);
					
		}

	}
	
	void Shoot ()
	{
		if (!Dead) {
			if (shootBurst) {
				if (shotsfired < 3) {
					if (bursttimer > 0) {
						bursttimer -= 2f * Time.deltaTime;
					}
					if (bursttimer <= 0) {
						EnemyBullet temp = bulletPrefab.Spawn (ShootFrom [2].transform.position, ShootFrom [2].transform.rotation);
						temp.Damage = Damage;
						temp.spriterenderer.sprite = EnemyBullets [2];
						audiosource.clip = fire [2];
						audiosource.Play ();
						EnemyBulletCasing [2].Spawn (new Vector3 (transform.position.x + Random.Range (-0.2f, 0.2f), transform.position.y + Random.Range (-0.2f, 0.2f), transform.position.z), transform.rotation);
						shotsfired += 1;
						bulletsused += 1;
						bursttimer = 0.2f;
						WSanim.SetTrigger ("Shoot");
						WMFanim.SetTrigger ("Shoot");
					}
				} else {
					shootBurst = false;
				}
			}
		}

	}

	bool startDeath;

	void CheckifAlive ()
	{
		if (health < 0) {
			if (!startDeath) {
				GetComponent<Rigidbody2D> ().isKinematic = true;
				StartCoroutine (Death ());
				startDeath = true;
			}
		}

		if (Dead) {

			if (!flipped) {
				if (CharacterAngle < 90) {
					CharacterAngle += 90 * Time.deltaTime * 2;
				} else {
					BloodPool.SetActive (true);
				}
			} else {
				if (CharacterAngle > -90) {
					CharacterAngle -= 90 * Time.deltaTime * 2;
				} else {
					BloodPool.SetActive (true);
				}
			}

			
		}

	}

	void DieForSure ()
	{

		this.Recycle ();

	}

	IEnumerator Death ()
	{
		foreach (GameObject weapons in WeaponOfChoice) {
			weapons.SetActive (false);
		}

		AimingGO.SetActive (false);

		if (Tutorial.AllowCombo) {
			CS.ComboTimer = 40;
			
			CharacterStats.Combo += 1;
			int ScoreGiven = 10 * CharacterStats.Combo;
			Transform Scoretemp = CS.ScoreCollectedText.Spawn (new Vector3 (transform.position.x + Random.Range (-0.2f, 0.2f), transform.position.y + 0.4f, transform.position.z), transform.rotation) as Transform;
			Scoretemp.GetComponentInChildren<Text> ().text = ScoreGiven.ToString ();
			Scoretemp.GetComponentInChildren<Text> ().color = Color.white;
			CharacterStats.Score += ScoreGiven;
			
		}

		GetComponent<Collider2D> ().enabled = false;
		ChildSprite.GetComponent<Collider2D> ().enabled = false;
		Dead = true;
		BloodSplat.SetActive (true);
		Shadow.SetActive (false);
		showgrey = true;
		Transform temp = BloodStain.Spawn (transform.position, transform.rotation) as Transform;
		temp.transform.rotation = Quaternion.AngleAxis (Random.Range (0, 360), Vector3.forward);
		temp.localScale = temp.localScale * Random.Range (0.5f, 1);
		Stats.Kills += 1;
		yield return new WaitForSeconds (5);
		this.Recycle ();

	}

	IEnumerator Reload ()
	{
		reload = true;
		// play animation here
//		Debug.Log (this.name + " is reloading");
		yield return new WaitForSeconds (reloadtime);
//		Debug.Log (this.name + " has finished reloading");
		//stop animation here
		reload = false;

	}

	public void Damaged (int dmg)
	{
		ChildSprite.GetComponent<Animator> ().SetTrigger ("GetHurt");
		health -= dmg;
	}


}
