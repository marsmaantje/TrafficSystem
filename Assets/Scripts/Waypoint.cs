using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;
    public Waypoint previousWaypoint;
    
    public List<Waypoint> branches;
    [Range(0f, 1f)]
    public float branchProbability = 0.5f;

    [Range(0f, 5f)]
    public float width = 1f;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position - (transform.right * width / 2f);
        Vector3 maxBound = transform.position + (transform.right * width / 2f);

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }

    void OnDestroy()
    {
        if (nextWaypoint != null)
        {
            nextWaypoint.previousWaypoint = previousWaypoint;
        }
        if (previousWaypoint != null)
        {
            previousWaypoint.nextWaypoint = nextWaypoint;
        }
    }
}
