using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinPrivateRoom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HandTag")) {
            PhotonVRManager.JoinPrivateRoom
        }
    }
}
