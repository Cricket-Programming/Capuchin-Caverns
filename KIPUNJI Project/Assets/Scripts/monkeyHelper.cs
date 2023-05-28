using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class monkeyHelper : Editor
{
    [UnityEditor.MenuItem("Monkey Helper/Uh/Force Cleanup NavMesh")]
    public static void ForceCleanupNavMesh()
    {
        if (Application.isPlaying)
            return;
 
        UnityEngine.AI.NavMesh.RemoveAllNavMeshData();
    }
}
