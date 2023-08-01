using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// This script makes the body colliders and player name not show from the player's eyes. However, all of the other players will be able to see the player's name and collide with the body colliders.
public class BodyColliders : MonoBehaviour
{ 
    public PhotonView PhotonView;

    private void Update()
    {
        if (PhotonView.IsMine){
            this.gameObject.SetActive(false);
        }
    }
}
