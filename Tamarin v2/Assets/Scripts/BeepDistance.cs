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
            if (distance < 5f) {
                delay = 0.3f;
            }
            else if (distance < 10f) {
                delay = 1f;
            }
            else if (distance < 50f) {
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
    /*
    void Start()
    {
        
    }

    void Update()
    {
        
        Debug.Log(distance);
        beepSound.Play();
        //if (distance < )
    }
    */


}
