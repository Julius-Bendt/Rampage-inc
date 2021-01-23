using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.LosPro;
using Pathfinding;

public class Enemy : Character, IObserverCallbacks, IListenerCallbacks
{
    public Transform target;

    [HideInInspector]
    public Vector2 lastKnowPosition = Vector2.zero;
    [HideInInspector]
    public Vector2 nextPatrolPos = Vector2.zero;
    [HideInInspector]
    public Vector3 pathDir;
    public float SHOOTDIST = 8;
    public Transform[] PatrolPoints;


    //State
    IEnemyState state;

    //Pathfinding
    [Header("Pathfinding")]
    private Seeker seeker;

    public Path path;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public float repathRate = 0.5f;
    private float lastRepath = float.NegativeInfinity;
    public bool reachedEndOfPath;


    public AudioSourceInfo? lastHeardAudioSource { get; protected set; }

    [HideInInspector]
    public float dist;

    public override void Start()
    {
        seeker = GetComponent<Seeker>();
        base.Start();
    }

    public override void Update()
    {
        dist = Vector2.Distance(lastKnowPosition, transform.position);
        PathFinding();
        base.Update();

        if (target != null) //vi ved hvor spilleren er, angrib
        {
            ChangeState(new AttackState());
        }
        else
        {
            if (lastKnowPosition.magnitude != 0) //vi ved ikke præcist hvor spilleren er, ryk hen til sidst kendte position
            {
                ChangeState(new SearchState());
            }
            else
            {
                ChangeState(new PatrolState()); //vi ved intet om spilleren, idle/patroller
            }
        }

        if (state != null)
            state.Execute();

    }


    public void FixedUpdate()
    {
        if (state != null)
            state.ExecuteFixed();


    }

    public void ChangeState(IEnemyState _state)
    {
        if (state != null)
        {
            if (state.id.Equals(_state.id))
                return;
            else
                state.Exit();
        }
          


        state = _state;
        state.Enter(this);
        Debug.Log(state);
    }


    public override void OnDie()
    {
        base.OnDie();
        App.Instance.killed++;
    }

    #region pathfinding
    public void OnPathComplete(Path p)
    {

        // Path pooling. To avoid unnecessary allocations paths are reference counted.
        // Calling Claim will increase the reference count by 1 and Release will reduce
        // it by one, when it reaches zero the path will be pooled and then it may be used
        // by other scripts. The ABPath.Construct and Seeker.StartPath methods will
        // take a path from the pool if possible. See also the documentation page about path pooling.
        p.Claim(this);
        if (!p.error)
        {
            if (path != null) path.Release(this);
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
        else
        {
            p.Release(this);
        }
    }

    public void PathFinding()
    {
        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            lastRepath = Time.time;

            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            Vector2 pos = (nextPatrolPos.magnitude != 0) ? nextPatrolPos : lastKnowPosition; //patrol, or hunt the player?
            seeker.StartPath(transform.position, pos, OnPathComplete);
        }

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        pathDir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

    }

    #endregion


    #region LOS
    public void OnTargetCameIntoRange(SightTargetInfo info)
    {
    
    }

    public void OnTargetWentOutOfRange(SightTargetInfo info)
    {
        if(info.target.gameObject.CompareTag("Player"))
        lastKnowPosition = info.lastSeenAt.Value.position;
    }

    public void OnTargetDestroyed(SightTargetInfo info)
    {

    }

    public void OnTryingToDetectTarget(SightTargetInfo info)
    {
        Debug.Log("OnTryingToDetectTarget");
        lastKnowPosition = info.target.gameObject.transform.position;
    }

    public void OnDetectingTarget(SightTargetInfo info)
    {
        Debug.Log("Detecting target!");
        lastKnowPosition = info.target.gameObject.transform.position;
    }

    public void OnDetectedTarget(SightTargetInfo info)
    {
        if (info.target.gameObject.CompareTag("Player"))
        {
            target = info.target.gameObject.transform;
        }
    }

    public void OnStopDetectingTarget(SightTargetInfo info)
    {
        
    }

    public void OnUnDetectedTarget(SightTargetInfo info)
    {
        
        if(info.target.gameObject.tag == "Player")
        {
            lastKnowPosition = target.position;
            target = null;
        }
            
            
    }

    public void OnHeardTarget(AudioSourceInfo info)
    {
        Debug.Log("heard target!");
        lastKnowPosition = info.lastHeardAtPosition.Value.position;
    }
    #endregion
}
