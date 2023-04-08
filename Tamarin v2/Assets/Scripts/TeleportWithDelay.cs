using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//camelcase for variables, pascalcase for function names
public class TeleportWithDelay : MonoBehaviour
{
    // Reference to the object to disable
    public GameObject objectToDisable;

    // Reference to the object to teleport
    public GameObject objectToTeleport;
    public Transform fluffyJumpscareObject;

    // Reference to the target object
    //public List<GameObject> targetObjects = new List<GameObject>();
    public Transform jumpscareLocation;
    public Transform respawnLocation;

    // Time to wait before teleporting (in seconds)
    public float waitTime = .5f;
    public AudioSource jumpscareSound;

    private Vector3 previousPos;

    void teleportIfFall()
    {
        if (objectToTeleport.transform.position.y < 15)
        {
            objectToDisable.SetActive(false);
            objectToTeleport.transform.position = respawnLocation.position;
            Invoke("showDisableObject", 0.2f);
        }
    }
    void showDisableObject() { objectToDisable.SetActive(true); }

    void Update() {
        teleportIfFall();
        objectToTeleport.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void OnTriggerEnter()
    {
        StartCoroutine(TeleportAndJumpScare());      
    }

    IEnumerator TeleportAndJumpScare() 
    {
        objectToDisable.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        objectToTeleport.transform.position = jumpscareLocation.position;



        jumpscareSound.Play();
        yield return new WaitForSeconds(waitTime);

        //objectToTeleport.transform.position = respawnLocation.position;

        yield return new WaitForSeconds(0.1f);

        showDisableObject();
           
    }

}


//teleport if fall off map



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    // Reference to the object to disable
    public GameObject objectToDisable;

    // Reference to the object to teleport
    public GameObject objectToTeleport;

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
        if (objectToTeleport.transform.position.y < 15)
        {
            objectToDisable.SetActive(false);
            objectToTeleport.transform.position = targetObjects[targetObjects.Count-1].transform.position;
            Invoke("showObject", 0.2f);

        }
    }
    void showObject() {
        objectToDisable.SetActive(true);
    }


    void OnTriggerEnter()
    {
        objectToDisable.SetActive(false);

        // Wait for the specified time before teleporting
        StartCoroutine(WaitAndTeleport(waitTimeBeforeTeleport));      
    }

    IEnumerator WaitAndTeleport(float waitTime) //change this to IEnumerator
    {
        yield return new WaitForSeconds(waitTime);

        foreach (var obj in targetObjects) {
            objectToTeleport.transform.position = obj.transform.position;
        }
    
        yield return new WaitForSeconds(waitTimeAfterTeleport);
        objectToDisable.SetActive(true);
           
    }

}


*/
