using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        playerPosition = Vector3.zero;
        playerViewDir = Vector3.zero;


        positionUpdate();
        updateRay();
        fireRate = 0;
        inacc = 0.0f; 
    }
	
	// Update is called once per frame
	void Update ()
    {
        fire();
        
	}

    void positionUpdate()
    {
        playerPosition = transform.position;
        playerViewDir = transform.forward;
    }

    void updateRay()
    {
        positionUpdate();

        playerViewDir.x += Random.value * inacc;
        playerViewDir.y += Random.value * inacc;
        playerViewDir.z += Random.value * inacc;


        bulletPath.origin = playerPosition;
        bulletPath.direction = playerViewDir;
    }

    void fire()
    {

        if (fireRate < 0)
            fireRate = 0;
        else
            fireRate = fireRate - 1;

        m_mouseClick = Input.GetAxisRaw("Fire1");

        if(m_mouseClick>0)
        {
            if (fireRate == 0)
                fireRate = 15.0f;
            else return;

            updateRay();

            Debug.DrawLine(bulletPath.origin, shotHitInfo.point, Color.red, 3);

            //Debug.DrawRay(bulletPath.origin, bulletPath.direction, Color.blue, 3.0f);
            if (Physics.Raycast(bulletPath, out shotHitInfo))
            {
                Debug.Log(shotHitInfo.transform.name);


            }
        }


    }

    Ray bulletPath;
    float m_mouseClick;
    RaycastHit shotHitInfo;
    Vector3 playerPosition;
    Vector3 playerViewDir;

    float inacc;
    static float fireRate;
}
