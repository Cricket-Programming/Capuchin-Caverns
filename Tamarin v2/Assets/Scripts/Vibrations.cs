using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;
public class Vibrations : MonoBehaviour
{ 
    public float Amplitude;
    public bool LeftHand; //if you create a bool, it makes it a checkbox I think


    void OnTriggerEnter() {

        if(LeftHand)
        {
            StartCoroutine(EasyInputs.Vibration(EasyHand.LeftHand, Amplitude, 0.15f));
            
        }
        if(!LeftHand)
        {

            StartCoroutine(EasyInputs.Vibration(EasyHand.RightHand, Amplitude, 0.15f));


        }

    }

    
}