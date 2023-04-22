using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//this script sets a tmp text to say whether the player is connected to a photon room or not connected to a photon room
public class ConnectionStatus : MonoBehaviourPunCallbacks //MonoBehaviorPunCallbacks is a class in the PUN class that extends the MonoBehavior class
{
    //you can also use TMP_Text it works the same.
    private TextMeshPro textObject;
    void Start()
    {
        textObject = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            // Player is connected to a Photon server
            textObject.text = "Connected to a Room";
        }
        else
        {
            // Player is not connected to a Photon server
            textObject.text = "Not Connected to Room";
        }
    }
    /*
    public override void OnJoinedRoom() { //changing a parent class method OnJoinedRoom() which was empty with this code 
        textObject.text = "In a room";
    }
    
    public override void OnLeftRoom() {
        textObject.text = "Not in a room";  
        Debug.Log("qwerty");
    }
    */

}
