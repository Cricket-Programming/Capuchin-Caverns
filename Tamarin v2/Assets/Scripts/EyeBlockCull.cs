using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.Pun;

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
