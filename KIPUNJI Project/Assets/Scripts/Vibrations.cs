using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;
public class Vibrations : MonoBehaviour
{ 
    public float Amplitude;
    public bool LeftHand; //if you create a bool, it will show up as a checkbox in the editor
    //public LayerMask vibrationEnabledLayers;
    private static int walkThroughLayerNumber;
    
    private void Start() {
        walkThroughLayerNumber = LayerMask.NameToLayer("Walk Through");
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer != walkThroughLayerNumber) { //make gameobject have Walk Through layer and WalkThrough tag//does not let vibrations happen for objects with certain tags, should be replaced in the future with objects with certain layers.
            if(LeftHand)
            {
                StartCoroutine(EasyInputs.Vibration(EasyHand.LeftHand, Amplitude, 0.1f)); //duration
                Debug.Log("VIBRATED"); //Logs VIBRATED if the hand is vibrated.
                
            }
            else //right hand
            {
                StartCoroutine(EasyInputs.Vibration(EasyHand.RightHand, Amplitude, 0.1f));
            }
        }

    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;
public class Vibrations : MonoBehaviour
{ 
    public float Amplitude;
    public bool LeftHand; //if you create a bool, it will show up as a checkbox in the editor
    //public LayerMask vibrationEnabledLayers;
    
    void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("WalkThrough")) { //make gameobject have Walk Through layer and WalkThrough tag//does not let vibrations happen for objects with certain tags, should be replaced in the future with objects with certain layers.
            if(LeftHand)
            {
                StartCoroutine(EasyInputs.Vibration(EasyHand.LeftHand, Amplitude, 0.1f));
                //Debug.Log("VIBRATED"); Logs VIBRATED if the hand is vibrated.
                
            }
            else if(!LeftHand)
            {
                StartCoroutine(EasyInputs.Vibration(EasyHand.RightHand, Amplitude, 0.1f));
            }
        }

    }
}
*/