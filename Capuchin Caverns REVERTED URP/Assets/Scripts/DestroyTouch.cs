using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When the player touches the object, it will disappear. It will respawn after respawnDelay.

public class DestroyTouch : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private float respawnDelay = 7f;
    private void OnTriggerEnter() {
        sound.Play();
        // Destroy the object after the sound finishes playing.
        Invoke("HideObject", sound.clip.length); 
    }
    private void HideObject() {
        gameObject.SetActive(false);
        Invoke("ShowObject", respawnDelay);
    }
    private void ShowObject() {
        gameObject.SetActive(true);
    }
}