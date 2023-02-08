# <b>TrafficSystem</b>
A simple traffic system in unity

The traffic system works with waypoints, each waypoint has a position, radius and a list of connections to other waypoints.
<br><br>

### <b>Waypoints</b>
Each Waypoint has a selectable gizmo in the scene view, and shows its radius and connections with neighbours.
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/WaypointSceneView.png" alt="WaypointSceneView.png" width=100%>
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/WaypointInspector.png" alt="WaypointInspector.png" width=100%>
<br>
With this you can create a grid/mesh of connected waypoints that an agent can traverse.
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/WaypointSceneViewGrid.png" alt="WaypointSceneViewGrid.png" width=100%>
<br>
The Waypoints also come with a manager window, this window can be found under `Tools->Waypoint Editor`<br>
This window allows for quickly creating and connecting new waypoints with eachother. When created you do need to set its Waypoint Root, this is the gameobejct under which it will spawn newly created waypoints.
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/WaypointEditorInspector.png" alt="WaypointEditorInspector.png" width=100%>
<br><br>
The Agent part of the system consists of an agent navigator and an agent controller.
<br><br>

### <b>Agent Navigator</b>
The agent navigator is responsible for picking a new target every time the agent reaches its current one, this is essentially the overarching "brain" of the agent.
Provided in the demo is a simple implementation of this that randomly picks a new target when the current one is reached.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/AgentNavigatorInspector.png" alt="AgentNavigatorInspector.png" width=100%>
<br><br>

### <b>Agent Controller</b>
The agent controller is responsible for "driving" the agent to its target, this target is a point in 3d space, set by the agent navigator. Once it reached this target it should invoke the `OnDestinationReached` UnityEvent so the agent navigator can pick a new target.

Provided in the demo are two implementations of the agent controller. The `BasicAgentController` simply moves directly towards its target at a constant speed, whereas the `NavMeshAgentController` uses a NavMeshAgent to move towards its destination.
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/BasicAgentControllerInspector.png" alt="BasicAgentControllerInspector.png" width=100%>
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavMeshAgentControllerInspector.png" alt="NavMeshAgentControllerInspector.png" width=100%>
<br><br>

### <b>Agent Spawner</b>
The agent spawner is responsible for exactly what it says, it spawns agents. You can configure what agents it spawns and their initial settings through the inspector, and after the script is loaded it spawns all of them.
<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/AgentSpawnerInspector.png" alt="AgentSpawnerInspector.png" width=100%>
<br><br>

## <b>Additional Credits</b>
* Vocel city and people by Mike Judge
<br>https://github.com/mikelovesrobots/mmmm
<br>licensed under Creative Commons Attribution 4.0 International
<br>https://creativecommons.org/licenses/by/4.0/