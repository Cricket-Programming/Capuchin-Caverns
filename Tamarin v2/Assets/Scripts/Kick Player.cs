using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KickPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Body"))  //if the gameObject touching the object with tag Enemy Body
        {
            Kick();
        }    
    }    

    void Kick() //void means this method does not return anything
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   
}
