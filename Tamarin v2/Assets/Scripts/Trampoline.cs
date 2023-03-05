using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("THIS SCRIPT WAS MADE BY SAMSAM. IT IS NOT YOURS.")]
    [Header("Distributing This Script Will Lead To A Permanent Ban")]

    [Header("Gorilla Rig Stuff")]
    public Rigidbody GorillaPlayer;

    [Header("Force (Reccomended Force Is 10)")]
 
    public int Force;
    
  void OnTriggerEnter() 
    {
        GorillaPlayer.AddForce(new Vector3(0, Force, 0), ForceMode.Impulse);
    
    }
    }

