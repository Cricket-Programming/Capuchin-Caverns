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
    public float waitTimeBeforeTeleport = 1.0f;

    // Time to wait after teleporting (in seconds)
    public float waitTimeAfterTeleport = 1.0f;

    public void OnTriggerEnter()
    {
        // Disable the object
        objectToDisable.SetActive(false);

        // Wait for the specified time before teleporting
        StartCoroutine(WaitAndTeleport(waitTimeBeforeTeleport));      
    }

    IEnumerator WaitAndTeleport(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        foreach (var obj in targetObjects)
        {
            objectToTeleport.transform.position = obj.transform.position;
        }
        //objectToTeleport.transform.position = targetObjects[4].transform.position;
        //objectToTeleport.transform.position = targetObjects[5].transform.position;
        // Wait for the specified time after teleporting
        yield return new WaitForSeconds(waitTimeAfterTeleport);

        // Enable the object
        objectToDisable.SetActive(true);
           
    }
}


/*

using System.Collections;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    // Reference to the object to disable
    public GameObject objectToDisable;

    // Reference to the object to teleport
    public GameObject objectToTeleport;

    // Reference to the target object
    
    public GameObject targetObject1;
    public GameObject targetObject2;
    public GameObject targetObject3;
    public GameObject targetObject4;
    public GameObject targetObject5;
    public GameObject targetObject6;

    // Time to wait before teleporting (in seconds)
    public float waitTimeBeforeTeleport = 1.0f;

    // Time to wait after teleporting (in seconds)
    public float waitTimeAfterTeleport = 1.0f;

    
    //BoxCollider enemyBox;
    void Start() 
    {
        //enemyBox = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter()
    {
        // Disable the object
        objectToDisable.SetActive(false);
        //enemyBox.enabled = false;

        // Wait for the specified time before teleporting
        StartCoroutine(WaitAndTeleport(waitTimeBeforeTeleport));
        
    }

    IEnumerator WaitAndTeleport(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Teleport the object
        //objectToTeleport.transform.position = new Vector3(objectToTeleport.transform.position.x, objectToTeleport.transform.position.y + 7f, objectToTeleport.transform.position.z); //y is the up direction
        //objectToTeleport.transform.position += Vector3.up * Time.deltaTime;
        
        objectToTeleport.transform.position = targetObject1.transform.position;
        //yield return new WaitForSeconds(waitTimeAfterTeleport);
        objectToTeleport.transform.position = targetObject2.transform.position;
        //yield return new WaitForSeconds(waitTimeAfterTeleport);
        objectToTeleport.transform.position = targetObject3.transform.position;
        //yield return new WaitForSeconds(waitTimeAfterTeleport);
        objectToTeleport.transform.position = targetObject4.transform.position;
        //yield return new WaitForSeconds(waitTimeAfterTeleport);
        objectToTeleport.transform.position = targetObject5.transform.position;
        //yield return new WaitForSeconds(waitTimeAfterTeleport);
        objectToTeleport.transform.position = targetObject6.transform.position;
        
        // Wait for the specified time after teleporting
        yield return new WaitForSeconds(waitTimeAfterTeleport);

        //enemyBox.enabled = true; 
        // Enable the object
        objectToDisable.SetActive(true);
           
    }
}


*/
