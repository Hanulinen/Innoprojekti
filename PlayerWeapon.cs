using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //If weapon senses an enemy, enemy is called to take damage
        if (other.gameObject.tag == "Enemy")
        {
            //If the scrip name changes, this breaks
            other.GetComponent<EnemyBehaviour>().TakeDamage();
        }
    }
}
