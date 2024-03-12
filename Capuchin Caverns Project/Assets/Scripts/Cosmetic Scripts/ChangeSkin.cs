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
    [Tooltip("Set as null if you want to remove the skin. Each skin (and cosmetic) MUST have an original name.")]
    [SerializeField] private Material skin;
    private int skinIndex = -1; // -1 means skinIndex has not been set yet.
    private PhotonVRPlayer myPlayer;
    private Material previousMaterial;

    private void OnTriggerEnter(Collider other) {
        if (!PhotonNetwork.IsConnected) return;
        if (!other.CompareTag("HandTag")) return;

        myPlayer = PhotonVRManager.Manager.LocalPlayer;

        if (skin == null) { // Remove skin
            myPlayer.GetComponent<NetworkSkin>().RunRemoveNetworkSkin();
        }  
        else {
            // skinIndex has not been set yet, so set it.
            if (skinIndex == -1) {
                skinIndex = myPlayer.GetComponent<NetworkSkin>().GetSkinIndex(skin);    
            }
            myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(skinIndex);

        }  
        
        myPlayer.GetComponent<TagScript6>().initialMaterial = myPlayer.ColourObjects[0].material;
    }
}
