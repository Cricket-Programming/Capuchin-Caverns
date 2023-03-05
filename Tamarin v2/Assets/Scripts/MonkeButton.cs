using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeButton : MonoBehaviour
{   
    public GameObject Button;
    public Material buttonPressed;
    public Material buttonUnPressed;
    //public AudioSource sound;

    void OnTriggerEnter()
    {
        Button.GetComponent<Renderer>().material = buttonPressed;
        //sound.Play();
    }

    void OnTriggerExit()
    {
        Button.GetComponent<Renderer>().material = buttonUnPressed;
        //sound.Stop();
    }
}
