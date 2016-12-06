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
    private Transform pos;
    private Transform barrel;
    public ParticleSystem ps;
    private Vector3 barrelFor;
    private void Awake()
    {
        Weapon = GameObject.Find("1stgun").GetComponent<WeaponFire>();
        ps = GetComponentInChildren<ParticleSystem>();
        pos = transform;
        frac = 0;
        distCovered = 0;

        barrel = Weapon.getBarrel();

        beam = Weapon.getRayPath();
        startTime = Time.time;
        shotLength = Vector3.Distance(barrel.position, barrel.position + (beam.direction * 50.0f));

        

    }

    private void Update()
    {

        
        distCovered = (Time.time - startTime) * speed;
        frac = distCovered / shotLength;

        pos.position = Vector3.Lerp(barrel.position, barrel.position + (beam.direction * 50.0f), frac);

    }

}
