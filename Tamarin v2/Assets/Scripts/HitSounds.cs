using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSounds : MonoBehaviour
{   
    public AudioSource Hitsound;
    void OnCollisionEnter() //this is for the object the the gorilla player hit
    {
        Hitsound.Play();
    }
    void OnTriggerEnter()  //this is for the gorilla player
    {
        Hitsound.Play();
    }
}

    /*


        public AudioSource Hitsound;
    /*
    void OnCollisionEnter(Collision other) {
        if (other.CompareTag("WoodSound"))
        {
            Hitsound.Play();
        }
    }
        void OnTriggerEnter(Collider other) {
        if (other.CompareTag("WoodSound"))
        {
            Hitsound.Play();
        }   
    } 
    */


