using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHandPhase : MonoBehaviour
{
    public Transform sphere;
    public Transform controller;

    void Update()
    {
        sphere.rotation = controller.rotation;
    }
}
