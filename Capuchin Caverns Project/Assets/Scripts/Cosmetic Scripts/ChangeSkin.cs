using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.VR.Player; //for the type PhotonVRPlayer
using Photon.VR; //for the type PhotonVRManager

// Each skin (and cosmetic) MUST have an original name.
// This script changes the material of the player. 
public class ChangeSkin : MonoBehaviourPunCallbacks
{
    [Tooltip("Set as null if you want to remove the skin.")]
    [Header("Remember to put this material in the skins array of NetworkSkin on the player prefab for this to work!")]
    [SerializeField] private Material skin;
    private int skinIndex = -1;
    private PhotonVRPlayer myPlayer;
    private Material previousMaterial;

    private void SetSkinIndex() {
        if (skin != null) { //ignores the case where the skin has been not assigned on purpose because the ChangeSkin script is for a disable button.
            myPlayer = PhotonVRManager.Manager.LocalPlayer;
            skinIndex = myPlayer.GetComponent<NetworkSkin>().GetSkinIndex(skin);

            if (skinIndex == -1) {
                Debug.LogError("Skin not found in array of skins of networkSkin script. Make sure that ChangeSkin and NetworkSkin scripts both have the skin material.");
            }
        }
    }  

    private void OnTriggerEnter(Collider other) {
        if (!PhotonNetwork.IsConnected) return;
        if (!other.CompareTag("HandTag")) return;

        myPlayer = PhotonVRManager.Manager.LocalPlayer;

        if (skin == null) { // remove skin
            myPlayer.GetComponent<NetworkSkin>().RunRemoveNetworkSkin();
        }  
        else {
            if (skinIndex == -1) { SetSkinIndex(); } //skinIndex has not been set yet, so set it..
            myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(skinIndex);
        }  

        myPlayer.GetComponent<TagScript6>().initialMaterial = myPlayer.ColourObjects[0].material;
    }

}






// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using Photon.Pun;
// using Photon.VR.Player; //for the type PhotonVRPlayer
// using Photon.VR; //for the type PhotonVRManager

// //each skin and cosmetic MUST have an original name
// //this script changes the material of the player. 
// public class ChangeSkin : MonoBehaviourPunCallbacks
// {
//     [Tooltip("Set as null if you want to remove the skin.")]
//     [Header("Remember to put this material in the skins array of networkskin on the player prefab for this to work!")]
//     [SerializeField] private Material skin;
//     private int skinIndex = -1;
//     private PhotonVRPlayer myPlayer;

//     private Material previousMaterial;

//     //when player joins the server
//     // public override void OnConnectedToMaster()
//     // {
//     //     Invoke("SetSkinIndex", 1f); //delay allow PhotonVRManager.Manager.LocalPlayer to not be null.
//     // } 

//     // private void OnEnable() {
        
//     //     if (PhotonNetwork.IsConnected) {
//     //         SetSkinIndex();
//     //     }
//     // }
//     private void SetSkinIndex() {
//         if (skin != null) { //ignores the case where it has been not assigned on purpose because the button is a disable button.
//             myPlayer = PhotonVRManager.Manager.LocalPlayer;
//             skinIndex = myPlayer.GetComponent<NetworkSkin>().GetSkinIndex(skin);

//             if (skinIndex == -1) {
//                 Debug.LogError("Skin not found in array of skins of networkSkin script. Make sure that changeSkin and networkSkin classes both have the skin material.");
//             }
//         }

//     }  
//     private void OnTriggerEnter(Collider other) {
//         if (!PhotonNetwork.IsConnected) return;
//         if (!other.CompareTag("HandTag")) return;

//         myPlayer = PhotonVRManager.Manager.LocalPlayer;

//         if (skin == null) { //remove skin
//             myPlayer.GetComponent<NetworkSkin>().RunRemoveNetworkSkin();
//         }  
//         else {
//             if (skinIndex == -1) { SetSkinIndex(); } //skinIndex is either never set or their is an error.
//             myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(skinIndex);
//         }  

//         myPlayer.GetComponent<TagScript6>().initialMaterial = myPlayer.ColourObjects[0].material;

//     }

// }


