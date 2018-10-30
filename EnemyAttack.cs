using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //Parent components
    private EnemyAttributes EA;
    private EnemyBehaviour EB;

    //Used for the attack
    private float NextAttack;
    private bool Attacking;

    public GameObject _weapon;

    // Use this for initialization
    void Start () {
        EA = transform.parent.GetComponent<EnemyAttributes>();
        EB = transform.parent.GetComponent<EnemyBehaviour>();
        NextAttack = 0;
        Attacking = false;
    }
	
	// Update is called once per frame
	void Update () {
        CheckRange();
    }

    //Used to check if the enemy is at the desired range from the player
    private void CheckRange() {

        //Check the distance between enemy and the player
        float distance = Vector3.Distance(gameObject.transform.position, EB.PlayerPosition.position);

        //If enemy is at the desired range, call Attack
        if (distance <= EA.targetRange) {
            Attack();
        }
    }

    void Attack() {
        if (Time.time > NextAttack) {
            NextAttack = Time.time + EA.AttackSpeed;
            Attacking = true;

            //Rotating the attack towards player
            var dir = Camera.main.WorldToScreenPoint(EB.PlayerPosition.position) - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            //Show weapon when attack starts. An animation plays here after art is ready
            _weapon.GetComponent<MeshRenderer>().enabled = true; //Remove after art for attack is in game
            //Enable collider so weapon can trigger damage in enemy
            _weapon.GetComponent<BoxCollider>().enabled = true;
            //Hide weapon after attack
            Invoke("HideWeapon", 0.2f);
        }
    }

    void HideWeapon() {
        _weapon.GetComponent<MeshRenderer>().enabled = false;//Remove after art for attack is in game
        _weapon.GetComponent<BoxCollider>().enabled = false;
    }
}
