using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.Pun;
//this script supposedly makes cosmetics not show up in the cameras view I don't think it works
public class EyeBlockCull : MonoBehaviour
{
    GameObject PlayerPrefab;
   
    void Update()
    {
        PlayerPrefab = this.gameObject.transform.root.gameObject;
        if (PlayerPrefab.GetComponent<PhotonView>().IsMine) 
        {
            this.gameObject.layer = 9;
        }
    }
}
