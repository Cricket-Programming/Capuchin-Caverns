using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//OBJECT BASED SCRIPT WORKING
[RequireComponent(typeof(AudioSource))]
public class HitSounds : MonoBehaviour
{   
    AudioSource Hitsound;
    void Start() 
    {
        Hitsound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter() //this is for the object the the gorilla player hit if its the body 
    {
        Hitsound.Play();
    }

    void OnTriggerEnter()  //this is for the gorilla player's hands
    {
        Hitsound.Play();
    }
}

    /*

Broken I think but attach to player
    public AudioSource Hitsound;
    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("WoodSound"))
        {
            Hitsound.Play();
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("WoodSound"))
        {
            Hitsound.Play();
        }   
    } 
*/