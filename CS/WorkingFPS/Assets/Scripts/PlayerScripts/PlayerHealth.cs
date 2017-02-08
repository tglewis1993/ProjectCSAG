using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private PlayerMaster PM;
    private GameManager_Master.GameManager_Master GM;

    private void OnEnable()
    {
        PM = GetComponent<PlayerMaster>();

        PM.ev_takedamage += TakeDamage;
        PM.ev_gainhealth += RestoreHealth;

    }

    private void OnDisable()
    {
        PM.ev_takedamage -= TakeDamage;
        PM.ev_gainhealth -= RestoreHealth;
    }

    private void TakeDamage(int dmg)
    {
        

    }

    private void RestoreHealth(int hc)
    {


    }
}
