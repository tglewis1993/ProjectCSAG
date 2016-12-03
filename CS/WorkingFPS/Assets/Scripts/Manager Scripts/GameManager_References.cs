using UnityEngine;
using System.Collections;

public class GameManager_References : MonoBehaviour
{

    void OnEnable()
    {
        if(playerTag == "")
            Debug.LogWarning("NO PLAYER TAG ENTERED FOR GAME MANAGER REFERENCE!");
        if (enemyTag == "")
            Debug.LogWarning("NO ENEMY TAG ENTERED FOR GAME MANAGER REFERENCE!");

        if (playerTag != "")
            g_playerTag = playerTag;
        if (enemyTag != "")
            g_enemyTag = enemyTag;

        if(g_playerTag!="")
            PlayerRef = GameObject.FindGameObjectWithTag(g_playerTag);

    }

    void OnDisable()
    {

    }

    public string playerTag;
    public static string g_playerTag;

    public string enemyTag;
    public static string g_enemyTag;

    public static GameObject PlayerRef;


}
