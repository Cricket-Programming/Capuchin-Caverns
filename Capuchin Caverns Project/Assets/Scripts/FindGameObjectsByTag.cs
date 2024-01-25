using UnityEngine;
using System;
using System.Collections;
// This script is only used for utility purposes.
public class FindGameObjectsByTag : MonoBehaviour
{   
    [SerializeField] private string tagName;
    private void Start()
    {
        // Find all GameObjects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagName);

        // Iterate over the taggedObjects array
        foreach (GameObject taggedObject in taggedObjects)
        {
            // Perform your operations on each taggedObject
            Debug.Log(taggedObject.name);
        }
    }
}
