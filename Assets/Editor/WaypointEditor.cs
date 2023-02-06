using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[ExecuteAlways]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        //draw sphere
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.4f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.15f);

        Gizmos.color = Color.white;

        //draw width line
        float halfWidth = waypoint.width / 2f;
        Gizmos.DrawLine(waypoint.transform.position - (waypoint.transform.right * halfWidth),
            waypoint.transform.position + (waypoint.transform.right * halfWidth));

        //draw connection lines
        if(waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 from = waypoint.transform.position + waypoint.transform.right * halfWidth;
            Vector3 to = waypoint.previousWaypoint.transform.position + waypoint.previousWaypoint.transform.right * (waypoint.previousWaypoint.width / 2f);

            Gizmos.DrawLine(from, to);
        }

        if (waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 from = waypoint.transform.position - waypoint.transform.right * halfWidth;
            Vector3 to = waypoint.nextWaypoint.transform.position - waypoint.nextWaypoint.transform.right * (waypoint.nextWaypoint.width / 2f);

            Gizmos.DrawLine(from, to);
        }

        if(waypoint.branches != null)
        {
            Gizmos.color = Color.blue;
            foreach (Waypoint branch in waypoint.branches)
            {
                Vector3 from = waypoint.transform.position;
                Vector3 to = branch.transform.position;

                Gizmos.DrawLine(from, to);
            }
        }
    }
}
