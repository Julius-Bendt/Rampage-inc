using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Sceneloader;

public class MenuScript : MonoBehaviour
{

    public void startGame()
    {
        SceneLoader.LoadScene("level 1");
    }

    public void Help()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
