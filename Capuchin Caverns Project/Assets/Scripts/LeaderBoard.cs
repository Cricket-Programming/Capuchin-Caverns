using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LeaderBoard : MonoBehaviour
{
    //not currently accessed anywhere but it can be!
    public string[] usernames;

    private TMP_Text peopleDisplay;
    // [SerializeField] private string activereportperson;
    private void Start() {
        peopleDisplay = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        if (PhotonNetwork.IsConnected) {
            usernames = new string[PhotonNetwork.PlayerList.Length]; //creates new string of a certain length #In summary, use PhotonNetwork.CurrentRoom.PlayerCount when you only need the number of players in the room, and use PhotonNetwork.PlayerList.Length when you need additional information about the players in the room.
            //Debug.Log(usernames);
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                usernames[i] = PhotonNetwork.PlayerList[i].NickName;
                peopleDisplay.text = string.Join("\n", usernames);
            }
        } else {
            peopleDisplay.text = "Not Connected";
        }
    }
}   

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using Photon.Pun;
// using TMPro;
// using Photon.Realtime;

// public class LeaderBoard : MonoBehaviour
// {
//     [SerializeField] public string[] usernames;
//     [SerializeField] public TMP_Text displaySpot;
//     [SerializeField] public string activereportperson;

//     private void Update()
//     {
//         if (PhotonNetwork.IsConnected) {
//             usernames = new string[PhotonNetwork.PlayerList.Length]; //creates new string of a certain length #In summary, use PhotonNetwork.CurrentRoom.PlayerCount when you only need the number of players in the room, and use PhotonNetwork.PlayerList.Length when you need additional information about the players in the room.
//             //Debug.Log(usernames);
//             for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
//             {
//                 usernames[i] = PhotonNetwork.PlayerList[i].NickName;
//                 displaySpot.text = string.Join("\n", usernames);
//             }
//         } else {
//             displaySpot.text = "Not Connected";
//         }
//     }
// }   