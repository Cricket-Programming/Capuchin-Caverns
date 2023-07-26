using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    public GameObject mapToDisable;

    public Transform gorillaPlayer;

    public Transform jumpscareLocation;
    public Transform respawnLocation;

    public float jumpscareRunningTime;

    //jumpscareObjects are the things like the box that shows around the player
    public GameObject jumpscareObjects;
    public AudioSource jumpscareSound;


    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Teleport());
        if (other.transform.IsChildOf(gorillaPlayer)) {
           
        }   
            
    }

        //when the map is disabled and the player is the masterclient, Fluffy won't move around the navmesh anymore since it is disabled.
        //By having a wait of 0.01 seconds, the player can get to the jumpscare location, but other players will only see Fluffy stop moving for 0.01 seconds
    IEnumerator Teleport()
    {
        // Disable the map temporarily
        mapToDisable.SetActive(false);

        // Stop the player's movement
        Rigidbody gorillaRigidbody = gorillaPlayer.GetComponent<Rigidbody>();
        gorillaRigidbody.isKinematic = true;

        // Teleport the player to the jumpscare location
        gorillaPlayer.position = jumpscareLocation.position;

        // Activate the jumpscare objects and play the jumpscare sound
        jumpscareObjects.SetActive(true);
        jumpscareSound.Play();

        // Wait for the jumpscare running time
        yield return new WaitForSeconds(jumpscareRunningTime);

        // Deactivate the jumpscare objects
        jumpscareObjects.SetActive(false);

        // Re-enable the Rigidbody's movement
        gorillaRigidbody.isKinematic = false;

        // Teleport the player to the respawn location
        gorillaPlayer.position = respawnLocation.position;

        // Re-enable the map
        mapToDisable.SetActive(true);
    }
       
    //the jumpscare is NOT networked which is good (like 3rd person)

}


/*
    //This code teleports the player back if he falls out of the map. Keep this code in case need to implement this in the future. 
    //Bug: If non-horror maps blocking path to respawn location, this will fail.
    // void teleportIfFall()
    // {
    //     if (gorillaPlayer.position.y < 15)
    //     {
    //         //Debug.Log("Teleporting");
    //         StartCoroutine(Teleport());
    //     }
    // }

    // void Update() {
    //     teleportIfFall();      
    // }
*/
