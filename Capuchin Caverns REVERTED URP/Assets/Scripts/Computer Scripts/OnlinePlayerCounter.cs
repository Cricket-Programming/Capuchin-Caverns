using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

// This script will count the total number of players in the server, including all of the rooms, but not other servers.
// Bug: Does not update, only updates when player joins the room.
public class OnlinePlayerCounter : MonoBehaviour
{
    private TMP_Text playerCountText;

    private void Start()
    {
        playerCountText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            int playerCount = PhotonNetwork.CountOfPlayers;
            playerCountText.text = playerCount.ToString();
        }
    }
}
