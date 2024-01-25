using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// How to add hit sounds to objects:
// - add hitsounds script, this will also add an audiosource component
// - Uncheck play on awake on audiosource component
// - put hit sound into the Audio Source component's AudioClip.
[RequireComponent(typeof(AudioSource))]
public class HitSounds : MonoBehaviour
{   
    [Tooltip("When this is checked, the volume on the audiosource component is used for the hitsound instead.")]
    [SerializeField] private bool manualVolume = false;
    private AudioSource hitSound;
    private void Start() 
    { 
        hitSound = GetComponent<AudioSource>();

        // Instead of manually adjusting the audiosource volume for each one in the inspector, you can just put the volume values here and change them all in the script.
        if (!manualVolume) {
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
                case "Glass Ice":
                    hitSound.volume = 1f;
                    break;
                default: // Code to execute if expression doesn't match any case
                    Debug.LogError("The name of the audio clip " + hitSound.clip.name + " is not specified in the hitsound script. (For volume of the hitsound purposes)");
                    break;
            }

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