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
    AudioSource hitSound;
    void Start() 
    { 
        hitSound = GetComponent<AudioSource>();
    }

    // onCollisonEnter() gets called when the gorilla player touches the ground with his body.
    void OnCollisionEnter() 
    {
        hitSound.Play();
    }
    // onTriggerEnter() gets called when the gorilla player touches things with his hands.
    void OnTriggerEnter()  
    {
        hitSound.Play();
    }
    
}

/*

*/