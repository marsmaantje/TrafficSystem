using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        //draw width radius circle
        float halfWidth = waypoint.width / 2f;
        Gizmos.DrawWireSphere(waypoint.transform.position, halfWidth);

        //draw connection lines
        if (waypoint.connections != null)
        {
            foreach (Waypoint.WaypointData connection in waypoint.connections)
            {
                if (connection.waypoint == null)
                    continue;
                
                Gizmos.color = (Color.green * connection.weight).WithAlpha(1);
                Vector3 from = waypoint.transform.position;
                Vector3 to = connection.waypoint.transform.position;
                Quaternion direction = Quaternion.LookRotation(to - from);
                Vector3 right = direction * Vector3.right;
                Vector3 fromOffset = right * waypoint.width / 2f;
                Vector3 toOffset = right * connection.waypoint.width / 2f;

                Gizmos.DrawLine(from + fromOffset, to + toOffset);
            }
        }
    }
}
