using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHandPhase : MonoBehaviour
{
    [SerializeField] private Transform sphere;
    [SerializeField] private Transform controller;

    private void Update()
    {
        sphere.rotation = controller.rotation;
    }
}
