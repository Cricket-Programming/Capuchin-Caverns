using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

// When the player passes through this object, it will let TagScript know that the player is in the tag area.
public class TagEnter : MonoBehaviour
{
    private void OnTriggerEnter() {
        PhotonVRManager.Manager.LocalPlayer.GetComponent<TagScript6>().ChangeIsInTagArea(true);
        Debug.Log("Trigger enter");
    }
}
