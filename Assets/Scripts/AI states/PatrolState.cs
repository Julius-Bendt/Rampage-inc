using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{

    Enemy e;
    private int currentPoint = 0;

    public int id => 1;
    public void Enter(Enemy enemy)
    {
        e = enemy;
        e.running = false;
    }

    public void Execute()
    {
        if(e.PatrolPoints.Length > 1)
        {
            if(Vector2.Distance(e.transform.position,e.PatrolPoints[currentPoint].position) < 0.5f)
            {
                currentPoint++;

                if (currentPoint > e.PatrolPoints.Length-1)
                    currentPoint = 0;
            }

            e.nextPatrolPos = e.PatrolPoints[currentPoint].position;

        }
    }

    public void ExecuteFixed()
    {
        e.Move(new Vector2(e.pathDir.x, e.pathDir.y));
        e.LookAt(e.pathDir);
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D o)
    {

    }
}
