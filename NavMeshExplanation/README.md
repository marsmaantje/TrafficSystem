# <b>NavMeshes in Unity</b>
NavMeshes can be very usefull, they allow characters to find paths to their destination and follow the ground while doing so.

In Unity, NavMeshes are built in, there is no need to download any special addons from the asset store or program complicated pathfinding AI's since Unity already has all the components and tools nescesary to set it up.
The system consists of two parts, the NavMesh and the Agent.
<br><br>

## <b>NavMesh</b>
The NavMesh is the static part of the system, this is the surface agents walk over and the object that determines what surfaces are walkable and which ones are not.<br>
To generate the NavMesh you need to do the following steps:<br>

1. Mark all gameobjects that you want to be part of the navmesh (ground and walls) as `Navigation Static`, this tells the mesh generator to use these in the generation of the NavMesh.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/MarkNavigationStatic.png" alt="MarkNavigationStatic.png" width=50%><br>

2. Open the `Navigation` window. It can be found under `Window->AI>Navigation`<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavigationInspectorLocation.png" alt="NavigationInspectorLocation.png" width=50%><br>

3. While in the `Object` tab, you can select one or more meshrenderes in the scene and change how the generator should handle them.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavigationInspectorObject.png" alt="NavigationInspectorObject.png" width=50%><br>

4. Finally, over in the `bake` tab, you can specify the settings for baking, and finally bake the NavMesh.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavigationInspectorBake.png" alt="NavigationInspectorBake.png" width=50%><br>
This will create a new folder in the same folder your current scene is in with the same name and place the baked NavMesh asset in there.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavigationInspectorBakeFolder.png" alt="NavigationInspectorBakeFolder.png" width=50%><br>
<br><br>

## <b>NavMeshAgent</b>
The NavMeshAgent is a component that drives its attached GameObject over the NavMesh based on its settings.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavMeshAgentInspector.png" alt="NavMeshAgentInspector.png" width=50%><br>
To get the agent to move to a location, you need to reference it in a script and use the `NavMeshAgent.SetDestination(Vector3 target)` method.
With this method you tell the Agent what position in world transform it should try to go towards, it will go towards the nearest point on the NavMesh.<br>
<img src="https://raw.githubusercontent.com/marsmaantje/TrafficSystem/main/readmeImages/NavMeshAgentSetDestination.png" alt="NavMeshAgentSetDestination.png" width=50%><br>
To then check whether the Agent has reached the target, you can check the `NavMeshAgent.RemainingDistance` field, this returns how many units the agent still has to go untill its target.<br>
<b>Note:</b> the `RemainingDistance` returns infinity if the agent still has to go around a corner. To fix this, you could use this extension method instead to check the remaining distance:<br>
```cs
using UnityEngine;
using UnityEngine.AI;

public static class ExtensionMethods
{
    public static float GetPathRemainingDistance(this NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }
        
        return distance;
    }
}
```