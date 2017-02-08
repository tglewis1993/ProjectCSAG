using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour {

    // CE = CALL EVENT
    // EH = EVENT HANDLER

    public delegate void PlayerEH();
    public event PlayerEH ev_invchange;
    public event PlayerEH ev_handsempty;

    public delegate void PlayerChangeWeaponEH(int index);
    public event PlayerChangeWeaponEH ev_changeweapon;

    public delegate void PlayerHealthEH(int healthChange);
    public event PlayerHealthEH ev_takedamage;
    public event PlayerHealthEH ev_gainhealth;

    public void CE_Player_Inv_Change()
    {
        if (ev_invchange != null)
        {
            ev_invchange();
        }
    }

    public void CE_Player_Hands_Empty()
    {
        if (ev_handsempty != null)
        {
            ev_handsempty();
        }
    }

    public void CE_Player_Take_Damage(int healthChange)
    {
        if(ev_takedamage != null)
        {
            ev_takedamage(healthChange);
        }
    }

    public void CE_Player_Gain_Health(int healthChange)
    {
        if (ev_gainhealth != null)
        {
            ev_gainhealth(healthChange);
        }
    }

    public void CE_Player_Change_To_Slot(int index)
    {
        if (ev_changeweapon != null)
        {
            ev_changeweapon(index);
        }


    }

}
