using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using TMPro;
//goes on the enter button that the player pushes when they are ready to join the private room
public class JoinPrivateRoom : MonoBehaviour
{
    [SerializeField] private JoinPrivateRoomNameDisplay JoinPrivateRoomNameDisplay;
    [SerializeField] private TMP_Text LeaveWarningToShow;
    private void Start() {
        LeaveWarningToShow.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HandTag")) {
            PhotonVRManager.Manager.Disconnect();
            Invoke("x", 1f);
            LeaveWarningToShow.gameObject.SetActive(true);
        }
    }

    // Checking if the room is invisible
    private void x() {
        PhotonVRManager.JoinPrivateRoom(JoinPrivateRoomNameDisplay.GetRoomName(), 20);
    }
    private static bool IsAlphabetic(string input) {
        foreach (char c in input) {
            if (!char.IsLetter(c)) {
                return false;
            }
        }
        return true;
    }
 
}
