using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun_PR : MonoBehaviour
{

    bool done = false;
    public GameObject hero;
    public GameObject[] enemies = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position,hero.transform.position) < 7  &&  done == false)
        {
            //enemies ska god mod et bestemt punkt og kigge mod hero
            foreach (GameObject e in enemies)
            {
                e.GetComponent<Enemy>().target = hero.transform;
            }
            done = true;
        }
    }
}
