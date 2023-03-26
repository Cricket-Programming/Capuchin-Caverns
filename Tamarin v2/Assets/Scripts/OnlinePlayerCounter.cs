using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class OnlinePlayerCounter : MonoBehaviour
{
    //credits flimcy and pearvr
    private TMP_Text playerCountText;
    int playerCount;

    void Start()
    {
        playerCountText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            playerCount = PhotonNetwork.CountOfPlayers;
            playerCountText.text = playerCount.ToString();
        }
    }
}
