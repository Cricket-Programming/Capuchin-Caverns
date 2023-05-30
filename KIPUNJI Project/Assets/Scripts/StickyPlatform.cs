using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///makes the player move with the platform.
public class StickyPlatform : MonoBehaviour
{
    [SerializeField] Transform gorillaOrigin; //gorillaOrigin transform
    void OnCollisionEnter(Collision collision) 
    {
        gorillaOrigin.SetParent(transform);
    }
    void OnTriggerEnter() {
        gorillaOrigin.SetParent(transform);
    }
    void OnCollisionExit(Collision collision)
    {
        gorillaOrigin.SetParent(null);
    }
    void OnTriggerExit() {
        gorillaOrigin.SetParent(null);
    }
}
