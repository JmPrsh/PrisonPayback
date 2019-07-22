using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public enum EnemyType
    {
        Normal,
        Boss,
        MiniBoss,
        Dog,
        Zombie,
        StunWand
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
    public Sprite EnemySprite, BulletSprite;
    public bool AllowWeaponDrop;
    public Transform WeaponToSpawn;
}
