using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mesh))]
public class WeaponMaster : MonoBehaviour {

    public Mesh m_WeaponModel;
    private Vector3 m_ModelPosition;

    private float m_FireRate;
    private float m_CanFire;

    private float m_MaxDamage;
    private float m_MinimumDamage;

    private float m_ReloadTime;
    private float m_CanReload;

    private int m_MagSize;
    private int m_RoundCount;

    private int m_Seed;

    private double m_Inaccuracy;

    private bool m_IsFiring;
    private bool m_IsReloading;

    // Use this for initialization
    void Start ()
    {

        SetWeaponStats();
        m_IsFiring = false;
        m_IsReloading = false;

        Random.InitState(m_Seed);

    }
	
	// Update is called once per frame
	void Update ()
    {

        CheckInput();
		
	}

    private void CheckInput()
    {
        if(Input.GetButton("Fire1") && !m_IsReloading)
        {
            m_IsFiring = true;
        }

        if (Input.GetButtonDown("Reload") && !m_IsFiring)
        {
            m_IsReloading = true;
        }

    }

    private void Fire()
    {


    }

    private void Reload()
    {


    }

    private void SetWeaponPosition(Vector3 newposition)
    {

        m_ModelPosition = newposition;

    }

    private void SetWeaponModel(Mesh newmodel)
    {

        m_WeaponModel = newmodel;

    }

    virtual public void SetWeaponStats()
    {
        m_FireRate = 30.0f;
        m_CanFire = 0.0f;
        m_ReloadTime = 30.0f;
        m_CanReload = 0.0f;
        m_MaxDamage = 100.0f;
        m_MinimumDamage = 25.0f;
        m_Inaccuracy = 0.238520589;

        m_Seed = 1247128472;
    }
}
