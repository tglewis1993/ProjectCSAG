using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public Transform invPlayerParent;

    private PlayerMaster PM;
    private float switchTime = 0.5f;

    private Transform activeItem;
    private int count;

    private List<Transform> invList = new List<Transform>();

    private void OnEnable()
    {
        PM = GetComponent<PlayerMaster>();
        updateInvList();
        checkIfHandsEmpty();

        PM.ev_invchange += updateInvList;
        PM.ev_handsempty += checkIfHandsEmpty;
        PM.ev_handsempty += clearHands;

        PM.ev_changeweapon += switchToIndex;

    }

    private void OnDisable()
    {
        PM.ev_invchange -= updateInvList;
        PM.ev_handsempty -= checkIfHandsEmpty;
        PM.ev_handsempty -= clearHands;
    }

    private void Update()
    {
        checkForInventoryButtons();
    }

    void checkForInventoryButtons()
    {
        if(Input.GetButtonDown("Primary"))
        {

            PM.CE_Player_Change_To_Slot(0);

        }
        if (Input.GetButtonDown("Secondary"))
        {

            PM.CE_Player_Change_To_Slot(1);

        }

    }

    void switchToIndex(int index)
    {
        deactivateAllItems();

        if (invList.Count>index)
        {
            StartCoroutine(switchToItem(invList[index]));

        }

    }

    void updateInvList()
    {
        count = 0;

        invList.Clear();
        invList.TrimExcess();

        foreach(Transform child in invPlayerParent)
        {
            if(child.CompareTag("Item"))
            {
                invList.Add(child);
                count++;


            }

        }
    }
    
    void checkIfHandsEmpty()
    {
        if(activeItem == null && invList.Count>0)
        {
            StartCoroutine(switchToItem(invList[invList.Count-1]));

        }

    }
    
    void clearHands()
    {
        activeItem = null;

    }   
    
    public void activateInvItem(int invIndex)
    {
        deactivateAllItems();

        StartCoroutine(switchToItem(invList[invIndex]));


    }

    public void deactivateAllItems()
    {
        foreach(Transform child in invPlayerParent)
        {
            if(child.CompareTag("Item"))
            {
                child.gameObject.SetActive(false);

            }

        }

    }

    IEnumerator switchToItem(Transform item)
    {
        yield return new WaitForSeconds(switchTime);

        activeItem = item;
        activeItem.gameObject.SetActive(true);

    }
}
