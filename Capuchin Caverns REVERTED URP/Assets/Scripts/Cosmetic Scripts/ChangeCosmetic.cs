using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR; // this namespace has the PhotonVRManager class.
using Photon.VR.Cosmetics; // This namespace is the location of the CosmeticType public enum.

public class ChangeCosmetic : MonoBehaviour
{
    // Accessing the CosmeticType public enum in Photon.VR.Cosmetics namespace (class PhotonVRCosmeticsData).
    [SerializeField] private CosmeticType cosmeticType;

    [Tooltip("The name of the gameobject of the cosmetic in PhotonVR player prefab")]
    [SerializeField] private string cosmeticName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag"))
        {
            PhotonVRManager.SetCosmetic(cosmeticType, cosmeticName);
        }
    }
}