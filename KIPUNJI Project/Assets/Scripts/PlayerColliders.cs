using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerColliders : MonoBehaviour
{ 
    public PhotonView PhotonView;

    void Update()
    {
        if(PhotonView.IsMine){
            this.gameObject.SetActive(false);
        }
    }
}
