using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSounds : MonoBehaviour
{   ///<p>blah</p>
    public AudioSource Hitsound;
    void OnCollisionEnter()
    {
        Hitsound.Play();
    
    }
}
