using UnityEngine;
using System.Collections;

public class ThrowThing : MonoBehaviour {

    public GameObject thingPrefab;
    private Transform myTran;

    public float throwForce = 50.0f;
	// Use this for initialization
	void Start () {

        myTran = transform;


	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonUp("Fire2"))
                throwThing();

	}

    void throwThing()
    {
        GameObject thing = (GameObject)Instantiate(thingPrefab, myTran.TransformPoint(-0.2f, 0, 0.5f), myTran.rotation);

        thing.GetComponent<Rigidbody>().AddForce(myTran.forward * throwForce, ForceMode.Impulse);


    }
}
