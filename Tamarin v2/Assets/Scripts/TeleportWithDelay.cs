using System.Collections;
using UnityEngine;

public class TeleportWithDelay : MonoBehaviour
{
    // Reference to the object to disable
    public GameObject objectToDisable;

    // Reference to the object to teleport
    public GameObject objectToTeleport;

    // Reference to the target object
    public GameObject targetObject;

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

        // Teleport the object
        objectToTeleport.transform.position = new Vector3(objectToTeleport.transform.position.x, objectToTeleport.transform.position.y + 7, objectToTeleport.transform.position.z); //y is the up direction
        objectToTeleport.transform.position = targetObject.transform.position;

        // Wait for the specified time after teleporting
        yield return new WaitForSeconds(waitTimeAfterTeleport);

        // Enable the object
        objectToDisable.SetActive(true);
    }
}
