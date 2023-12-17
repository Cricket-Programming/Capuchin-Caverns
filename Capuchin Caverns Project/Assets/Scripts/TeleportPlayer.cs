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

        // Stop the player's movement
        gorillaPlayerRigidbody.isKinematic = true;

        // //this slight delay is likely necessary. If it is necessary, it allows for the above code to have time to execute.
        yield return new WaitForSeconds(0.1f);

        // // Re-enable the Rigidbody's movement
        gorillaPlayerRigidbody.isKinematic = false;

        // Teleport the player to the respawn location
        gorillaPlayer.position = respawnLocation.position;
        yield return new WaitForSeconds(0.1f);
        // Re-enable the map
        mapToDisable.SetActive(true);
        //yield break;
    }
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
