using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script will play the audioSource when the player touches this object. This script works in conjunction with StopMusic.cs

// How to get it set up
// - Add PlayMusic script to the object that activates the music (This will automatically add an audio source component).
// - Put the audioclip into the audiosource component
// - Put the audiosource into the audioSource data field in the inspector.
[RequireComponent(typeof(AudioSource))]
public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter() {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource audioSource in allAudioSources) {
            audioSource.Pause();
        }
        audioSource.Play();
    }
}
