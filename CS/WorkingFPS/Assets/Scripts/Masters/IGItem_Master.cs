using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGItem_Master : MonoBehaviour {

    private PlayerMaster PM;

    public delegate void IGItemEH();
    public event IGItemEH ev_drop;
    public event IGItemEH ev_pickup;

    public delegate void PickupActionEH(Transform item);
    public event PickupActionEH ev_pickupaction;

    private void OnEnable()
    {
        if (GameManager_References.PlayerRef != null)
            PM = GameManager_References.PlayerRef.GetComponent<PlayerMaster>();
    }

    public void CE_IGItemDrop()
    {
        if (ev_drop != null)
        {
            ev_drop();
        }
    }

    public void CE_IGItemPickup()
    {
        if (ev_pickup != null)
        {
            ev_pickup();

        }
    }

    public void CE_IGItemPickupAction(Transform item)
    {
        if (ev_pickupaction != null)
        {
            ev_pickupaction(item);

        }
    }
}
