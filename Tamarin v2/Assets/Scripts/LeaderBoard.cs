using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.Networking;
using Photon.Realtime;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] public string[] usernames;
    [SerializeField] public TMP_Text displaySpot;
    [SerializeField] public string activereportperson;


    void Update()
    {
        usernames = new string[PhotonNetwork.PlayerList.Length]; //creates new string of a certain length
        //Debug.Log(usernames);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            usernames[i] = PhotonNetwork.PlayerList[i].NickName;
            displaySpot.text = string.Join("\n", usernames);
        }
    }
}   