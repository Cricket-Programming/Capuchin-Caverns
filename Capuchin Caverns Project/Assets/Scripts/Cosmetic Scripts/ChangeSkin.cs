using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.VR.Player; // For the object type PhotonVRPlayer
using Photon.VR; // For the object type PhotonVRManager

// This script works with NetworkSkin to provide skin (material) changing functionality. Its job is to communicate to NetworkSkin what skin to network. 

public class ChangeSkin : MonoBehaviour
{
    [Header("Remember to put this material in the skins array of NetworkSkin on the player prefab for this to work!")]
    [Tooltip("Set as null if you want to remove the skin. Each skin (and cosmetic) MUST have an original name")]
    [SerializeField] private Material skin;
    private int skinIndex = -1;
    private PhotonVRPlayer myPlayer;
    private Material previousMaterial;

    private void SetSkinIndex() {
        if (skin != null) { //ignores the case where the skin has been not assigned on purpose because the ChangeSkin script is for a disable button.
            myPlayer = PhotonVRManager.Manager.LocalPlayer;
            skinIndex = myPlayer.GetComponent<NetworkSkin>().GetSkinIndex(skin);

            if (skinIndex == -1) {
                Debug.LogError("Skin not found in array of skins of NetworkSkin script. Make sure that ChangeSkin and NetworkSkin scripts both have the skin material.");
            }
        }
    }  

    private void OnTriggerEnter(Collider other) {
        if (!PhotonNetwork.IsConnected) return;
        if (!other.CompareTag("HandTag")) return;

        myPlayer = PhotonVRManager.Manager.LocalPlayer;

        if (skin == null) { // Remove skin
            myPlayer.GetComponent<NetworkSkin>().RunRemoveNetworkSkin();
        }  
        else {
            if (skinIndex == -1) SetSkinIndex(); // skinIndex has not been set yet, so set it.
            myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(skinIndex);
        }  
        
        myPlayer.GetComponent<TagScript6>().initialMaterial = myPlayer.ColourObjects[0].material;
    }
}
