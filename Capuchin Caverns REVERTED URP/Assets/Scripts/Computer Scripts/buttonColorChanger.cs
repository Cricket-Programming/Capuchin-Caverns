using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorChanger : MonoBehaviour
{   
    [SerializeField] private Material buttonPressed;
    [Tooltip("Leave buttonUnPressed as null/none if you want to use the renderers material as the unpressed material")]
    [SerializeField] private Material buttonUnPressed;
    
    //the sound of this button is on a hitsounds script.
    private void Start() {
        if (buttonUnPressed == null) {
            buttonUnPressed = GetComponent<Renderer>().material;
        } 
    }
    private void OnTriggerEnter() {    
        this.GetComponent<Renderer>().material = buttonPressed;
    }

    private void OnTriggerExit() {
        this.GetComponent<Renderer>().material = buttonUnPressed;
    }
}
