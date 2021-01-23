using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Sceneloader;

public class LoadMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        SceneLoader.LoadScene("menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
