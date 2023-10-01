using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.VR.Cosmetics;
public class ChangeLeftCosmetic : MonoBehaviour
{
    public string Left;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag"))
        {
            PhotonVRManager.SetCosmetic(CosmeticType.LeftHand, Left);
        }
    }
}
