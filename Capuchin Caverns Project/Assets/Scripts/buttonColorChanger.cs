using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonColorChanger : MonoBehaviour
{   
    [SerializeField] private Material buttonPressed;
    [SerializeField] private Material buttonUnPressed;
    //the sound of this button is on a hitsounds script.

    private void OnTriggerEnter(Collider other) {    
        this.GetComponent<Renderer>().material = buttonPressed;
    }

    private void OnTriggerExit() {
        this.GetComponent<Renderer>().material = buttonUnPressed;
    }
}
