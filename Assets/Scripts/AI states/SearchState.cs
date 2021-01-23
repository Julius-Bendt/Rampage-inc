using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IEnemyState
{
    Enemy e;

    public int id => 0;

    public void Enter(Enemy enemy)
    {
        e = enemy;
        e.running = false;
    }

    public void Execute()
    {

    }

    public void ExecuteFixed()
    {
        if (e.dist > 0.1f)
        {
            e.LookAt(e.lastKnowPosition);
        }

        if (e.dist > 0.1f && e.dist > e.SHOOTDIST * 0.75f && e.lastKnowPosition.magnitude != 0)
        {
            e.Move(new Vector2(e.pathDir.x, e.pathDir.y));
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D o)
    {

    }
}
