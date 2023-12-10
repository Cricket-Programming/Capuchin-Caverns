using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.VR.Player; //for the type PhotonVRPlayer
using Photon.VR; //for the type PhotonVRManager

//each skin and cosmetic MUST have an original name
//this script changes the material of the player. 
public class ChangeSkin : MonoBehaviourPunCallbacks
{
    [Tooltip("Set as null if you want to remove the skin.")]
    [Header("Remember to put this material in the skins array of networkskin on the player prefab for this to work!")]
    [SerializeField] private Material skin;
    private int skinIndex;
    private PhotonVRPlayer myPlayer;

    private Material previousMaterial;
    private void Start() {
    
        //get the skins index
    }
    //when player joins the server
    public override void OnConnectedToMaster()
    {
        Invoke("SetSkinIndex", 1f); //delay allow PhotonVRManager.Manager.LocalPlayer to not be null
    } 
    private void SetSkinIndex() {
        if (skin != null) { //ignores the case where it has been not assigned on purpose because the button is a disable button 
            myPlayer = PhotonVRManager.Manager.LocalPlayer;
            skinIndex = myPlayer.GetComponent<NetworkSkin>().GetSkinIndex(skin);

            if (skinIndex == -1) {
                Debug.LogError("Skin not found in array of skins of networkskin script. Make sure that apply skin and network skin both have the skin material.");
            }
        }

    }  
    private void Update() {

        
        
    }
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("HandTag")) return;

        myPlayer = PhotonVRManager.Manager.LocalPlayer;

        if (skin == null) { //remove skin
            myPlayer.GetComponent<NetworkSkin>().RunRemoveNetworkSkin();
        }  
        else {
            myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(skinIndex);
        }  

        myPlayer.GetComponent<TagScript6>().initialMaterial = myPlayer.ColourObjects[0].material;

    }

}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using Photon.Pun;
// using Photon.VR.Player;
// using Photon.VR; //for the type PhotonVRManager
// //each skin and cosmetic MUST have an original name
// //this script changes the material of the player. 
// public class ChangeSkin : MonoBehaviourPunCallbacks
// {
//     [Tooltip("Set as null if you want to remove the skin.")]
//     [SerializeField] private Material skin;
//     public PhotonVRManager myPhotonVRManager;
//     private GameObject myPlayer;
    
//     private Material previousMaterial;
//     //public override void OnJoinedRoom()
//     public void Update()
//     {
//         // GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
//         // foreach (GameObject player in players)
//         // {
//         //     if (player.GetComponent<PhotonView>().IsMine)
//         //     {
//         //         myPlayer = player;
//         //     }
//         // }
//         myPlayer = PhotonVRManager.Manager.LocalPlayer;
//     }
//     private void OnTriggerEnter(Collider other) {
//         if (!other.CompareTag("HandTag")) return;

//         if (skin == null) { //remove skin
//             foreach (Renderer colourObject in myPlayer.GetComponent<PhotonVRPlayer>().ColourObjects) {
//                 colourObject.material.mainTexture = null;
//                 colourObject.material.color = myPhotonVRManager.Colour; //usually skin.color is white
//             }        
//         }  
//         else {
//             foreach (Renderer colourObject in myPlayer.GetComponent<PhotonVRPlayer>().ColourObjects) {
//                 colourObject.material.mainTexture = skin.mainTexture;
//                 colourObject.material.color = skin.color; //usually skin.color is white
                
//             }
//         }  
//         myPlayer.GetComponent<TagScript6>().initialMaterial = myPlayer.GetComponent<PhotonVRPlayer>().ColourObjects[0].material;

//     }
    

// }
