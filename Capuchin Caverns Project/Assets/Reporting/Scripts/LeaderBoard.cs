// using UnityEngine;
// using UnityEngine.Networking;
// using Photon.Pun;
// using TMPro;
// using Photon.VR;
// using Photon.Voice.PUN;
// using Photon.VR.Player;
// using System.Text;
// using System.Collections;
// using System;
// using System.Collections.Generic;

// [RequireComponent(typeof(PhotonView))]
// public class LeaderBoard : MonoBehaviour
// {
//     [SerializeField] public TMP_Text[] displaySpot;
//     [SerializeField] public Renderer[] ColorSpot;
//     [SerializeField] public string WebHookURL;
//     [SerializeField] public Playfablogin playfablogin;
//     private bool hashed;

//     private bool Kicked = false;

//     private void Start()
//     {
//         if (GetComponent<PhotonView>().OwnershipTransfer != OwnershipOption.Takeover)
//         {
//             GetComponent<PhotonView>().OwnershipTransfer = OwnershipOption.Takeover;
//         }
//     }
//     private void Update()
//     {
//         if (PhotonNetwork.IsConnected && !hashed) 
//         {
//             ExitGames.Client.Photon.Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
//             hash["PlayfabID"] = playfablogin.MyPlayFabID;
//             PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
//             hashed = true;
//         }
//         for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
//         {
//             if (!Kicked)
//             {
//                 displaySpot[i].text = PhotonNetwork.PlayerList[i].NickName;
//                 foreach (PhotonVRPlayer PVRP in FindObjectsOfType<PhotonVRPlayer>())
//                 {
//                     if (PVRP.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.PlayerList[i])
//                     {
//                         ColorSpot[i].material.color = JsonUtility.FromJson<Color>((string)PVRP.gameObject.GetComponent<PhotonView>().Owner.CustomProperties["Colour"]);
//                     }
//                 }
//             }

//             else
//             {
//                 if (PhotonNetwork.IsConnected)
//                 {
//                     PhotonNetwork.Disconnect();
//                 }
//                 displaySpot[i].color = Color.red;
//                 displaySpot[i].text = "You have been Kicked";

//             }
//         }
//         for (int i = 0; i < displaySpot.Length; i++)
//         {
//             if (i > PhotonNetwork.PlayerList.Length)
//             {
//                 displaySpot[i].text = null;
//                 ColorSpot[i].material.color = Color.white;
//             }
//         }
//     }

//     public void MutePress(int ButtonNumber)
//     {
//         if (PhotonNetwork.PlayerList.Length >= ButtonNumber - 1)
//         {
//             foreach (PhotonVRPlayer PVRP in FindObjectsOfType<PhotonVRPlayer>())
//             {
//                 if (PVRP.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.PlayerList[ButtonNumber - 1])
//                 {
//                     AudioSource audioSource = PVRP.gameObject.GetComponent<PhotonVoiceView>().SpeakerInUse.gameObject.GetComponent<AudioSource>();
//                     audioSource.mute = !audioSource.mute;
//                     break;
//                 }
//             }
//         }
//     }

//     public void KickPress(int ButtonNumber)
//     {
//         if (PhotonNetwork.PlayerList.Length >= ButtonNumber - 1)
//         {
//             foreach (PhotonVRPlayer PVRP in FindObjectsOfType<PhotonVRPlayer>())
//             {
//                 if (PVRP.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.PlayerList[ButtonNumber - 1])
//                 {
//                     GetComponent<PhotonView>().RequestOwnership();
//                     GetComponent<PhotonView>().RPC("KickPlayer", PVRP.gameObject.GetComponent<PhotonView>().Owner);
//                 }
//             }
//         }
//     }


//     [PunRPC]
//     void KickPlayer()
//     {
//         Kicked = true;
//     }

//     public void Report(int ButtonNumber)
//     {
//         string playfabid = playfablogin.MyPlayFabID;
//         if (PhotonNetwork.PlayerList.Length >= ButtonNumber - 1)
//         {
//             foreach (PhotonVRPlayer PVRP in FindObjectsOfType<PhotonVRPlayer>())
//             {
//                 if (PVRP.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.PlayerList[ButtonNumber - 1])
//                 {
//             SendtoWebhook(PhotonNetwork.PlayerList[ButtonNumber - 1].NickName + " " + ((string)PVRP.gameObject.GetComponent<PhotonView>().Owner.CustomProperties["PlayfabID"]) + " was reported by " + PlayerPrefs.GetString("Username", null) + " " + playfablogin.MyPlayFabID);
//                 }
//                 }
//         }
//     }

//     public void SendtoWebhook(string message)
//     {
//         StartCoroutine(PostToDiscord(message));
//     }

//     IEnumerator PostToDiscord(string message)
//     {
//         string jsonPayload = "{\"content\": \"" + message + "\"}";

//         UnityWebRequest www = new UnityWebRequest(WebHookURL, "POST");
//         byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonPayload);
//         www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
//         www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
//         www.SetRequestHeader("Content-Type", "application/json");
//         yield return www.SendWebRequest();

//         if (www.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("Reporting Webhook Error: " + www.error);
//         }
//     }
// }