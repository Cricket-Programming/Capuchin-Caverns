using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

INSTRUCTIONS:
1) Attach this script to the thing
2) Put in audiosource and drag audiosource into HorrorBG.
3) make sure box collider istrigger is false
4) mesh renderer Disableds
*/

public class HorrorSounds : MonoBehaviour
{
    [SerializeField] private AudioSource HorrorBG;
    
    bool playing = false;
    private void OnTriggerEnter(Collider other) { //even though this cube's box collider istrigger is false, istrigger is still activated by the gorilla player
        int leftHand = LayerMask.NameToLayer("Left Hand"); //since .layer takes an int, to get from name to int, use this.
        if (other.gameObject.CompareTag("HandTag") && other.gameObject.layer == leftHand) {
            if (playing) {
                HorrorBG.Pause();
                playing = false;
                Debug.Log("Not Playing");
            }
            else {
                HorrorBG.Play();
                playing = true;
                Debug.Log("Playing");
            }
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use camelCase
//this script beeps faster the closer the enemy is to the player, it should be on the enemy
public class BeepDistance : MonoBehaviour
{
    public Transform player; 
    public AudioSource beepSound;

    float delay = 1f;
    float delayTemp;

    void Start () {
        delayTemp = delay;
    }
     
    void Update () {
        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance" + distance);
        if (beepSound.isPlaying) {
            if (distance < 7f) {
                delay = 0.3f;
            }
            else if (distance < 15f) {
                delay = 1f;
            }
            else if (distance < 35f) {
                delay = 2f;
            }
        }
        else {
            delay -= Time.deltaTime;
        }   

        if (delay <= 0) {
            beepSound.Play();
        }

    }

*/



