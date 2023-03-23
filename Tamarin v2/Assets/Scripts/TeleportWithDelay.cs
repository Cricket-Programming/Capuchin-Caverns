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
        if (objectToTeleport.transform.position.y < 15)
        {
            objectToDisable.SetActive(false);
            objectToTeleport.transform.position = targetObjects[targetObjects.Count-1].transform.position;
            StartCoroutine(x());

        }
    }
    IEnumerator x()
    {
        yield return new WaitForSeconds(0.2f);
        objectToDisable.SetActive(true);
    }
    public void OnTriggerEnter()
    {
        // Disable the object
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
        if (objectToTeleport.transform.position.y < 15)
        {
            objectToDisable.SetActive(false);
            objectToTeleport.transform.position = targetObjects[targetObjects.Count-1].transform.position;
            StartCoroutine(x());

        }
    }
    IEnumerator x()
    {
        yield return new WaitForSeconds(0.2f);
        objectToDisable.SetActive(true);
    }
    public void OnTriggerEnter()
    {
        // Disable the object
        objectToDisable.SetActive(false);

        // Wait for the specified time before teleporting
        StartCoroutine(WaitAndTeleport(waitTimeBeforeTeleport));      
    }

    IEnumerator WaitAndTeleport(float waitTime) //change this to IEnumerator
    {
        yield return new WaitForSeconds(waitTime);
        
        foreach (var obj in targetObjects)
        {
            objectToTeleport.transform.position = obj.transform.position;
        }
        

        yield return new WaitForSeconds(waitTimeAfterTeleport);

        objectToDisable.SetActive(true);
           
    }

}

Rising lava thing script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    //https://www.youtube.com/watch?v=6uAFX3ktuzw by pear <3
    public GameObject GorillaPlayer;

    public GameObject RespawnPoint;

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Body"))
            {
            GorillaPlayer.transform.position = RespawnPoint.transform.position;
        }
        if (other.gameObject.CompareTag("MainCamera"))
        {
            GorillaPlayer.transform.position = RespawnPoint.transform.position;
        }
    }
}
*/
