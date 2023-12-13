using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;
public class FlashlightOnOff : MonoBehaviour
{
    [SerializeField] private bool isOn = true;
    [SerializeField] private GameObject lightSource;
    [SerializeField] private AudioSource clickSound;
    private void FixedUpdate() {
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
