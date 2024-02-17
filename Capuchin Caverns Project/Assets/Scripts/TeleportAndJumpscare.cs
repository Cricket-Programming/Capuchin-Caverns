using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Teleports the player and jumpscares them. TeleportAndJumpscare.cs is meant for horror purposes.
[RequireComponent(typeof(AudioSource))]
public class TeleportAndJumpscare : MonoBehaviour
{
    [SerializeField] private GameObject mapToDisable;
    [SerializeField] private Transform gorillaPlayer;
    private Rigidbody gorillaPlayerRigidbody;

    [SerializeField] private Transform jumpscareLocation;
    [SerializeField] private Transform respawnLocation;

    [SerializeField] private float jumpscareRunningTime = 2f;

    // jumpscareObjects are the things like the box that shows around the player
    [SerializeField] private GameObject jumpscareObjects;
    [SerializeField] private AudioSource jumpscareSound;

    private void Start() {
        //gets the gorillaPlayer's Rigidbody.
        if (!gorillaPlayer.TryGetComponent(out gorillaPlayerRigidbody)) {
            Debug.LogError("In order to access the rigidbody, make sure the name of the gorilla player is `GorillaPlayer`");
        }    
    }

    private void OnTriggerEnter() {
        StartCoroutine(Teleport());        
    }

    // When the map is disabled and the player is the MasterClient, Fluffy won't move around the navmesh anymore since it is disabled.
    // By having a wait of 0.01 seconds, the player can get to the jumpscare location, but other players will only see Fluffy stop moving for 0.01 seconds
    private IEnumerator Teleport()
    {
        // Disable the map temporarily.
        mapToDisable.SetActive(false);

        // allow player to only be affected by game code instead of physics engine/ basically stops player's movement.
        gorillaPlayerRigidbody.isKinematic = true;

        jumpscareSound.Play();

        // slight delay to allow the above code to execute
        yield return new WaitForSeconds(0.02f);

        // Teleport the player to the jumpscare location
        gorillaPlayer.position = jumpscareLocation.position;

        // Activate the jumpscare objects
        jumpscareObjects.SetActive(true);

        // Wait for the jumpscare running time
        yield return new WaitForSeconds(jumpscareRunningTime);

        // Deactivate the jumpscare objects
        jumpscareObjects.SetActive(false);

        // Re-enable the Rigidbody's movement
        gorillaPlayerRigidbody.isKinematic = false;

        // Teleport the player to the respawn location
        gorillaPlayer.position = respawnLocation.position;
    
        // Re-enable the map
        mapToDisable.SetActive(true);
    }
       
    // The jumpscare is NOT networked which is good (like 3rd person).

}


/*
    //This code teleports the player back if he falls out of the map. Keep this code in case it needs to implement this in the future. 
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
