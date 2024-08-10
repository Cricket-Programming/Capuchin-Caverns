// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using easyInputs;

// using Photon.VR;
// using Photon.Pun;
// using Photon.VR.Player;

// // This script turns on and off the flashlight when the player pushes the trigger button (index finger button) on their controller.

// public class FlashlightOnOff : MonoBehaviour
// {
//     [SerializeField] private GameObject lightSource;
//     [SerializeField] private PhotonView photonView;

//     private void ToggleFlashlight() {
//         lightSource.SetActive(true);

//     }
// }


// BROKEN, CODE FOR TURNING THE FLASHLIGHT ON AND OFF (DOES NOT WORK)
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using easyInputs;

// using Photon.VR;
// using Photon.Pun;
// using Photon.VR.Player;

// // This script turns on and off the flashlight when the player pushes the trigger button (index finger button) on their controller.
// // It may have an issue when multiple people have flashlight on, not syncing?
// public class FlashlightOnOff : MonoBehaviour
// {
//     [SerializeField] private bool isOn = true;
//     [SerializeField] private GameObject lightSource;
//     [SerializeField] private AudioSource clickSound;
//     [Tooltip("In seconds")]
//     [SerializeField] private float triggerCooldown = 0.5f;
//     [SerializeField] private EasyHand hand;
//     [SerializeField] private PhotonView photonView;
//     private float lastTriggerTime;

//     private void Update() {
//         if (!PhotonNetwork.InRoom) return;
//         if (!photonView.IsMine) return;
//         // Only turn on and off if the player has the cosmetic equiped.
//         if (!PhotonVRManager.Manager.LocalPlayer.GetComponent<PhotonVRPlayer>().Cosmetics.RightHand.Equals("Flashlight")) return;

//         if (CanTrigger()) {
//             if (hand == EasyHand.RightHand && EasyInputs.GetTriggerButtonDown(EasyHand.RightHand) || 
//                 (hand == EasyHand.LeftHand && EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand))) {
//                     ToggleFlashlight();
//                     lastTriggerTime = Time.time;
//                 }
//         }
//     }

//     private bool CanTrigger() {
//         float timeElapsedSec = Time.time - lastTriggerTime; 
//         return timeElapsedSec >= triggerCooldown;
//     }

//     private void ToggleFlashlight() {
//         clickSound.Play();
//         lightSource.SetActive(!isOn);
//         isOn = !isOn;
//     }
// }

