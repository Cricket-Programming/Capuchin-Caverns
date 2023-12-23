using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//teleports the player
public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private GameObject mapToDisable;
    [SerializeField] private Transform gorillaPlayer;
    private Rigidbody gorillaPlayerRigidbody;
    
    [SerializeField] private Transform respawnLocation;

    private void Start() {
        //gets the gorillaPlayer's Rigidbody.
        if (!gorillaPlayer.TryGetComponent(out gorillaPlayerRigidbody)) {
            Debug.LogError("In order to access the rigidbody, make sure the name of the gorilla player is `GorillaPlayer`");
        }    
    }
    private void OnTriggerEnter() {
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        // Disable the map temporarily
        mapToDisable.SetActive(false);

        // allow player to only be affected by game code instead of physics engine/ basically stops player's movement.
        gorillaPlayerRigidbody.isKinematic = true;

        // slight delay to allow the above code to execute
        yield return new WaitForSeconds(0.1f);
        
        // Teleport the player to the respawn location
        gorillaPlayer.position = respawnLocation.position; 
        
        // slight delay to allow the above code to execute before re-enabling the map
        yield return new WaitForSeconds(0.1f);

        // Re-enable the Rigidbody's movement
        gorillaPlayerRigidbody.isKinematic = false;

        // Re-enable the map
        mapToDisable.SetActive(true);
    }
}


/*
    //This code teleports the player back if he falls out of the map. Keep this code in case need to implement this in the future. 
    //Bug: If non-horror maps blocking path to respawn location, this will fail.
    // private void teleportIfFall()
    // {
    //     if (gorillaPlayer.position.y < 15)
    //     {
    //         //Debug.Log("Teleporting");
    //         StartCoroutine(Teleport());
    //     }
    // }

    // private void Update() {
    //     teleportIfFall();      
    // }
*/
