using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager_TogglePlayer : MonoBehaviour {

    

    void OnEnable()
    {
        SetInitRefs();
        Gmaster.ev_menuToggle += TogglePlayerController;
        Gmaster.ev_invUIToggle += TogglePlayerController;
    }

    void OnDisable()
    {
        Gmaster.ev_menuToggle -= TogglePlayerController;
        Gmaster.ev_invUIToggle -= TogglePlayerController;
    }

    void SetInitRefs()
    {

        Gmaster = GetComponent<GameManager_Master.GameManager_Master>();

    }

    void TogglePlayerController()
    {
        fpsC.enabled = !fpsC.enabled;

    }

    GameManager_Master.GameManager_Master Gmaster;

    public FirstPersonController fpsC;

}
