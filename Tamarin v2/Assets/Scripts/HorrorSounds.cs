using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use camelCase
//this script beeps faster the closer the enemy is to the player, it should be on the enemy
public class HorrorSounds : MonoBehaviour
{
    public Transform Player; 
    public AudioSource HorrorBG;
    void Update() {
        if (Player.position.z < 25) {
            HorrorBG.volume = 0.1f;
        }
        else {
            HorrorBG.volume = 0;
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



