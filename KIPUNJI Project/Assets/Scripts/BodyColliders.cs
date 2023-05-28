using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BodyColliders : MonoBehaviour
{ 
    public PhotonView PhotonView;

    void Update()
    {
        if(PhotonView.IsMine){
            this.gameObject.SetActive(false);
        }
    }
}
