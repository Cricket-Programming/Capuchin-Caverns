using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// AntiHandPhase.cs syncs the L Gorilla Origin's hand spheres with the controller. Essentially, its purpose is the prevent the hand from    
public class AntiHandPhase : MonoBehaviour
{
    [SerializeField] private Transform sphere;
    [SerializeField] private Transform controller;

    private void Update()
    {
        sphere.rotation = controller.rotation;
    }
}
