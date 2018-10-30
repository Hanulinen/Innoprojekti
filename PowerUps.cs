using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    //Are the power ups active?
    public bool invincibleOn;
    public bool speedUpOn;
    public bool noCooldownOn;
    //powerup's duration
    [SerializeField]
    float _powerupDuration;
    //Timer for invincibility. All powerups last for _powerupDuration seconds
    float _invincibleTimer;
    //Timer for speed up. All powerups last for _powerupDuration seconds
    float _speedUpTimer;
    //Timer for no cooldown. All powerups last for _powerupDuration seconds
    float _noCooldownTimer;

    // Use this for initialization
    void Start () {
        invincibleOn = false;
        speedUpOn = false;
        noCooldownOn = false;

        _invincibleTimer = _powerupDuration;
        _speedUpTimer = _powerupDuration;
        _noCooldownTimer = _powerupDuration;
    }
	
	// Update is called once per frame
	void Update () {
        if (_invincibleTimer < _powerupDuration)
        {
            _invincibleTimer += Time.deltaTime;
        }
        else if (invincibleOn)
        {
            Invincible();
        }

        if (_speedUpTimer < _powerupDuration)
        {
            _speedUpTimer += Time.deltaTime;
        }
        else if (speedUpOn)
        {
            SpeedUp();
        }

        if (_noCooldownTimer < _powerupDuration)
        {
            _noCooldownTimer += Time.deltaTime;
        }
        else if (noCooldownOn)
        {
            NoCooldown();
        }

    }

    void Invincible()
    {
        invincibleOn = !invincibleOn;
    }

    void SpeedUp()
    {
        speedUpOn = !speedUpOn;
    }

    void NoCooldown()
    {
        noCooldownOn = !noCooldownOn;
    }

    void OnTriggerEnter(Collider other)
    {
        //Check if the item has a correct tag
        if(other.gameObject.tag == "Invincible")
        {
            //if powerup is on, just extend the time it's active
            if (!invincibleOn)
            {
                Invincible();
            }
            //Reset timer
            _invincibleTimer = 0;
            //Destoy the item that's picked up 
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Speed Up")
        {
            if (!speedUpOn)
            {
                SpeedUp();
            }
            _speedUpTimer = 0;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "No Cooldown")
        {
            if (!noCooldownOn)
            {
                NoCooldown();
            }
            _noCooldownTimer = 0;
            Destroy(other.gameObject);
        }
    }
}
