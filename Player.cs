using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float _health;
    public float _healthMax;
    private float _drunk;
    [SerializeField]
    private Image healthBar;
    PowerUps _powerups;
    private CustomImageEffect drunkHandler;
    private float _timer;
    private float _soberPeriod;

    // Use this for initialization
    void Start () {
        _timer = 0;
        _soberPeriod = 5f;

        _health = 20;
        _healthMax = 20;
        _powerups = FindObjectOfType<PowerUps>();
        //haetaan camerasta scripti ja sorkitaan materiaalin propertya. Materiaalin property ei resettaa muuten nollaan pelin alkaessa velii
        _drunk = 0;
        drunkHandler = GameObject.Find("Main Camera").GetComponent<CustomImageEffect>();
        drunkHandler.EffectMaterial.SetFloat("_Magnitude", 0);
    }
	
	// Update is called once per frame
	void Update () {
        //Movement
        float x;
        float z;
        //Powerup can increase speed
        if (_powerups.speedUpOn) {
            x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f*2;
            z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f*2;
        }
        else
        {
            x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
            z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        }

        transform.Translate(x, z, 0);
        
        if(Time.time > _timer)
        {
            _timer += _soberPeriod;
            if (_drunk > 0)
            {
                RemoveDrunk();
            }
        }
    }

    /// <summary>
    /// Called when player takes damage
    /// </summary>
    /// <param name="damage">damage taken by the player</param>
    public void TakeDamage(float damage)
    {
        //Possible critical hit
        if (Random.Range(1, 100) > 97) damage = damage * 1.5f;

        //Invincibility deflects all damage
        if (_powerups.invincibleOn) return;

        //if health is empty = game over
        if (_health <= 0)
        {
            //Game over
            FindObjectOfType<GameOver>().GameIsOver();

            //health bar shouldn't go negative, so it's put to 0 upon game over
            healthBar.transform.localScale = new Vector3(0, 1, 1);
        }
        else
        {
            //Damage id reduced from health
            _health -= damage;

            //We use this method to heal the player by taking negative damage
            //Because of this we need to make sure players health doesn't go over the maximum
            if (_health > _healthMax) {
                _health = _healthMax;
                healthBar.transform.localScale = new Vector3 (2, 0.2f, 1);
            } else {
                //Calculations for reducing the visual health bar
                //This is stupidly complex because it needs to work even if someone changes the size of player's health
                healthBar.transform.localScale -= new Vector3((_healthMax / (5 * _healthMax * (_healthMax / 10))) * damage, 0, 0);

                //Calculations for changing the color of the health bar as the health diminishes
                float green = 255 * _health / _healthMax;
                float red = 255 * (1 - (_health / _healthMax));

                //Changing the color of the health bar.
                healthBar.color = new Color32((byte)red, (byte)green, 0, 255);
            }
        }
    }

    //Method called when colliding with objects. Use this to heal player with potions
    private void OnCollisionEnter(Collision collision) {
        //If collides with potion, call method TakeDamage. We call the TakeDamage script with negative "damage" resulting in healing
        if (collision.gameObject.tag == "Potion") {
            TakeDamage(-5);
            AddDrunk();
            Destroy(collision.gameObject);
        }
    }

    void AddDrunk()
    {
        _drunk = _drunk + 0.002f;
        drunkHandler.EffectMaterial.SetFloat("_Magnitude", _drunk);
    }

    void RemoveDrunk()
    {
        _drunk = _drunk - 0.002f;
        drunkHandler.EffectMaterial.SetFloat("_Magnitude", _drunk);
    }
}
