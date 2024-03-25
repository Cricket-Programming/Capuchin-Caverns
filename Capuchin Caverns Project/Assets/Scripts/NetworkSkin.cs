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
    [Tooltip("All skins must have mainTextures")]
    public Material[] skins; // Accessed for initialMaterialIndex in TagScript

    [Tooltip("This material is used to reset material properties when removing the skin. This material has 'default' material properties such as dark brown material. The player will still keep its original color when removing the skin even with this.")]
    [SerializeField] private Material materialWithDefaultProperties;

    private void Start() {
        ColourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
        Invoke("NewPlayerSkinLoadAndCatchUp", 0.1f); 
    }

    private void NewPlayerSkinLoadAndCatchUp() {
        // Skin Index key exists if the player has a skin. SkinIndex equals -1 if the player has no skin on, otherwise it will be the index of the skin to put on.
        if (PlayerPrefs.HasKey("SkinIndex") && PlayerPrefs.GetInt("SkinIndex") != -1) {     
            PhotonVRPlayer myPlayer = PhotonVRManager.Manager.LocalPlayer;
            
            myPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(PlayerPrefs.GetInt("SkinIndex"));        
        }  

        // This delay allows the SetNetworkSkin stuff above to have time to execute.      
        Invoke("NewPlayerSkinCatchUp", 0.1f);
    }
    private void NewPlayerSkinCatchUp() {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            Material playerMaterial = player.GetComponent<PhotonVRPlayer>().ColourObjects[0].material;
            // Player has a skin
            if (playerMaterial.mainTexture != null) {
                // Linear search the skins in the array to find out which one the current player has. 
                for (int i = 0; i < skins.Length; i++) {
                    // Check if the current skin is the player's skin.
                    if (skins[i].mainTexture.name.Equals(playerMaterial.mainTexture.name)) {
                        player.GetComponent<NetworkSkin>().photonView.RPC("SetSkin", photonView.Owner, i);
                    }
                }
            }
        }
    }
    
    // GetSkinIndex(), RunSetNetworkSkin, and RunRemoveNetworkSkin are called in the ChangeSkin class.
    // The skin is serialized as a number, which is reconstructed on the receiving end (networkSkin classes). We do this because skins can't travel through RPC calls.
    public int GetSkinIndex(Material skin) {
        if (skin != null) { // Ignores the case where the skin has been not assigned on purpose because the ChangeSkin script is for a disable button.
            for (int index = 0; index < skins.Length; index++){
                if (skins[index].mainTexture.name.Equals(skin.mainTexture.name)) {    
                    return index;
                }
            }
            Debug.LogError("Skin not found in array of skins of NetworkSkin script. Make sure that ChangeSkin and NetworkSkin scripts both have the skin material. Skin mainTexture name: " + skin.mainTexture.name);
        }

        return -1;     
    }
    public void RunSetNetworkSkin(Material PlayerMaterial) {
        int materialIndex = GetSkinIndex(PlayerMaterial);
        _RunSetNetworkSkin(materialIndex);        
    }
    public void RunSetNetworkSkin(int index) {
        _RunSetNetworkSkin(index);
    }
    private void _RunSetNetworkSkin(int index) {
        PlayerPrefs.SetInt("SkinIndex", index);
        photonView.RPC("SetSkin", RpcTarget.All, index);
        PhotonVRManager.Manager.LocalPlayer.GetComponent<TagScript6>().initialMaterial = new Material(PhotonVRManager.Manager.LocalPlayer.ColourObjects[0].material); // This creates a copy, not a reference.
    }
    public void RunRemoveNetworkSkin() {
        PlayerPrefs.SetInt("SkinIndex", -1);
        photonView.RPC("RemoveSkin", RpcTarget.All);
        PhotonVRManager.SetColour(PhotonVRManager.Manager.Colour); // This will also refresh the player values. 
        PhotonVRManager.Manager.LocalPlayer.GetComponent<TagScript6>().initialMaterial = new Material(PhotonVRManager.Manager.LocalPlayer.ColourObjects[0].material); // This creates a copy, not a reference.
    }


    
    [PunRPC]
    private void SetSkin(int index) {
        foreach (Renderer colourObject in ColourObjects) {
            colourObject.material = skins[index];
        }
    }
    [PunRPC]
    private void RemoveSkin() {
        foreach (Renderer colourObject in ColourObjects) {
            colourObject.material = materialWithDefaultProperties;    
        }
    }
}
