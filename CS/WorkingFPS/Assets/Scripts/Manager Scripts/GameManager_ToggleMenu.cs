using UnityEngine;
using System.Collections;

public class GameManager_ToggleMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Menu.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        CheckForMenuToggleRequest();

	}

    void OnEnable()
    {
        SetInitRefs();
        Gmaster.ev_gameOver += ToggleMenu; // subscribe to game over event.
    }

    void OnDisable()
    {
        Gmaster.ev_gameOver -= ToggleMenu;

    }

    void SetInitRefs()
    {
        Gmaster = GetComponent<GameManager_Master.GameManager_Master>();
        
    }

    void CheckForMenuToggleRequest()
    {
        if(Input.GetKeyUp(KeyCode.Escape)&& !Gmaster.bGameOver && !Gmaster.bInvUIOn)
        {
            ToggleMenu();

        }
    }

    void ToggleMenu()
    {
        if(Menu!=null)
        {
            Menu.SetActive(!Menu.activeSelf);
            Gmaster.bMenuOn = !Gmaster.bMenuOn;
            Gmaster.CallEvent_MenuToggle();
        }
    }

    GameManager_Master.GameManager_Master Gmaster;
    public GameObject Menu;
}
