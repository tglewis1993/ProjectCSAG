using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugMenuNav : MonoBehaviour {

    Button Debug_Quit;

	// Use this for initialization
	void Start () {
        Debug_Quit = GetComponentInChildren<Button>();
        Debug_Quit.onClick.AddListener(Clicked);
	}
	
	// Update is called once per frame
	void Update () {
        if(Debug_Quit)
        {


        }
	}

    void Clicked()
    {

        Application.Quit();

    }
}
