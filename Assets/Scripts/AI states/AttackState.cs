using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    Enemy e;
    public int id => 2;
    public void Enter(Enemy enemy)
    {
        e = enemy;
        e.running = true;
    }

    public void Execute()
    {
        if (e.target != null)
        {
            e.lastKnowPosition = e.target.position;
            e.fire = e.dist <= e.SHOOTDIST;
        }
        else
        {
            e.fire = false;
        }
    }

    public void ExecuteFixed()
    {
        if (e.dist > 0.1f)
        {
            e.LookAt(e.lastKnowPosition);
        }

        if (e.dist > 0.1f && e.dist > e.SHOOTDIST * 0.75f)
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
