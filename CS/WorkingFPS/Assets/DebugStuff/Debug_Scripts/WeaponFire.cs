using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour {

    void printDebugInfo()
    {
        //Debug.Log("Bullet Count: " + bulletCount);
        //Debug.Log("Reload Pressed: " + m_ReloadPressed);
        //Debug.Log("Reload Delay: " + canReload);
    }

	// Use this for initialization
	void Start ()
    {
        PlayerView = GetComponentInParent<Camera>();

        playerPosition = Vector3.zero;
        playerViewDir = Vector3.zero;

        updateRay();

        reloadDelay = 0;

        m_ReloadPressed = false;

        canFire = 0;
        canReload = 0;
        inacc = 0.048833f;
        fireDelay = 20.0f;
        reloadDelay = 200.0f;


        magSize = 30;

        bulletCount = magSize;

        

    }
	
	// Update is called once per frame
	void Update ()
    {


        //checkForFiring();

        //printDebugInfo();
        

	}

    void positionUpdate()
    {
        playerPosition = PlayerView.transform.position;
        playerViewDir = PlayerView.transform.forward;
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

    public void checkForFiring()
    {

        updateRay(); // update the ray's path by updating the references to players position. 

        if (canFire < 0)
            canFire = 0;
        else
            canFire = canFire - 1;

        m_mouseClick = Input.GetButton("Fire1"); // Get mouse click. When mouse is clicked m_mouseClick == 1;

        if(m_mouseClick && !m_ReloadPressed) // Don't fire if mouse isn't clicked or reloading.
        {
            if (canFire == 0 && bulletCount > 0) // Controls time between shots. The larger fire delay is the longer the time between shots.
            {
                canFire = fireDelay;
                bulletCount--;
                if (Physics.Raycast(bulletPath, out shotHitInfo)) // fire a ray using values obtained in updateRay. 
                {
                    //HIT DETECTED, use shotHitInfo to get information on the object hit. 

                    Debug.DrawLine(bulletPath.origin, shotHitInfo.point, Color.red, 1);
                    Debug.Log(shotHitInfo.transform.tag);
                }
            }

        }
        checkForReload();

    }

    void checkForReload()
    {
        if (bulletCount != magSize) // Don't reload if at full ammo
        {
            if (canReload < 0) // reload delay ticker
                canReload = 0;
            else
                canReload = canReload - 1;

            if (!m_ReloadPressed && !m_mouseClick)
            {

                m_ReloadPressed = Input.GetButtonDown("Reload");
                if(m_ReloadPressed)
                    canReload = reloadDelay;
            }

            if (m_ReloadPressed)
            { 

                if (canReload == 0)
                {
                    bulletCount = magSize;
                    m_ReloadPressed = false;
                }

            }
        }

    }


    public int getBulletCount()
    {
        return bulletCount;
    }

    public int getFullMagSize()
    {
        return magSize;
    }

    Camera PlayerView;

    Ray bulletPath;
    bool m_mouseClick;
    bool m_ReloadPressed;
    RaycastHit shotHitInfo;
    Vector3 playerPosition;
    Vector3 playerViewDir;

    float inacc;
    static float canFire;
    float fireDelay;

    int bulletCount;
    int magSize;

    static float canReload;
    float reloadDelay; // Time between start of reload and end of reload.

    
}
