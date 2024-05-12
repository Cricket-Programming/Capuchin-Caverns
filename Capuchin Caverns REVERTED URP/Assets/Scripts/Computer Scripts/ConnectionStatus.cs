using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Photon.Pun;
using JoinPrivateRoomScript;

//this script sets a tmp text to say whether the player is connected to a photon room or not connected to a photon room

//PhotonNetwork.IsConnected says the player is connected. 
//PhotonNetwork.InRoom says the player is in a room
//They are two different things.
public class ConnectionStatus : MonoBehaviour   
{
    //you can also use TMP_Text it works the same.
    private TextMeshPro textObject;
    void Start()
    {
        textObject = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            // Player is not connected to a Photon server
            textObject.text = "Not Connected to Room";
            textObject.color = Color.red;
            return;
        }
        if (JoinPrivateRoomManager.Manager.GetInPrivateRoom())  {//JoinPrivateRoomManager is a class in the namespace JoinPrivateRoomScript
            textObject.text = "Connected to Private Room: " + PhotonNetwork.CurrentRoom.Name;/// + ;
            textObject.color = Color.green;
        } else {
            textObject.text = "Connected to Public Room: " + PhotonNetwork.CurrentRoom.Name;/// + ;
            textObject.color = Color.green;
        }
    }
    /*
    //may need to change monobehaviour to monobehaviourpuncallbacks for this to work.
    public override void OnJoinedRoom() { //changing a parent class method OnJoinedRoom() which was empty with this code 
        textObject.text = "In a room";
    }
    
    public override void OnLeftRoom() {
        textObject.text = "Not in a room";  
        Debug.Log("qwerty");
    }
    */

}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using Photon.Pun;

// //this script sets a tmp text to say whether the player is connected to a photon room or not connected to a photon room
// public class ConnectionStatus : MonoBehaviour   
// {
//     //you can also use TMP_Text it works the same.
//     private TextMeshPro textObject;
//     void Start()
//     {
//         textObject = GetComponent<TextMeshPro>();
//     }

//     void Update()
//     {
//         //if (PhotonNetwork.CurrentRoom.Name != null) { Debug.Log(PhotonNetwork.CurrentRoom.Name); }
        
//         if (PhotonNetwork.CurrentRoom != null)
//         {
//             // Player is connected to a Photon server
//             textObject.text = "Connected to Room: " + PhotonNetwork.CurrentRoom.Name;/// + ;
//             textObject.color = Color.green;
//         }
//         else
//         {
//             // Player is not connected to a Photon server
//             textObject.text = "Not Connected to Room";
//             textObject.color = Color.red;
//         }
//     }
//     /*
//     //may need to change monobehaviour to monobehaviourpuncallbacks for this to work.
//     public override void OnJoinedRoom() { //changing a parent class method OnJoinedRoom() which was empty with this code 
//         textObject.text = "In a room";
//     }
    
//     public override void OnLeftRoom() {
//         textObject.text = "Not in a room";  
//         Debug.Log("qwerty");
//     }
//     */

// }
