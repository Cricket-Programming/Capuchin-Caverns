using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
public class TagEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HandTag")) {
            PhotonVRManager.Manager.LocalPlayer.GetComponent<TagScript6>().ChangeIsInTagArea(true);
        }
    }
    // private bool isInTagArea = false;

    // //getter
    // public bool IsInTagArea() {
    //     return isInTagArea;
    // } 
    // //setter
    // public void ChangeIsInTagArea(bool boolean) {
    //     isInTagArea = boolean;
    // }
    // private void OnTriggerEnter() {
    //     isInTagArea = true;  
    // }
}
