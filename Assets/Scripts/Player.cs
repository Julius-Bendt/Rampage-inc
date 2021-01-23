using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.LosPro;

public class Player : Character
{
    Camera cam;

    Vector2 input, mousePos;



    public override void Start()
    {
        if (LoadScene.w != null)
            weapon = LoadScene.w;

        base.Start();
        cam = Camera.main;

        CameraScript.target = gameObject.transform;
  

    }

    public override void OnDie()
    {
        base.OnDie();


        App.Instance.death++;
        App.Instance.isPlaying = false;
        App.Instance.respawn.PlayerDied();
    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        if(weapon != null)
        {
            if (weapon.autoFire)
                fire = Input.GetButton("Fire");
            else
                fire = Input.GetButtonDown("Fire");
        }

    }

    public void FixedUpdate()
    {


        Move(input.normalized);
        LookAt(mousePos);
        

    }
}
