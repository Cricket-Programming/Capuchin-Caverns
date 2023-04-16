using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource HorrorBG;
    void OnTriggerEnter() {
        HorrorBG.Play();
    }

}
