using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script beeps faster the closer the enemy is to the player, it should be on the enemy
public class BeepDistance : MonoBehaviour
{
    public Transform Player; 

    void Start()
    {
        
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        Debug.Log(distance);
        //if (distance < )
    }
}
