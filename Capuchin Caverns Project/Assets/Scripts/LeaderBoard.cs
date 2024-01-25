using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.VR.Player;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    // Not currently accessed anywhere, but it can be!
    [HideInInspector] public static string[] usernames;

    [SerializeField] private Transform colorSpotsParent;
    private Renderer[] colorSpots;
    private TMP_Text peopleDisplay;

    private void Start() {
        peopleDisplay = GetComponent<TMP_Text>(); 

        // Get all of the color Spots in colorSpotsParent.
        int childCount = colorSpotsParent.childCount;
        colorSpots = new Renderer[childCount]; 
        // Iterate through each child.
        for (int i = 0; i < childCount; i++)
        {
            // Get the i-th child using GetChild(i)
            Transform child = colorSpotsParent.GetChild(i);
            colorSpots[i] = child.GetComponent<Renderer>();
        }

        StartCoroutine(UpdateLeaderboardCoroutine(2f));
    }
    private IEnumerator UpdateLeaderboardCoroutine(float interval) {
        while (true) {
            if (PhotonNetwork.InRoom) {
                UpdateLeaderboardData();
            } else {
                peopleDisplay.text = "Not Connected";
            }
            yield return new WaitForSeconds(interval);
        }

    }

    // Refreshes usernames and player colorSpots on the board.
    // I'm not sure, but I think this should not be static because it is editing instance specific variables.
    private void UpdateLeaderboardData() {        
        PhotonVRPlayer[] allPhotonVRPlayerScripts = FindObjectsOfType<PhotonVRPlayer>();  
        // Use PhotonNetwork.CurrentRoom.PlayerCount when you only need the number of players in the room. Use PhotonNetwork.PlayerList.Length when you need additional information about the players in the room.
        int PhotonNetworkPlayerListLength = PhotonNetwork.PlayerList.Length;

        usernames = new string[PhotonNetworkPlayerListLength];
        
        //assign the colorSpots and player names
        for (int i = 0; i < PhotonNetworkPlayerListLength; i++)
        {
            var currentPlayer = PhotonNetwork.PlayerList[i];

            usernames[i] = currentPlayer.NickName;

            foreach (PhotonVRPlayer PVRP in allPhotonVRPlayerScripts)
            {   
                if (PVRP.gameObject.GetComponent<PhotonView>().Owner == currentPlayer)
                {
                    colorSpots[i].material.mainTexture = PVRP.ColourObjects[0].material.mainTexture;
                    colorSpots[i].material.color = PVRP.ColourObjects[0].material.color;
                }
            }

        } 
        peopleDisplay.text = string.Join("\n", usernames);
        // If the colorSpot has no player, then set the colorSpot back to white.
        resetUnusedColorSpots(PhotonNetworkPlayerListLength);
        
    }
    private void resetUnusedColorSpots(int j) {
        for (; j < colorSpotsParent.childCount; j++) {
            colorSpots[j].material.mainTexture = null;
            colorSpots[j].material.color = Color.white;
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
//     // Not currently accessed anywhere but it can be!
//     private string[] usernames;

//     private TMP_Text peopleDisplay;
//     // [SerializeField] private string activereportperson;
//     private void Start() {
//         peopleDisplay = GetComponent<TMP_Text>();
//     }
//     private void Update()
//     {
//         if (PhotonNetwork.IsConnected) {
//             usernames = new string[PhotonNetwork.PlayerList.Length]; //creates new string of a certain length #In summary, use PhotonNetwork.CurrentRoom.PlayerCount when you only need the number of players in the room, and use PhotonNetwork.PlayerList.Length when you need additional information about the players in the room.
//             //Debug.Log(usernames);
//             for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
//             {
//                 usernames[i] = PhotonNetwork.PlayerList[i].NickName;
//                 peopleDisplay.text = string.Join("\n", usernames);
//             }
//         } else {
//             peopleDisplay.text = "Not Connected";
//         }
//     }
// }   
