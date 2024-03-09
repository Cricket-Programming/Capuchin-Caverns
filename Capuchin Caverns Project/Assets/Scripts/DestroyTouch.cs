using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTouch : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    private void OnTriggerEnter() {
        sound.Play();
        // Destroy the object after the sound finishes playing.
        Invoke("DestroyObject", sound.clip.length); 
    }
    private void DestroyObject() {
        Destroy(gameObject);
    }
}
