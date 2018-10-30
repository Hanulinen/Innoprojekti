using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private Image _cooldownBar;
    private float _cooldownTimer;
    private bool _cooldownOn;
    [SerializeField]
    private float _cooldownTime;

    // Use this for initialization
    void Start () {
        _cooldownTimer = _cooldownTime;
        _cooldownOn = false;
    }
	
	// Update is called once per frame
	void Update () {

        //Attacking
        if (Input.GetMouseButtonUp(0))
        {
            Attack();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SpecialAttack();
        }

        //Cooldown bar is shrinking while the attack is ncooling down and and is set back to full after the bar is hidden
        if (_cooldownOn)
        {
            _cooldownBar.transform.localScale -= new Vector3(Time.deltaTime / _cooldownTime, 0, 0);
            if (_cooldownTimer >= _cooldownTime)
            {
                _cooldownOn = false;
                _cooldownBar.gameObject.SetActive(false);
                _cooldownBar.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        //Cooldown is in real time
        _cooldownTimer += Time.deltaTime;
    }

    void Attack()
    {
        _cooldownTimer = _cooldownTime;
        _cooldownOn = false;

        //Rotating the sword to point to mouse
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        GameObject _weapon = GameObject.FindGameObjectWithTag("Weapon").gameObject;
        //Show weapon when attack starts. An animation plays here after art is ready
        _weapon.GetComponent<MeshRenderer>().enabled = true;//Remove after art for attack is in game
        //Enable collider so weapon can trigger damage in enemy
        _weapon.GetComponent<BoxCollider>().enabled = true;
        //Hide weapon after attack
        Invoke("HideWeapon", 0.1f);
    }

    //Hides weapon's mesh renderer and box collider
    void HideWeapon()
    {
        GameObject _weapon = GameObject.FindGameObjectWithTag("Weapon").gameObject;
        _weapon.GetComponent<MeshRenderer>().enabled = false;//Remove after art for attack is in game
        _weapon.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    /// Do a spin attack
    /// </summary>
    void SpecialAttack()
    {
        Debug.Log(FindObjectOfType<PowerUps>().noCooldownOn);
        //No cooldown with powerup
        if (FindObjectOfType<PowerUps>().noCooldownOn == true)
        {
            GameObject _specialWeapon = GameObject.FindGameObjectWithTag("Special Weapon").gameObject;
            //Show weapon when attack starts. An animation plays here after art is ready
            _specialWeapon.GetComponent<MeshRenderer>().enabled = true;//Remove after art for attack is in game
            //Enable collider so weapon can trigger damage in enemy
            _specialWeapon.GetComponent<CapsuleCollider>().enabled = true;
            //Hide weapon after attack
            Invoke("HideSpecialWeapon", 0.3f);
        }
        //Cannot spin attack if cooldown is happening
        else if (_cooldownTimer >= _cooldownTime)
        {
            _cooldownTimer = 0;
            _cooldownOn = true;
            _cooldownBar.gameObject.SetActive(true);

            GameObject _specialWeapon = GameObject.FindGameObjectWithTag("Special Weapon").gameObject;
            //Show weapon when attack starts. An animation plays here after art is ready
            _specialWeapon.GetComponent<MeshRenderer>().enabled = true;//Remove after art for attack is in game
            //Enable collider so weapon can trigger damage in enemy
            _specialWeapon.GetComponent<CapsuleCollider>().enabled = true;
            //Hide weapon after attack
            Invoke("HideSpecialWeapon", 0.3f);
        }
    }

    //Hides weapon's mesh renderer and collider
    void HideSpecialWeapon()
    {
        GameObject _specialWeapon = GameObject.FindGameObjectWithTag("Special Weapon").gameObject;
        _specialWeapon.GetComponent<MeshRenderer>().enabled = false;//Remove after art for attack is in game
        _specialWeapon.GetComponent<CapsuleCollider>().enabled = false;
    }
}
