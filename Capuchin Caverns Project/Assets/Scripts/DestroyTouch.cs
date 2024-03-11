using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTouch : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private float delay = 7f;
    private void OnTriggerEnter() {
        sound.Play();
        // Destroy the object after the sound finishes playing.
        Invoke("HideObject", sound.clip.length); 
    }
    private void HideObject() {
        gameObject.SetActive(false);
        Invoke("ShowObject", delay);
    }
    private void ShowObject() {
        gameObject.SetActive(true);
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DestroyTouch : MonoBehaviour
// {
//     [SerializeField] private AudioSource sound;
//     private void OnTriggerEnter() {
//         sound.Play();
//         // Destroy the object after the sound finishes playing.
//         Invoke("DestroyObject", sound.clip.length); 
//     }
//     private void DestroyObject() {
//         Destroy(gameObject);
//     }
// }
