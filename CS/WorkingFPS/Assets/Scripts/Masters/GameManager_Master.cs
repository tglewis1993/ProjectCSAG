using UnityEngine;
using System.Collections;


namespace GameManager_Master
{ 

    public class GameManager_Master : MonoBehaviour {


        //event handlers
        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler ev_menuToggle;
        public event GameManagerEventHandler ev_invUIToggle;
        public event GameManagerEventHandler ev_restartLevel;
        public event GameManagerEventHandler ev_gotoMenuScene;
        public event GameManagerEventHandler ev_gameOver;


        //event flags
        public bool bGameOver; // Is Game Over?
        public bool bInvUIOn;
        public bool bMenuOn;

        void Start () {
            Application.targetFrameRate = 289;
            bMenuOn = false;
            bInvUIOn = false;
            bGameOver = false;

        }
	

	    void Update () {
	
	    }

        public void CallEvent_MenuToggle()
        {
            if(ev_menuToggle != null)
            {
                ev_menuToggle();

            }
        }
        public void CallEvent_InvUIToggle()
        {
            if (ev_invUIToggle != null)
            {
                ev_invUIToggle();

            }
        }
        public void CallEvent_RestartLevel()
        {
            if (ev_restartLevel != null)
            {
                ev_restartLevel();

            }
        }
        public void CallEvent_GotoMenuScene()
        {
            if (ev_gotoMenuScene != null)
            {
                ev_gotoMenuScene();

            }
        }
        public void CallEvent_GameOver()
        {
            if (ev_gameOver != null)
            {
                bGameOver = true;
                ev_gameOver();

            }
        }
    }

}