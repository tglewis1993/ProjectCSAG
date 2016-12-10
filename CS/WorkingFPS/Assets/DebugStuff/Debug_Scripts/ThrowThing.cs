using UnityEngine;
using System.Collections;



public class ThrowThing : MonoBehaviour {

    public GameObject thingPrefab;
    public float thingLifetime = 3.0f;

    public CharacterController player_cc;

    private Transform myTran;
    [Range(0f,0.1f)]public float movementInfluenceFactor = 0.025f;
    public float throwForce = 5.0f;
	// Use this for initialization
	void Start () {

        myTran = transform;
        player_cc = GameObject.Find("Debug_Character").GetComponent<CharacterController>(); // Grab CC for player velocity.

    }
	
	// Update is called once per frame
	void Update () {

            
	}

    public void throwThing()
    {
        if (Input.GetButtonUp("Fire2") && !Input.GetButton("Fire1")) // Will throw when right click is released and when fire isn't held.

        {
            GameObject thing = Instantiate(thingPrefab, myTran.TransformPoint(-0.0f, 0, 0.4f), myTran.rotation);

            Debug.Log(player_cc.velocity);

            thing.GetComponent<Rigidbody>().AddForce((myTran.forward + (player_cc.velocity * movementInfluenceFactor)) * throwForce, ForceMode.VelocityChange); //(myTran.forward + (player_cc.velocity*0.05f)

            StartCoroutine(Despawn(thing, thingLifetime));
        }

    }

    IEnumerator Despawn(GameObject go, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(go);
    }
}
