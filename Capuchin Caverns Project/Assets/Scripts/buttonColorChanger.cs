using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonColorChanger : MonoBehaviour
{   
    public Material buttonPressed;
    public Material buttonUnPressed;
    //the sound of this button is on a hitsounds script.

    void OnTriggerEnter(Collider other) {    
        this.GetComponent<Renderer>().material = buttonPressed;
    }

    void OnTriggerExit() {
        this.GetComponent<Renderer>().material = buttonUnPressed;
    }
}
