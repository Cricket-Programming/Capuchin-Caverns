using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.VR.Player;
//each skin and cosmetic MUST have an original name
//this script changes the material of the player. 
public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private Material skin;
    private GameObject myPlayer;
    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {


        foreach (Renderer colourObject in myPlayer.GetComponent<PhotonVRPlayer>().ColourObjects) {
            colourObject.material = skin;
            colourObject.material.color = skin.color; //usually skin.color is white
        }
    }


}
