using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

//this script syncs the gameobject with the player's controller hands
//THIS WILL SYNC WITH THE RIGHT HAND ONLY
//
public class SyncFlyingBall : MonoBehaviour
{
    private Vector3 offset = new Vector3(0.019f,-0.016f,-0.061f);
    private void Update() {
        Vector3 rightHandPos = PhotonVRManager.Manager.RightHand.transform.position;
        //transform.position = rightHandPos + offset;

        transform.position = PhotonVRManager.Manager.RightHand.transform.position;
        transform.rotation = PhotonVRManager.Manager.RightHand.transform.rotation;
    }   
}
