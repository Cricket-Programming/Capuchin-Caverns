using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Photon.Pun;
using Photon.Realtime;
using Photon.VR;
using Photon.VR.Player;
using Photon.Voice.PUN;

// This script provides the muting functionality for the mute buttons on the leaderboard.
public class MuteButton : MonoBehaviour
{
    [SerializeField] private int buttonNumber;
    [SerializeField] private Material mutedMaterial;
    private Material unMutedMaterial;
    private Renderer rend;
    private bool muted = false;

    private Player MutedUser;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        unMutedMaterial = rend.material;

    }

    private void MutePress(int ButtonNumber)
    {
        if (PhotonNetwork.PlayerList.Length >= ButtonNumber - 1) // checks if the mute button pressed has a player associated with it.
        {
            foreach (PhotonVRPlayer PVRP in FindObjectsOfType<PhotonVRPlayer>())
            {
                if (PVRP.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.PlayerList[ButtonNumber - 1])
                {
                    AudioSource audioSource = PVRP.gameObject.GetComponent<PhotonVoiceView>().SpeakerInUse.gameObject.GetComponent<AudioSource>();
                    audioSource.mute = !audioSource.mute;
                    break;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (buttonNumber > 0 && buttonNumber <= PhotonNetwork.PlayerList.Length)
        {
            if (other.CompareTag("HandTag"))
            {
                MutePress(buttonNumber);

                muted = !muted;
                MutedUser = PhotonNetwork.PlayerList[buttonNumber - 1];
                if (muted)
                {
                    rend.material = mutedMaterial;
                }
                else
                {
                    rend.material = unMutedMaterial;
                }
            }
        }
    }
}


// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;
// using Photon.Pun;
// using Photon.Realtime;
// using Photon.VR;
// using Photon.VR.Player;
// using Photon.Voice.PUN;

// // This script provides the muting functionality for the leaderboard.
// public class MuteButton : MonoBehaviour
// {
//     [SerializeField] private int ButtonNumber;
//     public Material MutedMaterial;
//     private Material UnMutedMaterial;
//     private Renderer rend;
//     private bool muted = false;

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
//             if (PhotonNetwork.PlayerList[ButtonNumber - 1] != MutedUser && muted)
//             {
//                 muted = false;
//                 rend.material = UnMutedMaterial;
//             }
//         }
//     }

//     private void MutePress(int ButtonNumber)
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

//                 muted = !muted;
//                 MutedUser = PhotonNetwork.PlayerList[ButtonNumber - 1];
//                 if (muted)
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
