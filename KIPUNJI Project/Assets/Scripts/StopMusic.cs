using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    //the playMusic's audioSource component goes in HorrorBG. STOP MUSIC DOES NOT HAVE ITS OWN AUDIOSOURCE COMPONENT
    public AudioSource HorrorBG;
    void OnTriggerEnter() {
        HorrorBG.Pause();
    }
}
