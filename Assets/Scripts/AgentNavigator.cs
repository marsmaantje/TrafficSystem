using System.Collections;
using System.Collections.Generic;
using TrafficSystem;
using UnityEngine;

public class AgentNavigator : MonoBehaviour
{
    [SerializeField] AgentController _controller;
    public Waypoint currentWaypoint;

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

        if (nextWaypoint != null)
        {
            currentWaypoint = nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }

    Waypoint getNextWaypoint()
    {
        if (currentWaypoint == null)
            return null;

        if (currentWaypoint.connections == null || currentWaypoint.connections.Count == 0)
            return null;
        
        float totalWeight = 0;
        foreach (var connection in currentWaypoint.connections)
        {
            totalWeight += connection.weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float currentWeight = 0;
        foreach (var connection in currentWaypoint.connections)
        {
            currentWeight += connection.weight;
            if (randomValue <= currentWeight)
            {
                return connection.waypoint;
            }
        }

        return null;
    }
}
