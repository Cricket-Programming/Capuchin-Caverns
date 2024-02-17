
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script will pause the music playing in the AudioSource data field when a player touches it. This script works in conjunction with PlayMusic.cs.
// STOPMUSIC DOES NOT HAVE ITS OWN AUDIOSOURCE COMPONENT (The data field is for PlayMusic's audioSource component)
public class StopMusic : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private void OnTriggerEnter() {
        music.Pause();
    }
}
