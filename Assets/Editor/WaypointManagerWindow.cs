using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>("Waypoint Editor");
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject window = new SerializedObject(this);

        EditorGUILayout.PropertyField(window.FindProperty("waypointRoot"));


        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical();
            DrawButtons();
            EditorGUILayout.EndHorizontal();
        }

        window.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>() != null)
        {
            Waypoint activeWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            if (GUILayout.Button("Create Branch"))
            {
                AddBranch(activeWaypoint);
            }
            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBetween(activeWaypoint.previousWaypoint, activeWaypoint, true);
            }
            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointBetween(activeWaypoint, activeWaypoint.nextWaypoint, false);
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint(activeWaypoint);
            }
        }
    }

    private void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if(waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;
            //position our new waypoint at the previous one
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypointObject;
    }

    private void CreateWaypointBetween(Waypoint previous, Waypoint next, bool positionAtNext = false)
    {
        GameObject newWaypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        newWaypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = newWaypointObject.GetComponent<Waypoint>();

        if (positionAtNext)
        {
            newWaypoint.transform.position = next.transform.position;
            newWaypoint.transform.forward = next.transform.forward;
            newWaypoint.width = next.width;
        }
        else
        {
            newWaypoint.transform.position = previous.transform.position;
            newWaypoint.transform.forward = previous.transform.forward;
            newWaypoint.width = previous.width;
        }

        newWaypoint.previousWaypoint = previous;
        newWaypoint.nextWaypoint = next;

        if (previous != null)
        {
            previous.nextWaypoint = newWaypoint;
        }

        if (next != null)
        {
            next.previousWaypoint = newWaypoint;
        }

        newWaypointObject.transform.SetSiblingIndex((previous != null ? previous.transform.GetSiblingIndex() : -1) + 1);

        Selection.activeGameObject = newWaypointObject;
    }

    private void AddBranch(Waypoint source)
    {
        GameObject branchWaypoint = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        branchWaypoint.transform.SetParent(waypointRoot, false);

        Waypoint branch = branchWaypoint.GetComponent<Waypoint>();

        branch.transform.position = source.transform.position;
        branch.transform.forward = source.transform.forward;
        branch.width = source.width;

        if (source.branches == null)
        {
            source.branches = new List<Waypoint>();
        }
        source.branches.Add(branch);

        Selection.activeGameObject = branchWaypoint;
    }

    private void RemoveWaypoint (Waypoint toRemove)
    {
        if (toRemove.previousWaypoint != null)
        {
            toRemove.previousWaypoint.nextWaypoint = toRemove.nextWaypoint;
        }

        if (toRemove.nextWaypoint != null)
        {
            toRemove.nextWaypoint.previousWaypoint = toRemove.previousWaypoint;
        }

        DestroyImmediate(toRemove.gameObject);
    }
}
