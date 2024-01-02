using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

using Photon.VR;
using Photon.Pun;
using Photon.VR.Player;

public class FlashlightOnOff : MonoBehaviour
{
    [SerializeField] private bool isOn = true;
    [SerializeField] private GameObject lightSource;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private float triggerCooldown = 0.5f;
    [SerializeField] private EasyHand hand;
    private float lastTriggerTime;

    private void Update() {
        if (!PhotonNetwork.InRoom) return;

        // Only turn on and off if the player has the cosmetic equiped.
        if (!PhotonVRManager.Manager.LocalPlayer.GetComponent<PhotonVRPlayer>().Cosmetics.RightHand.Equals("Flashlight")) return;

        if (CanTrigger()) {
            if (hand == EasyHand.RightHand && EasyInputs.GetTriggerButtonDown(EasyHand.RightHand) || 
                (hand == EasyHand.LeftHand && EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand))) {
                    ToggleFlashlight();
                    lastTriggerTime = Time.time;
                }
        }
    
    }

    private bool CanTrigger() {
        return (Time.time - lastTriggerTime) >= triggerCooldown;
    }

    private void ToggleFlashlight() {
        clickSound.Play();
        lightSource.SetActive(!isOn);
        isOn = !isOn;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using easyInputs;

// using Photon.VR;
// using Photon.Pun;
// using Photon.VR.Player;
// public class FlashlightOnOff : MonoBehaviour
// {
//     [SerializeField] private bool isOn = true;
//     [SerializeField] private GameObject lightSource;
//     [SerializeField] private AudioSource clickSound;
//     private bool canTrigger = true;
//     private void Update() {
//         if (!PhotonNetwork.InRoom) return;
//         //only turn on and off if the player has it on.
//         if (!PhotonVRManager.Manager.LocalPlayer.GetComponent<PhotonVRPlayer>().Cosmetics.RightHand.Equals("Flashlight")) return;

//         if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand) && canTrigger) {
//             if (isOn) {
//                 //turnoff
//                 clickSound.Play();
//                 lightSource.SetActive(true);
//                 isOn = false;
//             }
//             else {
//                 // Turn on
//                 clickSound.Play();
//                 lightSource.SetActive(false);
//                 isOn = true;
//             }
//             canTrigger = false;
//         }
//         else {
//             canTrigger = true;
//         }

//     }
// }
