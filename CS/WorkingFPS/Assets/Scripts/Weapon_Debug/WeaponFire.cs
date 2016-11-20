using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        positionUpdate();
        updateRay();
        fire();

	}

    void positionUpdate()
    {
        playerPosition = transform.position;
        playerViewDir = transform.forward;
    }

    void updateRay()
    {
        bulletPath.origin = playerPosition;
        bulletPath.direction = playerViewDir;

    }

    void fire()
    {

        m_mouseClick = Input.GetAxisRaw("Fire1");

        if(m_mouseClick>0)
        {
            Debug.DrawRay(bulletPath.origin, bulletPath.direction, Color.blue, 3.0f);
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
}
