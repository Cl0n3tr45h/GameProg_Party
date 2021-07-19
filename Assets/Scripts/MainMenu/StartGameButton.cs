using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public bool Is1Player;
    public void LoadScene()
    {
        if (Is1Player)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
