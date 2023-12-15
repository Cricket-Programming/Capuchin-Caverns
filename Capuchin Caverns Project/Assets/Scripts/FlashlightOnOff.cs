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
    private void Update() {
        if (!PhotonNetwork.InRoom) return;
        //only turn on and off if the player has it on.
        if (!PhotonVRManager.Manager.LocalPlayer.GetComponent<PhotonVRPlayer>().Cosmetics.RightHand.Equals("Flashlight")) return;

        if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand)) {
            if (isOn) {//turnoff
                isOn = false;
                clickSound.Play();
                lightSource.SetActive(true);
            }
            else {
                isOn = true;
                clickSound.Play();
                lightSource.SetActive(false);
                //turn on
            }
        }

    }
}
