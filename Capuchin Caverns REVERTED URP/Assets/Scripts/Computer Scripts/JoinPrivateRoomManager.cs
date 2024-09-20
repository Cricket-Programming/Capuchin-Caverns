using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
using Photon.Pun;
using Photon.Realtime; // for Clientstate.Joining
using TMPro;

// Bug: If player disconnects because they put their headset down, they can't join private rooms.
namespace JoinPrivateRoomScript {
    public class JoinPrivateRoomManager : MonoBehaviourPunCallbacks, IAddLetterable
    {
        [Header("This script can go on any gameobject that remains active for the entire game")]
        [SerializeField] private TMP_Text JoinPrivateRoomNameDisplay;
        [SerializeField] private TMP_Text LeaveWarningToShow;
        
        // C# property, shorthand syntax
        public static JoinPrivateRoomManager Manager { get; private set; } //the point of this is so that other classes can access this instance of the class without directly finding it. Ideally, in this case, it would be better to have static methods, but leaving it like this for reference purposes.
        private bool inPrivateRoom = false;
        private void Awake() {
            if (Manager == null)
                Manager = this;
            ToggleLeaveInstructions(false);
        }
        //leave instructions is active when in private room, inactive when not in private room
        private void ToggleLeaveInstructions(bool value) {
            LeaveWarningToShow.gameObject.SetActive(value);
        }
        public bool GetInPrivateRoom() => inPrivateRoom;
        public void SetInPrivateRoom(bool value) {
            inPrivateRoom = value;
            ToggleLeaveInstructions(value);
            
        }

        public void JoinPrivateRoomLogic() {
            if (GetRoomName().Equals("")) return;

            SetInPrivateRoom(true);
            if (PhotonNetwork.InRoom) {
                PhotonNetwork.LeaveRoom(); // You are still connected even if you leave the room
            } else {
                PhotonVRManager.Connect();
            }

            StartCoroutine(WaitForConnection());
        }

        private IEnumerator WaitForConnection() {
            yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);
            
            // Debug.Log("Joining the private room");
            // Debug.Log(PhotonNetwork.NetworkClientState);
            //this if statement is here to prevent joinroom errors caused by player spamming the enter button(
            if (PhotonNetwork.NetworkClientState != ClientState.Joining)//makes sure player is Joined before. //PhotonNetwork.NetworkClientState == ClientState.ConnectedToMaster || !PhotonNetwork.IsConnected
            {
                PhotonVRManager.JoinPrivateRoom(GetRoomName());
                // LeaveWarningToShow.gameObject.SetActive(true);
                
            }      
        }
        private string GetRoomName() => JoinPrivateRoomNameDisplay.text;
        private void SetRoomName(string txt) {
            JoinPrivateRoomNameDisplay.text = txt;
        }
        //Invoked by AddLetter() script through IAddLetterable interface. 
        public void AddLetter(string letter) {
            if (GetRoomName().Equals("_"))
                Backspace();
            if (GetRoomName().Length < 12) 
                SetRoomName(GetRoomName() + letter);
        }

        public void Backspace() {
            int nameLength = GetRoomName().Length;
            if (nameLength > 0) 
                SetRoomName(GetRoomName().Remove(nameLength - 1));
        }
    }
}
