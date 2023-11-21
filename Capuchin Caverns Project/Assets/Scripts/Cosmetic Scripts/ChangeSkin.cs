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
    [SerializeField] private Material skin;
    private PhotonVRPlayer myPlayer;

    private Material previousMaterial;
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("HandTag")) return;

        myPlayer = PhotonVRManager.Manager.LocalPlayer;

        //photonView.RPC("setSkin", RpcTarget.AllBuffered);
        // if (skin == null) { //remove skin
        //     foreach (Renderer colourObject in myPlayer.ColourObjects) {
        //         colourObject.material.mainTexture = null;
        //         colourObject.material.color = PhotonVRManager.Manager.Colour; //usually skin.color is white
        //     }        
        // }  
        // else {
        //     foreach (Renderer colourObject in myPlayer.ColourObjects) {
        //         colourObject.material.mainTexture = skin.mainTexture;
        //         colourObject.material.color = skin.color; //usually skin.color is white
                
        //     }
        // }  
        if (skin == null) { //remove skin
            myPlayer.GetComponent<NetworkSkin>().RunRemoveNetworkSkin();
        }  
        else {
            myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin();
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
