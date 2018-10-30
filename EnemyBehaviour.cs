using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {

    //AI
    public NavMeshAgent agent;
    public GameObject target;
    //Gamecontroller
    private GameObject GC;

    //Attributes script
    private EnemyAttributes EA;

    //Variables used to find player and his position
    private GameObject Player;

    [HideInInspector]
    public Transform PlayerPosition;

    //Potion that the enemy drops when it dies
    public GameObject Potion;

    //to change drunkenness
    private CustomImageEffect drunkHandler;
    private float _drunk;

    // Use this for initialization
    void Start () {

        //Initialise variables
        EA = GetComponent<EnemyAttributes>();
        Player = GameObject.FindGameObjectWithTag("Player");
        GC = GameObject.FindGameObjectWithTag("GameController");
        target = Player;
        
    }
	
	// Update is called once per frame
	void Update () {

        //Find Player position
        PlayerPosition = Player.transform;
        agent.SetDestination(Player.transform.position);
        

        //Move();
    }

    //Method used to take damage
    public void TakeDamage() {

        //Take damage
        EA.HP -= 5;
        //Possible critical hit
        if (Random.Range(1, 100) > 97) EA.HP -= 2;

        //push the enemy away from the player
        var magnitude = 500;
        var force = transform.position - PlayerPosition.position;
        force.Normalize();
        GetComponent<Rigidbody>().AddForce(force * magnitude);

        //If HP goes under 0, destroy gameobject
        if (EA.HP <= 0) {
            Instantiate(Potion, gameObject.transform.position, gameObject.transform.rotation);
			Score.score += 1;
            GC.GetComponent<EnemySpawner>().enemiesLeft -= 1;
            
            //If the enemy was the boss, change the boolean about killing the boss to true
            if (EA.boss = true) {
                GC.GetComponent<EnemySpawner>()._bossKilled = true;
            }
            
            Destroy(gameObject);
            
        }
    }

    //Used to move the enemy
    private void Move() {

        //Check the range to player to determine if we want to move towards or away from the player
        float distance = Vector3.Distance(gameObject.transform.position, PlayerPosition.position);

        //If distance to player is too big, move closer
        //Else move away from the player
        if (distance > EA.targetRange) {
            //Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, Time.deltaTime * EA.MovementSpeed);
        } else {
            //Move away from the player
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, -(Time.deltaTime * EA.MovementSpeed));
        }
        
    }

    //Method called when colliding with objects. Use this to deal damage to player
    private void OnCollisionStay(Collision collision)
    {
        //If collides with Player, deal players method "TakeDamage" to deal damage to player
        if (collision.gameObject.tag == "Player") {
            //collision.gameObject.GetComponent<Player>().TakeDamage((float)EA.Damage);
        }
    }

    
}
