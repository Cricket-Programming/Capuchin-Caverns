using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using TMPro;
//goes on the enter button that the player pushes when they are ready to join the private room
public class JoinPrivateRoom : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        string roomCode = "aa";
        if (other.CompareTag("HandTag")) {
            PhotonVRManager.JoinPrivateRoom(roomCode, 20);
            Debug.Log(roomCode);

        }
    }
 
}
