using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WeaponFire))]
[RequireComponent(typeof(ParticleSystem))]
public class Proj_Logic : MonoBehaviour {

    public WeaponFire Weapon;
    public float speed = 5.0f;
    private Ray beam;
    private float startTime;
    private float shotLength;
    private float distCovered;
    private float frac;

    private Vector3 BarrelPos_Store;
    private Vector3 BeamDir_Store;

    [SerializeField]private Transform pos;
    [SerializeField]private Transform barrel;
    public ParticleSystem ps;
    private Vector3 barrelFor;
    private void Start()
    {
        //pos.parent = null;

        Weapon = GameObject.Find("1stgun").GetComponent<WeaponFire>();
        ps = GetComponentInChildren<ParticleSystem>();
        pos = transform;
        frac = 0;
        distCovered = 0;

        barrel = Weapon.getBarrel();
        beam = Weapon.getRayPath();

        BarrelPos_Store = barrel.position;
        BeamDir_Store = beam.direction;


        startTime = Time.time;
        shotLength = Vector3.Distance(BarrelPos_Store, BarrelPos_Store + (BeamDir_Store * 50.0f));

        

    }

    private void FixedUpdate()
    {

        
        distCovered = (Time.time - startTime) * speed;
        frac = distCovered / shotLength;

        pos.position = Vector3.Lerp(BarrelPos_Store, BarrelPos_Store + (BeamDir_Store * 50.0f), frac);

    }

}
