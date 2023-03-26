using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class NetworkedEnemyFollow : MonoBehaviourPunCallbacks
{
    [Header("Credit to FlimcyVR for portions of this script")]
    [Header("Set speed in navmeshagent component")]
    public float detectRange = 10f;
    //due to networking, I think you cannot set the player position thing in the editor, instead you have to get the collider's parent, in this case gorillaplayer and check the position


    private NavMeshAgent agent;
    private Transform target;

    Vector3 newRandLocation;   
    float sec = 0;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = null;
        StartCoroutine(wait());
    }

    void Update()
    {
        
        sec += Time.deltaTime;
        if (PhotonNetwork.IsMasterClient)
        {
            
            if (target == null) //nobody is in the enemy's range
            {
                
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);
                float closestDistance = Mathf.Infinity;

                
                foreach (Collider collider in colliders)
                {
                    
                    if (collider.gameObject.CompareTag("HandTag")) //checks if the collider is a player
                    {
                        
                        float distance = Vector3.Distance(transform.position, collider.transform.position);
                        
                        if (distance < closestDistance)
                        {
                            //collider.transform.parent.position.z);
                            closestDistance = distance;
                            target = collider.transform;
                        }
                    }
                }
            }
            else //makes enemy to go closest player 
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget > detectRange) 
                {
                    target = null;
                }
                if (target.position.z < 15.5) { //makes sure the target is in the horror area.
                    agent.SetDestination(target.position);
                }
                 //this piece of code comes after the  if (distanceToTarget > detectRange) so that if player get teleported, it will not go to the player's position anymore.
                Debug.Log(target.position);
            }
        }
    }
    IEnumerator wait()
    {
        sec = 0;
        newRandLocation = new Vector3(Random.Range(-61, -17), 21, Random.Range(-3, 24)); //this is where you put in the corners of the floor
        //Debug.Log(newRandLocation); uncomment this to see the newRandLocation in the console

        agent.SetDestination(newRandLocation);

        yield return new WaitUntil( () =>  (Vector3.Distance(transform.position, newRandLocation) < 4 || Mathf.RoundToInt(sec) > 6) ); //&& Vector3.Distance(transform.position, Player.position) > EnemyRange);
        StartCoroutine(wait());
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class NetworkedEnemyFollow : MonoBehaviourPunCallbacks
{
    [Header("Credit to FlimcyVR for portions of this script")]
    [Header("Set speed in navmeshagent component")]
    public float detectRange = 10f;
    public Transform Player;


    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = null;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            
            if (target == null) //nobody is in the enemy's range
            {
                
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);
                float closestDistance = Mathf.Infinity;

                
                foreach (Collider collider in colliders)
                {
                    
                    if (collider.gameObject.CompareTag("HandTag")) //checks if the collider is a player
                    {
                        Debug.Log("blah");
                        float distance = Vector3.Distance(transform.position, collider.transform.position);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            target = collider.transform;
                        }
                    }
                }
            }
            else //makes enemy to go closest player 
            {
                agent.SetDestination(target.position);

                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget > detectRange) 
                {
                    target = null;
                }
            }
        }
    }
}
*/