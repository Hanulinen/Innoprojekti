using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour {
    public int HP;
    public int Damage;
    public float MovementSpeed;
    public float AttackSpeed;
    public float ammoSpeed; //Only used if the enemy shoots projectiles
    public bool boss;

    //The range compared to the player that the enemy wants to get to
    public float targetRange;
}
