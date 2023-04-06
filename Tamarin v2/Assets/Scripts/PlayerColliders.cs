using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerColliders : MonoBehaviour
{ 
    public PhotonView PhotonView;
    //public GameObject Disable;

    void Update()
    {
        if(PhotonView.IsMine){
            this.gameObject.SetActive(false);
        }
    }
}
