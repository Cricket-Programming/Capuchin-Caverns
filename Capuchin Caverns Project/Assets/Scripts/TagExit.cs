using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

public class TagExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("HandTag")) {
            PhotonVRManager.Manager.LocalPlayer.GetComponent<TagScript6>().ChangeIsInTagArea(false);
        }
    }
}
