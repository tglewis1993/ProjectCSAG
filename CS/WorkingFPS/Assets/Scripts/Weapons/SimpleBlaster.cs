using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBlaster : WeaponMaster {


    public SimpleBlaster()
    {
        

    }

    public override void SetWeaponStats()
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
