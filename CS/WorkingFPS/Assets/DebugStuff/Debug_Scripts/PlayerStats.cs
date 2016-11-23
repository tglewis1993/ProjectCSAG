using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    int health;
    int armor;

	// Use this for initialization
	void Start () {
        health = 100;
        armor = 125;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getHealth()
    {
        return health;
    }

    public int getArmor()
    {
        return armor;
    }
}
