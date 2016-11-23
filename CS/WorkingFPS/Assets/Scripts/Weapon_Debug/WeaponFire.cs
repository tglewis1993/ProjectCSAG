using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        playerPosition = Vector3.zero;
        playerViewDir = Vector3.zero;

        positionUpdate();

        canFire = 0;
        inacc = 0.12993f;
        fireRate = 20.0f;


    }
	
	// Update is called once per frame
	void Update ()
    {
        positionUpdate();
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

        bulletPath.origin = playerPosition;

        playerViewDir.x += Random.Range(-1.0f, 1.0f) * inacc;
        playerViewDir.y += Random.Range(-1.0f, 1.0f) * inacc;
        playerViewDir.z += Random.Range(-1.0f, 1.0f) * inacc;

        bulletPath.direction = playerViewDir;

    }

    void fire()
    {

        updateRay(); // update the ray's path by updating the references to players position. 

        if (canFire < 0)
            canFire = 0;
        else
            canFire = canFire - 1;

        m_mouseClick = Input.GetAxisRaw("Fire1"); // Get mouse click. When mouse is clicked m_mouseClick == 1;

        if(m_mouseClick>0) // Don't fire if mouse isn't clicked.
        {
            if (canFire == 0) // Controls time between shots. The larger fire rate is the longer the time between shots.
                canFire = fireRate;
            else return; // Don't fire at all between shots. 

            

            if (Physics.Raycast(bulletPath, out shotHitInfo)) // fire a ray using values obtained in updateRay. 
            {
                Debug.DrawLine(bulletPath.origin, shotHitInfo.point, Color.red, 3);
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
    static float canFire;
    float fireRate;
}
