using UnityEngine;
using System.Collections;


 
public class GameManager_TogglePause : MonoBehaviour {

	void OnEnable()
    {
        SetInitRefs();
        Gmaster.ev_menuToggle += TogglePause;
        Gmaster.ev_invUIToggle += TogglePause;

    }

    void OnDisable()
    {
        Gmaster.ev_menuToggle -= TogglePause;
        Gmaster.ev_invUIToggle -= TogglePause;
    }

    void SetInitRefs()
    {

        Gmaster = GetComponent<GameManager_Master.GameManager_Master>();
    }

    void TogglePause()
    {
        if(bPaused)
        {
            Time.timeScale = 1;
            bPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            bPaused = true;
        }

    }

    GameManager_Master.GameManager_Master Gmaster;

    bool bPaused = false;
}
