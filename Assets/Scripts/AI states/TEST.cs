using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.LosPro;
public class TEST : MonoBehaviour, IListenerCallbacks
{
    public void OnHeardTarget(AudioSourceInfo info)
    {
        Debug.Log("Heard target!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
