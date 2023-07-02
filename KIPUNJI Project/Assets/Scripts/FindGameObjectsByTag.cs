using UnityEngine;
using System;
using System.Collections;
public class FindGameObjectsByTag : MonoBehaviour
{
    private void Start()
    {
        // Find all GameObjects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Train Track Waypoint");

        // Iterate over the taggedObjects array
        foreach (GameObject taggedObject in taggedObjects)
        {
            // Perform your operations on each taggedObject
            Debug.Log(taggedObject.name);
        }
    }
}
