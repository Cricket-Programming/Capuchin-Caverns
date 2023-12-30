// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;
// using Photon.Pun;
// using Photon.Realtime;
// public class MuteButton : MonoBehaviour
// {
//     [SerializeField] public int ButtonNumber;
//     [SerializeField] public LeaderBoard LB;
//     [SerializeField] public string HandTag = "HandTag";

//     private bool Muted = false;
//     public Material MutedMaterial;
//     private Material UnMutedMaterial;
//     private Renderer rend;

//     private Player MutedUser;

//     private void Start()
//     {
//         rend = GetComponent<Renderer>();
//         UnMutedMaterial = rend.material;
//     }

//     private void Update()
//     {
//         if (ButtonNumber > 0 && ButtonNumber <= PhotonNetwork.PlayerList.Length)
//         {
//             if (PhotonNetwork.PlayerList[ButtonNumber - 1] != MutedUser && Muted)
//             {
//                 Muted = false;
//                 rend.material = UnMutedMaterial;
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
//     private void OnTriggerEnter(Collider other)
//     {
//         if (ButtonNumber > 0 && ButtonNumber <= PhotonNetwork.PlayerList.Length)
//         {
//             if (other.CompareTag("HandTag"))
//             {
//                 MutePress(ButtonNumber);

//                 Muted = !Muted;
//                 MutedUser = PhotonNetwork.PlayerList[ButtonNumber - 1];
//                 if (Muted)
//                 {
//                     rend.material = MutedMaterial;
//                 }
//                 else
//                 {
//                     rend.material = UnMutedMaterial;
//                 }
//             }
//         }
//     }
// }
