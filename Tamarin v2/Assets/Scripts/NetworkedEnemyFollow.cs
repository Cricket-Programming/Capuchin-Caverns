using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class NetworkedEnemyFollow : MonoBehaviourPunCallbacks
{
    [Header("Credit to FlimcyVR for portions of this script")]
    //credit omarVision for some randomnavmeshstuff
    [Header("Set speed in navmeshagent component")]
    public float detectRange = 10f;
    private NavMeshAgent nma = null; //nma stands for navMeshAgent
    private Bounds bndFloor;
    private Vector3 moveto;
    private bool flag = false;

    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        bndFloor = GameObject.Find("Horror Floor1").GetComponent<Renderer>().bounds;
        SetRandomDestination();

        target = null;
    }

    void Update()
    {
        if (nma.hasPath == false && flag == false)
        {
            flag = true;
            SetRandomDestination();
        }
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
            else //somebody is in range
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget > detectRange) //|| target.position.z > 15.5) //if player out of range or 
                {
                    target = null;
                }
                else if (target.position.z < 15.5) { //if target is in horror area, go to the horror area.
                    nma.SetDestination(target.position);
                }
                 //this piece of code comes after the  if (distanceToTarget > detectRange) so that if player get teleported, it will not go to the player's position anymore.
                Debug.Log(target.position);
            }
        }
    }
    private void SetRandomDestination()
    {
        //1. pick a point
        float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
        float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);
        moveto = new Vector3(rx, this.transform.position.y, rz);
        nma.SetDestination(moveto); //figure out path, starts gameobject moving

        flag = false;
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
    //credit omarVision for some randomnavmeshstuff
    [Header("Set speed in navmeshagent component")]
    public float detectRange = 10f;


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
            else //somebody is in range
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget > detectRange) //|| target.position.z > 15.5) //if player out of range or 
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

        yield return new WaitUntil( () =>  (Vector3.Distance(transform.position, newRandLocation) < 4 || Mathf.RoundToInt(sec) > 3) ); //&& Vector3.Distance(transform.position, Player.position) > EnemyRange);
        StartCoroutine(wait());
    }
}



*/