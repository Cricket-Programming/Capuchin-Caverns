using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

// When the player passes through this object, it will let TagScript know that the player is not in the tag area anymore.
public class TagExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
            PhotonVRManager.Manager.LocalPlayer.GetComponent<TagScript6>().ChangeIsInTagArea(false);
    }
}
