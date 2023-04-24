using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OBJECT BASED SCRIPT WORKING
//put this script along with an audioSource component on the gameobject you want to have hitsounds on. Put the audio sound you want to play in the audiosource component
[RequireComponent(typeof(AudioSource))]
public class HitSounds : MonoBehaviour
{   
    AudioSource hitSound;
    void Start() 
    { 
        hitSound = GetComponent<AudioSource>();
    }

    //onCollisonEnter() gets called when the gorilla player touches the ground with his body.
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