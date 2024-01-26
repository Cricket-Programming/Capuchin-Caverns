using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.VR;
using Photon.VR.Player;

// This script works with ChangeSkin to provide skin changing functionality. Its job is to network the skin based on input from ChangeSkin.
public class NetworkSkin : MonoBehaviourPunCallbacks
{
    private List<MeshRenderer> ColourObjects = new List<MeshRenderer>();
    private GameObject[] players;
    [SerializeField] private Material[] skins;

    private void Start() {
        ColourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
        Invoke("newPlayerSkinCatchUp", 0.1f); 
    }

 
    private void newPlayerSkinCatchUp() {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            Material playerMaterial = player.GetComponent<PhotonVRPlayer>().ColourObjects[0].material;
            if (playerMaterial.mainTexture != null) {
                for (int i = 0; i < skins.Length; i++) {
                    if (skins[i].mainTexture.name.Equals(playerMaterial.mainTexture.name)) {   // + " (Instance)" {    
                        player.GetComponent<NetworkSkin>().photonView.RPC("SetSkin", photonView.Owner, i);
                    }
                }
            }
        }
    }
    
    // GetSkinIndex(), RunSetNetworkSkin, and RunRemoveNetwork skin are called in the ChangeSkin class.
    public int GetSkinIndex(Material skin) {
        for (int index = 0; index < skins.Length; index++){
            if (skins[index].mainTexture.name.Equals(skin.mainTexture.name)) {   // + " (Instance)" {    
                return index;
            }
        }
        return -1;     
    }
    public void RunSetNetworkSkin(int index) {
        photonView.RPC("SetSkin", RpcTarget.All, index);
    }
    public void RunRemoveNetworkSkin() {
        photonView.RPC("RemoveSkin", RpcTarget.All);
    }
    [PunRPC]
    private void SetSkin(int index) {
        foreach (Renderer colourObject in ColourObjects) {
            colourObject.material.mainTexture = skins[index].mainTexture;
            colourObject.material.color = skins[index].color; //usually skin.color is white
            
        }
    }
    [PunRPC]
    private void RemoveSkin() {
        foreach (Renderer colourObject in ColourObjects) {
            colourObject.material.mainTexture = null;
            colourObject.material.color = PhotonVRManager.Manager.Colour;
        }   
    }
}
