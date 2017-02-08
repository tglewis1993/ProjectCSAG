using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class WeaponFire : MonoBehaviour {

    private Camera PlayerCamera;

    public Beam_Effect beam;

    private IEnumerator spawnCo;

    private Ray bulletPath;

    private bool m_mouseClick;
    private bool m_ReloadPressed;
    private RaycastHit shotHitInfo;
    private Vector3 playerPosition;
    private Vector3 PlayerCameraDir;

    private Vector3 bulletPlace;
    private Transform eyepos;

    private Transform ParentRef;
    private Transform BarrelPos;

    public GameObject BeamProjectile;

    private float inacc;
    private static float canFire;
    private float fireDelay;

    private int bulletCount;
    private int magSize;

    private static float canReload;
    private float reloadDelay; // Time between start of reload and end of reload.


    private void printDebugInfo()
    {
        //Debug.Log("Bullet Count: " + bulletCount);
        //Debug.Log("Reload Pressed: " + m_ReloadPressed);
        //Debug.Log("Reload Delay: " + canReload);
    }

    // Use this for initialization
    void Start ()
    {

        ParentRef = transform.parent;

        PlayerCamera = GameObject.Find("Eyes").GetComponent<Camera>();
        BarrelPos = GameObject.Find("Barrel_End").GetComponent<Transform>();

        playerPosition = Vector3.zero;
        PlayerCameraDir = Vector3.zero;
        bulletPath = new Ray();
        updateRay();
        m_ReloadPressed = false;

        canFire = 0;
        canReload = 0;
        inacc = 0.048833f;
        fireDelay = 0.4f;
        reloadDelay = 6.0f;


        magSize = 30;

        bulletCount = magSize;

    }

    private void positionUpdate()
    {
        playerPosition = PlayerCamera.transform.position;
        PlayerCameraDir = PlayerCamera.transform.forward;
    }

    private void updateRay()
    {
        positionUpdate();
        bulletPath.origin = playerPosition;

        PlayerCameraDir.x += Random.Range(-1.0f, 1.0f) * inacc;
        PlayerCameraDir.y += Random.Range(-1.0f, 1.0f) * inacc;
        PlayerCameraDir.z += Random.Range(-1.0f, 1.0f) * inacc;

        bulletPath.direction = PlayerCameraDir;
        beam.getRay();
    }

    private void ProcessShot()
    {



        Vector3 shotStart = BarrelPos.position;
        Quaternion shotRot = BarrelPos.rotation;

        canFire = fireDelay;
        bulletCount--;

        GameObject bp = Instantiate(BeamProjectile, shotStart, shotRot);
        bp.transform.parent = null;
        spawnCo = despawnProjectile(bp, 3.0f);
        StartCoroutine(spawnCo);
        
        if (Physics.Raycast(bulletPath, out shotHitInfo)) // fire a ray using values obtained in updateRay. 
        {
            //HIT DETECTED, use shotHitInfo to get information on the object hit. 
            //beam.processBeam(true);
            
            if (bp.transform.position == shotHitInfo.point)
            {
                StopCoroutine(spawnCo);
                Destroy(bp);
            }
            Debug.DrawLine(bulletPath.origin, shotHitInfo.point, Color.red, 1);
            //Debug.Log(shotHitInfo.transform.tag);
        }
        else
        {
            //beam.processBeam(false);
        }

    }

    public void checkForFiring()
    {
        updateRay(); // update the ray's path by updating the references to players position. 
        
        if (canFire < 0)
            canFire = 0;
        else
            canFire -= Time.fixedDeltaTime;

        m_mouseClick = Input.GetButton("Fire1"); // Get mouse click. When mouse is clicked m_mouseClick == 1;

        if(m_mouseClick && !m_ReloadPressed) // Don't fire if mouse isn't clicked or reloading.
        {
            if (canFire == 0 && bulletCount > 0) // Controls time between shots. The larger fire delay is the longer the time between shots.
            {
                ProcessShot();
            }
        }
        checkForReload();
    }

    private IEnumerator despawnProjectile(GameObject proj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(proj);

    }

    private void checkForReload()
    {
        if (bulletCount != magSize) // Don't reload if at full ammo
        {
            if (canReload < 0) // reload delay ticker
                canReload = 0;
            else
                canReload -= Time.fixedDeltaTime;

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

    public void GenerateShotPath()
    {


    }

    // ACCESSORS
    public int getBulletCount()
    {
        return bulletCount;
    }

    public int getFullMagSize()
    {
        return magSize;
    }

    public Ray getRayPath()
    {
        return bulletPath;
    }

    public RaycastHit getHitInfo()
    {
        return shotHitInfo;
    }

    public Transform getBarrel()
    {
        return BarrelPos;
    }
    
}
