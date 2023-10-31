using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// This script changes the tag to untagged if PhotonView.IsMine. You should set the tag of the gameobject in the inspector like VRHand or something.
public class TagIfOtherVRHand : MonoBehaviour
{ 
    public PhotonView PhotonView;

    private void Update()
    {
        if (PhotonView.IsMine) {
            this.gameObject.tag = "Untagged";
        }
    }
}
