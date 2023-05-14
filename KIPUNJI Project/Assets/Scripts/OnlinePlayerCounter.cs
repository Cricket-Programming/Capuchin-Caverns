using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class OnlinePlayerCounter : MonoBehaviourPunCallbacks
{
    private TMP_Text playerCountText;
    private int playerCount;

    void Start()
    {
        playerCountText = GetComponent<TMP_Text>();
        UpdatePlayerCount();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount();
    }

    private void UpdatePlayerCount()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        playerCountText.text = playerCount.ToString();
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//This scripts counts how many players total players there are online.
public class OnlinePlayerCounter : MonoBehaviourPunCallbacks
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
            Debug.Log(playerCount);
            playerCountText.text = playerCount.ToString();
        }
    }
}
*/