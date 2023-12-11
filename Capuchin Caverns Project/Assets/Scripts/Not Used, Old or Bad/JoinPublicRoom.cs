using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
using Photon.Pun;
public class JoinPublicRoom : MonoBehaviourPunCallbacks
{
    //the player connects automatically.
    //then, this joins the player to a room
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonVRManager.Manager.DefaultQueue + "hello bru");
        PhotonVRManager.JoinRandomRoom(PhotonVRManager.Manager.DefaultQueue, 20);
    }

    void Update()
    {
        
    }
}
