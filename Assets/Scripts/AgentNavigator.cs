using System.Collections;
using System.Collections.Generic;
using TrafficSystem;
using UnityEngine;

public class AgentNavigator : MonoBehaviour
{
    [SerializeField] AgentController _controller;
    public Waypoint currentWaypoint;
    public bool reverse = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_controller == null)
            _controller = GetComponent<AgentController>();
        
        if (currentWaypoint != null)
        {
            _controller.SetDestination(currentWaypoint.GetPosition());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDestinationReached(AgentController controller)
    {
        Waypoint nextWaypoint = getNextWaypoint();
        if (nextWaypoint == null)
        {
            reverse = !reverse;
            nextWaypoint = getNextWaypoint();
        }

        if (nextWaypoint != null)
        {
            currentWaypoint = nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }

    Waypoint getNextWaypoint()
    {
        if(currentWaypoint.setReverse)
        {
            reverse = Random.Range(0f, 1f) < currentWaypoint.reverseProbability;
        }
        
        bool willBranch;
        if(!reverse)
            willBranch = Random.Range(0f, 1f) < currentWaypoint.forwardBranchProbability;
        else
            willBranch = Random.Range(0f, 1f) < currentWaypoint.reverseBranchProbability;

        if (willBranch && currentWaypoint.branches.Count > 0)
        {
            return currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)];
        } else
        {
            if (!reverse)
            {
                if (currentWaypoint.nextWaypoint != null)
                {
                    return currentWaypoint.nextWaypoint;
                }
            }
            else
            {
                if (currentWaypoint.previousWaypoint != null)
                {
                    return currentWaypoint.previousWaypoint;
                }
            }
        }
        
        return null;
    }
}
