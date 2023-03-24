using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [Header("THIS SCRIPT WAS MADE BY SAMSAM. IT IS NOT YOURS.")]
    [Header("Distributing This Script Will Lead To A Permanent Ban")]

    [Header("Gorilla Rig Stuff")]
    public Rigidbody GorillaPlayer;

    [Header("Default values are for red trampoline")]
    // default values are for trampoline
    public int XForce = 0;
    public int YForce = 30;
    public int ZForce = 0;

    //Private Stuff

    bool Triggered;
 
    
  void OnTriggerEnter() 
    {
        Triggered = true;
    
    }

    void OnTriggerExit()
    {
        Triggered = false;

    }

    void Update() 
    {
        if (Triggered)
        {
            GorillaPlayer.AddForce(new Vector3(XForce, YForce, ZForce), ForceMode.Impulse);
        }
    }
    }

