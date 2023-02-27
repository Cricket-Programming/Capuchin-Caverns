using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{

    public GameObject FlashyThing;

    public GameObject AudioSource;

    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("HandTag"))
        {
            Invoke("scare", 0.0f);

        }
    }

    void scare(){
        FlashyThing.SetActive(true);
        AudioSource.SetActive(true);
        Invoke("QuitGame", 4.0f);
    }

     void QuitGame(){

    Application.Quit();

    }
}
