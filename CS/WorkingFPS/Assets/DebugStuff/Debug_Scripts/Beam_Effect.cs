using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class Beam_Effect : MonoBehaviour {

    public LineRenderer lr;
    public GameObject Weapon;
    Ray bp;
    RaycastHit rh;
    WeaponFire wf;
    Vector3 end;

    bool fire;

    private void Start()
    {

        lr.enabled = false;
        wf = Weapon.GetComponent<WeaponFire>();
    }

    public void getRay()
    {
        bp = wf.getRayPath();
        end = bp.origin + (bp.direction * 50.0f);

    }

    public void processBeam(bool hit)
    {
        
        lr.enabled = true;
        lr.SetPosition(0, bp.origin);

        if(hit)
            lr.SetPosition(1, wf.getHitInfo().point);
        else
            lr.SetPosition(1, end);

        Invoke("endFire", 0.5f);
    }

    void endFire()
    {
        lr.enabled = false;
        fire = true;
    }
}
