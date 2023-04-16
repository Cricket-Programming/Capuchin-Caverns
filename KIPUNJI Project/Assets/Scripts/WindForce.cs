using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [Header("THIS SCRIPT WAS MADE BY SAMSAM. IT IS NOT YOURS.")]
    [Header("Distributing This Script Will Lead To A Permanent Ban")]

    [Header("Gorilla Player RigidBody")]
    public Rigidbody GorillaPlayer;

    [Header("Default values are for regular trampoline")]
    public int XForce = 0;
    public int YForce = 30;
    public int ZForce = 0;
 
    void OnTriggerEnter() 
    {
        GorillaPlayer.AddForce(new Vector3(XForce, YForce, ZForce), ForceMode.Impulse);
    }

}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [Header("THIS SCRIPT WAS MADE BY SAMSAM. IT IS NOT YOURS.")]
    [Header("Distributing This Script Will Lead To A Permanent Ban")]

    [Header("Gorilla Player RigidBody")]
    public Rigidbody GorillaPlayer;

    [Header("Default values are for regular trampoline")]
    public int XForce = 0;
    public int YForce = 30;
    public int ZForce = 0;

    private bool flag = false;
 
    void OnTriggerEnter() 
    {
        if (flag == false) {
            ApplyForce();
        }
        
        trigger = true;

    }

    void ApplyForce() {
        if (trigger == false) {
            GorillaPlayer.AddForce(new Vector3(XForce, YForce, ZForce), ForceMode.Impulse);
        }

        trigger = false;
    }
}


*/
