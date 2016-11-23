using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UiText : MonoBehaviour {


    Text HealthString;
    Text AmmoString;
    Text ArmorString;

    PlayerStats playerStats;
    WeaponFire weaponStats;

    // Use this for initialization
    void Start () {

        HealthString = GameObject.Find("Health_Text").GetComponent<Text>();
        AmmoString = GameObject.Find("Ammo_Text").GetComponent<Text>(); 
        ArmorString = GameObject.Find("Armor_Text").GetComponent<Text>();

        playerStats = GameObject.Find("Debug_Character").GetComponent<PlayerStats>();
        weaponStats = GameObject.Find("DebugGun").GetComponent<WeaponFire>();

    }
	
	// Update is called once per frame
	void Update () {

        if (HealthString != null) { HealthString.text = "Health: " + playerStats.getHealth(); }

        if (AmmoString != null) { AmmoString.text = "Ammo: " + weaponStats.getBulletCount() + "/" + weaponStats.getFullMagSize(); }

        if (ArmorString != null) { ArmorString.text = "Armor: " + playerStats.getArmor(); }



    }
}
