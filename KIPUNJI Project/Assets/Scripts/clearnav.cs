/* Instead of this script, in order to clear the navmesh
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class ClearNav : Editor
{
    [UnityEditor.MenuItem("Light Brigade/Debug/Force Cleanup NavMesh")]
    public static void ForceCleanupNavMesh()
    {
        if (Application.isPlaying)
            return;
 
        UnityEngine.AI.NavMesh.RemoveAllNavMeshData();
    }
}
*/