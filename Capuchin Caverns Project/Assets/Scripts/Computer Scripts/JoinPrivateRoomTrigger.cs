using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JoinPrivateRoomScript;

//goes on the enter button that the player pushes when they are ready to join the private room
public class JoinPrivateRoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HandTag")) {
            JoinPrivateRoomManager.Manager.JoinPrivateRoomLogic();
        }   
    }
}





//this code has a lot of valuable insights, but it has BUGS: connects player to private room only when player is connected to photon, and player will not autoconnect to public room multiplayer when this script is not active.

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using Photon.VR;
// using Photon.Pun;
// using Photon.Realtime; //for Clientstate.Joining
// using TMPro;

// //goes on the enter button that the player pushes when they are ready to join the private room
// namespace JoinPrivateRoomScript {
//     public class JoinPrivateRoom1 : MonoBehaviourPunCallbacks
//     {
//         [SerializeField] private JoinPrivateRoomNameDisplay JoinPrivateRoomNameDisplay;
//         [SerializeField] private TMP_Text LeaveWarningToShow;
//         //C# property, shorthand syntax
//         public static JoinPrivateRoom1 Manager { get; private set; }
//         private bool inPrivateRoom = false;
//         private void Awake() {
//             if (Manager == null)
//                 Manager = this;
//             ShowLeaveInstructions(false);
//         }
//         //leave instructions is active when in private room, inactive when not in private room
//         public void ShowLeaveInstructions(bool value) {
//             LeaveWarningToShow.gameObject.SetActive(value);
//         }
//         public bool GetInPrivateRoom() {
//             return inPrivateRoom;
//         }
//         public void SetInPrivateRoom(bool value) {
//             inPrivateRoom = value;
//             ShowLeaveInstructions(value);
//         }

//         private void OnTriggerEnter(Collider other) {
//             if (other.CompareTag("HandTag")) {

//                 if (PhotonNetwork.InRoom) {
//                     PhotonNetwork.LeaveRoom();
//                 }
//                 inPrivateRoom = true;
//                 StartCoroutine(WaitForConnection());
//             }
//         }
//         private IEnumerator WaitForConnection() {
//             yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);

//             // Debug.Log("Joining the private room");  
//             // Debug.Log(PhotonNetwork.NetworkClientState);
//             //this if statement is here to prevent joinroom errors caused by player spamming the enter button
//             if (PhotonNetwork.NetworkClientState != ClientState.Joining && //makes sure player is Joined bef
//                 PhotonNetwork.NetworkClientState == ClientState.ConnectedToMaster) //on the off chace that in this brief period of time 
//             {
//                 PhotonVRManager.JoinPrivateRoom(JoinPrivateRoomNameDisplay.GetRoomName());
//                 LeaveWarningToShow.gameObject.SetActive(true);
//             }
            
//         }
        
//         private static bool IsAlphabetic(string input) {
//             foreach (char c in input) {
//                 if (!char.IsLetter(c)) {
//                     return false;
//                 }
//             }
//             return true;
//         }
    
//     }

// }

