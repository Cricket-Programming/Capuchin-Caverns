using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
using Photon.Pun;
using Photon.Realtime; //for Clientstate.Joining
using TMPro;

//goes on the enter button that the player pushes when they are ready to join the private room
namespace JoinPrivateRoomScript {
    public class JoinPrivateRoomManager : MonoBehaviourPunCallbacks
    {
        [Header("This script can go on any gameobject that remains active for the entire game")]
        [SerializeField] private JoinPrivateRoomNameDisplay JoinPrivateRoomNameDisplay;
        [SerializeField] private TMP_Text LeaveWarningToShow;
        //C# property, shorthand syntax
        public static JoinPrivateRoomManager Manager { get; private set; }
        private bool inPrivateRoom = false;
        private void Awake() {
            if (Manager == null)
                Manager = this;
            ToggleLeaveInstructions(false);
        }
        //leave instructions is active when in private room, inactive when not in private room
        public void ToggleLeaveInstructions(bool value) {
            LeaveWarningToShow.gameObject.SetActive(value);
        }
        public bool GetInPrivateRoom() {
            return inPrivateRoom;
        }
        public void SetInPrivateRoom(bool value) {
            inPrivateRoom = value;
            ToggleLeaveInstructions(value);
        }

        public void JoinPrivateRoomLogic() {
            if (PhotonNetwork.InRoom) {
                PhotonNetwork.LeaveRoom();
            } else {
                PhotonVRManager.Connect();
            }
            inPrivateRoom = true;
            StartCoroutine(WaitForConnection());
        }

        
        private IEnumerator WaitForConnection() {
            yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);

            // Debug.Log("Joining the private room");  
            // Debug.Log(PhotonNetwork.NetworkClientState);
            //this if statement is here to prevent joinroom errors caused by player spamming the enter button
            if (PhotonNetwork.NetworkClientState != ClientState.Joining)//makes sure player is Joined before.
            {
                PhotonVRManager.JoinPrivateRoom(JoinPrivateRoomNameDisplay.GetRoomName());
                LeaveWarningToShow.gameObject.SetActive(true);
            }
            
        }
        

    }

}
