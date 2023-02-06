using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [System.Serializable]
    public class AgentSpawnData
    {
        public GameObject agentPrefab;
        public int count;
    }

    [SerializeField] List<AgentSpawnData> _spawnData;
    [SerializeField] List<Transform> _waypointRoots;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spawnData in _spawnData)
        {
            for (int i = 0; i < spawnData.count; i++)
            {
                //pick waypoint root
                Transform _waypointRoot = _waypointRoots[Random.Range(0, _waypointRoots.Count)];
                //pick waypoint to spawn agent at
                Waypoint targetWaypoint = _waypointRoot.GetChild(Random.Range(0, _waypointRoot.childCount)).GetComponent<Waypoint>();
                //spawn agent
                GameObject agent = Instantiate(spawnData.agentPrefab, targetWaypoint.transform.position, Quaternion.identity);
                AgentNavigator navigator = agent.GetComponent<AgentNavigator>();
                navigator.currentWaypoint = _waypointRoot.GetChild(Random.Range(0, _waypointRoot.childCount)).GetComponent<Waypoint>();
                navigator.reverse = Random.Range(0, 2) == 0;
            }
        }
    }
}
