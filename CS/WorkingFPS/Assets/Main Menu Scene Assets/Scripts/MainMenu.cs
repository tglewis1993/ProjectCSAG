using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void playGame()
    {
        SceneManager.LoadScene(1);

    }

    public void quitGame()
    {
        Application.Quit();

    }
}
