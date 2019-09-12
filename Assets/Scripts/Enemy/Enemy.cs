using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public float CashToGive;
    public float DifficultyWeight;
    public enum EnemyType
    {
        Normal,
        Boss,
        MiniBoss,
        Dog,
        Zombie,
        StunWand,
        Demolition
    }
    public EnemyType enemyType;
    public enum EnemyAttackType
    {
        Melee,
        Gun
    }
    public EnemyAttackType enemyAttackType;
    public int Damage;
    public float Health;
    public float MoveSpeed = 6;
    public float ShootRate;
    public float AttackDistance;
    public float reloadtime;
    public float AimSpeed;
    public int bulletsinclip;
    public EnemyBullet bulletPrefab;
    public Sprite[] EnemySprite;
    public Sprite BulletSprite;
    public bool AllowWeaponDrop;
    public Transform WeaponToSpawn, BulletCasing;
    public AudioClip AttackSound;
    public Transform DroppedItem;
}
