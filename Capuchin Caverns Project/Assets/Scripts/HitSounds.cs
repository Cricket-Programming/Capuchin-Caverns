using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OBJECT BASED SCRIPT WORKING
//How to add hitsounds:
// add audiosource
// uncheck play on awake 
// put hit sound into the Audio Source component's AudioClip.
// add hitsounds script

[RequireComponent(typeof(AudioSource))]
public class HitSounds : MonoBehaviour
{   
    private AudioSource hitSound;
    private void Start() 
    { 
        hitSound = GetComponent<AudioSource>();

        //instead of manually adjusting the audiosource volume for each one in the inspector, you can just put the values here and change them all at once.
        switch (hitSound.clip.name) {
            case "Wood":
                hitSound.volume = 0.06f;
                break;
            case "Dirt&Rocks": 
                hitSound.volume = 0.15f;
                break;
            case "Metal":
                hitSound.volume = 0.3f;
                break;
            case "Button":
                hitSound.volume = 0.4f;
                break;
            default: // Code to execute if expression doesn't match any case
                Debug.LogError("The name of the audio clip " + hitSound.clip.name + "is not specified in the hitsound script. (For volume of the hitsound purposes)");
                break;
        } 
    }

    // onCollisonEnter() gets called when the gorilla player touches the ground with his body.
    private void OnCollisionEnter() 
    {
        hitSound.Play();
    }
    // onTriggerEnter() gets called when the gorilla player touches things with his hands.
    private void OnTriggerEnter()  
    {
        hitSound.Play();
    }
    
}

/*

*/