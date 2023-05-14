using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class OnlinePlayerCounter : MonoBehaviour
{
    public TMP_Text playerCountText;

    private void Start()
    {
        playerCountText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            int playerCount = PhotonNetwork.CountOfPlayers;
            playerCountText.text = "Online Players: " + playerCount.ToString();
        }
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