using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public AudioSource HorrorBG;
    void OnTriggerEnter() {
        HorrorBG.Pause();
    }
}
