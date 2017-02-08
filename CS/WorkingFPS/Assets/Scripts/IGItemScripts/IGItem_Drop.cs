using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGItem_Drop : MonoBehaviour {

    private IGItem_Master IM;
    private GameManager_Master.GameManager_Master GM;
    private Transform itemTransform;
    private Rigidbody itemRB;
    private Vector3 dropDir;

    public bool throwable;
    public float dropForce;


    private void Start()
    {
        IM = GetComponent<IGItem_Master>();
        itemTransform = transform;
        itemRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        checkForDropInput(); 
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    void checkForDropInput()
    {
        if(Input.GetButtonDown("Drop") && !GM.bMenuOn && throwable && itemTransform.root.CompareTag(GameManager_References.g_playerTag))
        {
            dropActions();
        }

    }

    void dropActions()
    {
        dropDir = itemTransform.parent.forward;
        itemTransform.parent = null;
        IM.CE_IGItemDrop();
        dropIGItem();
    }

    void dropIGItem()
    {
        itemRB.AddForce(dropDir * dropForce, ForceMode.Impulse);

    }

}
