using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//camelcase for variables, pascalcase for function names
public class TeleportWithDelay : MonoBehaviour
{
    public GameObject mapToDisable;

    public Transform gorillaPlayer;

    //public List<GameObject> targetObjects;
    public Transform jumpscareLocation;
    public Transform respawnLocation;

    public float jumpscareRunningTime;

    //jumpscareObjects are the objects 
    public GameObject jumpscareObjects;
    public AudioSource jumpscareSound;


    void teleportIfFall()
    {
        if (gorillaPlayer.position.y < 15)
        {
            //Debug.Log("Teleporting");
            StartCoroutine(Teleport());
        }
    }

    void Update() {
        teleportIfFall();      
    }

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
        //yield retru
        // Teleport the player to the respawn location
        gorillaPlayer.position = respawnLocation.position;



        // Re-enable the map
        mapToDisable.SetActive(true);
    }
       
    //the jumpscare is NOT networked which is good (like 3rd person)

}
/*

    IEnumerator Teleport() //and jumpscare
    {
        //when the map is disabled and the player is the masterclient, Fluffy won't move around the navmesh anymore since it is disabled.
        //By having a wait of 0.01 seconds, the player can get to the jumpscare location, but other players will only see Fluffy stop moving for 0.01 seconds
        mapToDisable.SetActive(false);   

        gorillaPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.01f);
        gorillaPlayer.position = jumpscareLocation.position;
        //gorillaPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        jumpscareObjects.SetActive(true);
        jumpscareSound.Play();
        //yield return new WaitForSeconds(0.01f);
        mapToDisable.SetActive(true);
        //gorillaPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(jumpscareRunningTime); //This is the time that I allow the jumpscare to run

        gorillaPlayer.position = respawnLocation.position;
        jumpscareObjects.SetActive(false);
        

           
    }











using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//camelcase for variables, pascalcase for function names
public class TeleportWithDelay : MonoBehaviour
{
    public GameObject mapToDisable;

    public Transform gorillaPlayer;

    //public List<GameObject> targetObjects;
    public Transform jumpscareLocation;
    public Transform respawnLocation;

    public float jumpscareRunningTime;

    //jumpscareObjects are the objects 
    public GameObject jumpscareObjects;
    public AudioSource jumpscareSound;


    void teleportIfFall()
    {
        if (gorillaPlayer.position.y < 15)
        {
            //Debug.Log("Teleporting");
            StartCoroutine(Teleport());
        }
    }

    void Update() {
        teleportIfFall();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(gorillaPlayer)) {
           StartCoroutine(Teleport());  
        }
            
    }

    IEnumerator Teleport() //and jumpscare
    {
        mapToDisable.SetActive(false);   
        
        gorillaPlayer.position = jumpscareLocation.position;

        jumpscareObjects.SetActive(true);
        jumpscareSound.Play();

        yield return new WaitForSeconds(jumpscareRunningTime +12233);

        gorillaPlayer.position = respawnLocation.position;
        jumpscareObjects.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        mapToDisable.SetActive(true);
           
    }
    //the jumpscare is NOT networked which is good (like 3rd person)


}


*/
