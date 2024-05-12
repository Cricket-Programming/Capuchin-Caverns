using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
// This script makes the object it is on not viewable from the player's eyes . However, the other players will still be able to see the object. 
// Example uses are body colliders that other players can collide with, but player can't collide into self or player name visible to other players, but not the player whose name it is.

public class BodyColliders : MonoBehaviour
{ 
    [SerializeField] private PhotonView photonView;

    private void Update()
    {
        if (photonView.IsMine){
            this.gameObject.SetActive(false);
        }
    }
}
