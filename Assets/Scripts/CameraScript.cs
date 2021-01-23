using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MF;

public class CameraScript : MonoBehaviour
{

    public static Transform target;



    [SerializeField]
    public Gradient[] gradient;
    private GradientBackground gb;

    private ShakeBehavior shake;


    int loadedScene = -1;

    public void OnLevelLoaded()
    {
        
        gb = GetComponent<GradientBackground>();
        shake = GetComponent<ShakeBehavior>();

        //Random gradient pr level
        gb.Gradient = gradient[Random.Range(0, gradient.Length - 1)];
        gb.UpdateTheGradient();
    }

    private void Update()
    {
        if (target != null && App.Instance.isPlaying)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

    }

    public void Shake(float time = 0.1f)
    {
        shake.TriggerShake(time);
    }


}
