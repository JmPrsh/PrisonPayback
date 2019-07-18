using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public float Health;
    public float MoveSpeed;
    public float ShootRate;
    public float AttackDistance;
    public Sprite Weapon;
}
