using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put script in enemy cat
public class RespawnPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject TargetRespawn;
 
    void OnTriggerEnter(Collider other){
 
        if(other.gameObject.tag == "Body" || other.gameObject.CompareTag("HandTag"))
        {
            player.transform.position = TargetRespawn.transform.position;

        }
     }




}
/*
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Body")) // 
        {
            Destroy(this);
        }
    }
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "EnemyCat")   //player Body
        {
            Debug.Log("dddddddddddddddddd  dddddddddddddddd"); //works!
            transform.position = new Vector3(13f ,1f, 15f);

        }

    }
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Body")   //player Body
        {
            transform.position = TargetRespawn.position;
        }

    }
*/