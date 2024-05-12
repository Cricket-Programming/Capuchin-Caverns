using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ApplyForce sends the gorilla player's rigidbody flying based on forcesXYZ.

// Note: Small trampolines may not pick up rigidbody collision when the collider is not big enough (this is especially critical for trampolines because the rigidbody is moving so fast).
// Launchpads Sideways Trampoline Force: Vector3(16.1,2,23.5499992).


public class ApplyForce : MonoBehaviour
{
    [Header("Default values are for regular launchpads trampoline")]
    [Tooltip("You can create a empty gameobject and put it where you want the Gorilla to be. Then, you can calculate the forces based on the position you want.")]
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
