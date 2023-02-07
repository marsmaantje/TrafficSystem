using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class Waypoint : MonoBehaviour
{

    [System.Serializable]
    public class WaypointData
    {
        public Waypoint waypoint;
        public float weight;
    }
    public List<WaypointData> connections;

    [Range(0f, 5f)]
    public float width = 1f;

    public Vector3 GetPosition()
    {
        //return random position on circle
        float angle = Random.Range(0f, 360f);
        float radius = Random.Range(0, width / 2f);
        Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        return transform.position + position;
    }

    public void TryAddConnection(Waypoint other, float weight = 1)
    {
        if (other == null)
            return;
        if (other == this)
            return;
        if (connections == null)
            connections = new List<WaypointData>();
        if (connections.Exists(x => x.waypoint == other))
            return;
        connections.Add(new WaypointData() { waypoint = other, weight = weight });
    }

    public void TryRemoveConnection(Waypoint toRemove)
    {
        if (connections == null)
            return;
        connections.RemoveAll(x => x.waypoint == toRemove);
    }
}
