using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public bool Boss;
    public bool MiniBoss;
    public enum TypeOfEnemy{
        Melee,
        Gun
    }
    public TypeOfEnemy enemyType;
    public float Health;
    public float MoveSpeed = 6;
    public float ShootRate;
    public float AttackDistance;
    public float reloadtime;
    public int bulletsinclip;
    public EnemyBullet bulletPrefab;
}
