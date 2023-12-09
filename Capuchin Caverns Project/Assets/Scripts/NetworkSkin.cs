using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.VR;
using Photon.VR.Player;
public class NetworkSkin : MonoBehaviourPunCallbacks
{
    private List<MeshRenderer> ColourObjects = new List<MeshRenderer>();
    private GameObject[] players;
    [SerializeField] private Material skin;
    private void Start() {
        ColourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
       
        Invoke("newPlayerSkinCatchUp", 0.1f); 
    }

    private void newPlayerSkinCatchUp() {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            Material playerMaterial = player.GetComponent<PhotonVRPlayer>().ColourObjects[0].material;
            if (playerMaterial.mainTexture != null) {
                if (skin.mainTexture.name.Equals(playerMaterial.mainTexture.name)) {   // + " (Instance)" {    
                    player.GetComponent<NetworkSkin>().photonView.RPC("SetSkin", photonView.Owner);
                }
            }

        }
    }
    // private void Update() {
    //     Debug.Log(GetComponent<PhotonVRPlayer>().ColourObjects[0].material.mainTexture.name);
    //     Debug.Log(skin.mainTexture.name);
    // }
    public void RunSetNetworkSkin() {
        photonView.RPC("SetSkin", RpcTarget.All);
    }
    public void RunRemoveNetworkSkin() {
        photonView.RPC("RemoveSkin", RpcTarget.All);
    }
    [PunRPC]
    private void SetSkin() {
        foreach (Renderer colourObject in ColourObjects) {
            colourObject.material.mainTexture = skin.mainTexture;
            colourObject.material.color = skin.color; //usually skin.color is white
            
        }
    }
    [PunRPC]
    private void RemoveSkin() {
        foreach (Renderer colourObject in ColourObjects) {
            colourObject.material.mainTexture = null;
            colourObject.material.color = PhotonVRManager.Manager.Colour; //usually skin.color is white
        }   
    }
}
