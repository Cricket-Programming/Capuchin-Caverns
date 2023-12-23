using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
using Photon.VR.Cosmetics; //this namespace is the location of the CosmeticType public enum.

public class ChangeCosmetic : MonoBehaviour
{
    //accessing CosmeticType public enum in Photon.VR.Cosmetics namespace (class PhotonVRCosmeticsData).
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


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.VR;
// using Photon.VR.Cosmetics;

// public class ChangeCosmetic : MonoBehaviour
// {
//     private enum cosmeticTypes { CosmeticType.Head, CosmeticType.Left, CosmeticType.Right }
//     [SerializeField] private cosmeticTypes cosmeticType;

//     [Tooltip("The name of the gameobject of the cosmetic in PhotonVR player prefab")]
//     public string CosmeticName;



//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("HandTag"))
//         {
//             PhotonVRManager.SetCosmetic((CosmeticType)(int)cosmeticType, CosmeticName);
//         }
//     }
// }