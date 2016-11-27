using UnityEngine;
using System.Collections;

public class ThrowThing : MonoBehaviour {

    public GameObject thingPrefab;
    public float thingLifetime = 15.0f;
    private Transform myTran;

    GameManager_TogglePause gm_tp;


    public float throwForce = 50.0f;
	// Use this for initialization
	void Start () {

        myTran = transform;
        gm_tp = GameObject.Find("GameManager").GetComponent<GameManager_TogglePause>();

	}
	
	// Update is called once per frame
	void Update () {
        if (!gm_tp.bPaused)
        {
            if (Input.GetButtonUp("Fire2") && !Input.GetButton("Fire1")) // Will throw when right click is released and when fire isn't held.
                throwThing();
        }
	}

    void throwThing()
    {
        GameObject thing = (GameObject)Instantiate(thingPrefab, myTran.TransformPoint(-0.2f, 0, 0.5f), myTran.rotation);

        thing.GetComponent<Rigidbody>().AddForce(myTran.forward * throwForce, ForceMode.Impulse);

        StartCoroutine(Despawn(thing, 3.0f));

    }

    IEnumerator Despawn(GameObject go, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(go);
    }
}
