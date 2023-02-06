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
        if(!reverse)
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
        
        return null;
    }
}
