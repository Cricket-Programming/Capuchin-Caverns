using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//camelcase for variables, pascalcase for function names
public class TeleportWithDelay : MonoBehaviour
{
    public GameObject mapToDisable;

    public Transform gorillaPlayer;

    //public List<GameObject> targetObjects;
    public Transform jumpscareLocation;
    public Transform respawnLocation;

    public float waitTime;

    //jumpscareObjects are the objects 
    public GameObject jumpscareObjects;
    public AudioSource jumpscareSound;


    void teleportIfFall()
    {
        if (gorillaPlayer.position.y < 15)
        {
            //Debug.Log("Teleporting");
            StartCoroutine(Teleport());
        }
    }

    void Update() {
        teleportIfFall();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(gorillaPlayer)) {
           StartCoroutine(Teleport());  
        }
            
    }

    IEnumerator Teleport() //and jumpscare
    {
        //the issue with setting the maps to inactive is that while the jumpscare is happening, the agent is not moving. 
        //If the player being jumpscared is the masterclient, the players won't see him being moved
        mapToDisable.SetActive(false);   
        
        gorillaPlayer.position = jumpscareLocation.position;

        jumpscareObjects.SetActive(true);
        jumpscareSound.Play();

        yield return new WaitForSeconds(waitTime +12233);

        gorillaPlayer.position = respawnLocation.position;
        jumpscareObjects.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        mapToDisable.SetActive(true);
           
    }
    //the jumpscare is NOT networked which is good (like 3rd person)


}





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//camelcase for variables, pascalcase for function names
public class TeleportWithDelay : MonoBehaviour
{
    public GameObject mapToDisable;

    public Transform gorillaPlayer;

    //public List<GameObject> targetObjects;
    public Transform jumpscareLocation;
    public Transform respawnLocation;

    public float waitTime;

    //jumpscareObjects are the objects 
    public GameObject jumpscareObjects;
    public AudioSource jumpscareSound;


    void teleportIfFall()
    {
        if (gorillaPlayer.position.y < 15)
        {
            //Debug.Log("Teleporting");
            StartCoroutine(Teleport());
        }
    }

    void Update() {
        teleportIfFall();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(gorillaPlayer)) {
           StartCoroutine(Teleport());  
        }
            
    }

    IEnumerator Teleport() //and jumpscare
    {
        mapToDisable.SetActive(false);   
        
        gorillaPlayer.position = jumpscareLocation.position;

        jumpscareObjects.SetActive(true);
        jumpscareSound.Play();

        yield return new WaitForSeconds(waitTime +12233);

        gorillaPlayer.position = respawnLocation.position;
        jumpscareObjects.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        mapToDisable.SetActive(true);
           
    }
    //the jumpscare is NOT networked which is good (like 3rd person)


}


*/
