using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mesh))]
public class WeaponMaster : MonoBehaviour {

    private PlayerMaster PM;

    public Mesh m_WeaponModel;
    private Vector3 m_ModelPosition;

    protected float m_FireRate;
    protected float m_CanFire;

    protected float m_MaxDamage;
    protected float m_MinimumDamage;

    protected float m_ReloadTime;
    protected float m_CanReload;

    protected int m_MagSize;
    protected int m_RoundCount;

    protected int m_Seed;

    protected double m_Inaccuracy;

    protected bool m_IsFiring;
    protected bool m_IsReloading;

    // Use this for initialization
    private void OnEnable()
    {

        PM = GetComponent<PlayerMaster>();

        SetWeaponStats();
        m_IsFiring = false;
        m_IsReloading = false;

        Random.InitState(m_Seed);

    }

    private void OnDisable()
    {

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
