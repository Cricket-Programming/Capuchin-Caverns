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

    public float waitTime;

    //jumpscareObjects are the objects 
    public GameObject jumpscareObjects;
    public AudioSource jumpscareSound;


    void teleportIfFall()
    {
        if (gorillaPlayer.position.y < 15)
        {
            Debug.Log("Teleporting");
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

        yield return new WaitForSeconds(waitTime);

        gorillaPlayer.position = respawnLocation.position;
        jumpscareObjects.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        mapToDisable.SetActive(true);
           
    }
    //the jumpscare is NOT networked which is good (like 3rd person)


}





/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    // Reference to the object to disable
    public GameObject mapToDisable;

    // Reference to the object to teleport
    public GameObject gorillaPlayer;

    // Reference to the target object
    public List<GameObject> targetObjects = new List<GameObject>();

    // Time to wait before teleporting (in seconds)
    public float waitTimeBeforeTeleport = .2f;

    // Time to wait after teleporting (in seconds)
    public float waitTimeAfterTeleport = .2f;

    void Update() {
        teleportIfFall();

    }
    void teleportIfFall()
    {
        if (gorillaPlayer.transform.position.y < 15)
        {
            mapToDisable.SetActive(false);
            gorillaPlayer.transform.position = targetObjects[targetObjects.Count-1].transform.position;
            Invoke("showObject", 0.2f);

        }
    }
    void showObject() {
        mapToDisable.SetActive(true);
    }


    void OnTriggerEnter()
    {
        mapToDisable.SetActive(false);

        // Wait for the specified time before teleporting
        StartCoroutine(WaitAndTeleport(waitTimeBeforeTeleport));      
    }

    IEnumerator WaitAndTeleport(float waitTime) //change this to IEnumerator
    {
        yield return new WaitForSeconds(waitTime);

        foreach (var obj in targetObjects) {
            gorillaPlayer.transform.position = obj.transform.position;
        }
    
        yield return new WaitForSeconds(waitTimeAfterTeleport);
        mapToDisable.SetActive(true);
           
    }

}


*/
