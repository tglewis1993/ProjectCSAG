using UnityEngine;
using System.Collections;

public class GameManager_ToggleCursor : MonoBehaviour {

    void OnEnable()
    {
        SetInitRefs();
        Gmaster.ev_menuToggle += ToggleCursor;
        Gmaster.ev_invUIToggle += ToggleCursor;
    }

    void OnDisable()
    {
        Gmaster.ev_menuToggle -= ToggleCursor;
        Gmaster.ev_invUIToggle -= ToggleCursor;
    }

    void SetInitRefs()
    {

        Gmaster = GetComponent<GameManager_Master.GameManager_Master>();

    }

    void Update()
    {
        CheckCursorLock();


    }

    void ToggleCursor()
    {
        cursorLocked = !cursorLocked;

    }

    void CheckCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    GameManager_Master.GameManager_Master Gmaster;
    bool cursorLocked = true;
}
