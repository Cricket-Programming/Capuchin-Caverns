using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [Header("Thanks Samsam for part of this script")]

    // [Header("Gorilla Player RigidBody")]
    // public Rigidbody GorillaPlayer;

    [Header("Default values are for regular launchpad trampoline")]
    [SerializeField] private Vector3 forcesXYZ = new Vector3(0, 30, 0);
 
    private Rigidbody gorillaPlayerRigidbody;

    private void Start() {
        GameObject gorillaPlayer = GameObject.Find("GorillaPlayer");
        if (gorillaPlayer == null) { 
            Debug.LogError("In order to access the rigidbody, make sure the name of the gorilla player is `GorillaPlayer`");
        }
        else {
            gorillaPlayerRigidbody = gorillaPlayer.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerEnter() {
        gorillaPlayerRigidbody.AddForce(forcesXYZ, ForceMode.Impulse);
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
