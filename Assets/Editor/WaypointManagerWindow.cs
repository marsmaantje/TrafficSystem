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
            if(GUILayout.Button("Connect active to selected"))
            {
                List<Waypoint> targets = new List<Waypoint>();
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    if (gameObject.GetComponent<Waypoint>() != null)
                    {
                        targets.Add(gameObject.GetComponent<Waypoint>());
                    }
                }
                ConnectWaypoints(new List<Waypoint>{ activeWaypoint}, targets);
            }

            if(GUILayout.Button("Connect selected to active"))
            {
                List<Waypoint> targets = new List<Waypoint>();
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    if (gameObject.GetComponent<Waypoint>() != null)
                    {
                        targets.Add(gameObject.GetComponent<Waypoint>());
                    }
                }
                ConnectWaypoints(targets, new List<Waypoint> { activeWaypoint });
            }

            if (GUILayout.Button("Connect both ways"))
            {
                List<Waypoint> targets = new List<Waypoint>();
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    if (gameObject.GetComponent<Waypoint>() != null)
                    {
                        targets.Add(gameObject.GetComponent<Waypoint>());
                    }
                }
                ConnectWaypoints(targets, new List<Waypoint> { activeWaypoint });
                ConnectWaypoints(new List<Waypoint> { activeWaypoint }, targets);
            }

            if (GUILayout.Button("Remove connections between selection"))
            {
                List<Waypoint> targets = new List<Waypoint>();
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    if (gameObject.GetComponent<Waypoint>() != null)
                    {
                        targets.Add(gameObject.GetComponent<Waypoint>());
                    }
                }
                RemoveConnections(targets, targets);
            }
            SceneView.RepaintAll();
            
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint(activeWaypoint);
            }
        }
    }

    private void CreateWaypoint()
    {
        GameObject newWaypoint = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        newWaypoint.transform.SetParent(waypointRoot);
        newWaypoint.transform.position = Selection.activeGameObject.transform.position;

        Selection.SetActiveObjectWithContext(newWaypoint, null);
    }
    
    private void ConnectWaypoints(List<Waypoint> from, List<Waypoint> to)
    {
        foreach (Waypoint fromWaypoint in from)
        {
            foreach (Waypoint toWaypoint in to)
            {
                if (fromWaypoint != toWaypoint)
                {
                    fromWaypoint.TryAddConnection(toWaypoint);
                }
            }
        }
    }

    private void RemoveConnections(List<Waypoint> from, List<Waypoint> to)
    {
        foreach (Waypoint fromWaypoint in from)
        {
            foreach (Waypoint toWaypoint in to)
            {
                if (fromWaypoint != toWaypoint)
                {
                    fromWaypoint.TryRemoveConnection(toWaypoint);
                }
            }
        }
    }



    private void RemoveWaypoint (Waypoint toRemove)
    {
        foreach (Waypoint.WaypointData waypointData in toRemove.connections)
        {
            waypointData.waypoint.TryRemoveConnection(toRemove);
        }
        
        DestroyImmediate(toRemove.gameObject);
    }
}
