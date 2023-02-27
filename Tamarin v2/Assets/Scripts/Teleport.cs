using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject GorillaPlayer;

    public GameObject RespawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag"))
        {
            GorillaPlayer.transform.position = RespawnPoint.transform.position;
        }
        if (other.gameObject.CompareTag("Body"))
        {
            GorillaPlayer.transform.position = RespawnPoint.transform.position;
        }
        if (other.gameObject.CompareTag("MainCamera"))
        {
            GorillaPlayer.transform.position = RespawnPoint.transform.position;
        }
        if (other.gameObject.CompareTag("GorillaRig"))
        {
            GorillaPlayer.transform.position = RespawnPoint.transform.position;
        }
        /*

        */
    }
}
