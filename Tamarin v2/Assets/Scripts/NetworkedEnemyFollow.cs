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

        //Fluffy only works when connected to multiplayer
        if (PhotonNetwork.IsMasterClient) {
            SetRandomDestination(); 
        }
        
        target = null;
    }

    void Update()
    {

        if (PhotonNetwork.IsMasterClient)
        {      
            if (!nma.hasPath && !flag)
            {
                flag = true;
                SetRandomDestination();
            }
            if (target == null) //nobody is in the enemy's range
            {
                findClosestPlayersTransform();
            }
            else //some player is in range
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget > detectRange) //if player is out of ranges
                {
                    target = null;
                }
                //this else if comes after the  if (distanceToTarget > detectRange) so that if player get teleported, it will not go to the player's position anymore.
                else if (target.position.z < 15.5) { //if target is in horror area, go to the horror area.
                    nma.SetDestination(target.position);
                }

            }
        }
    }
    void findClosestPlayersTransform() {
        float distanceToClosestPlayer = Mathf.Infinity;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); //this gets all the gameObjects with a tag of `Player`. This tag is ONLY on the head of the photonVR player. If there is 3 people in the room, then the length of this array will be 3.
        foreach (GameObject p in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if (distanceToPlayer < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToPlayer;
                target = p.transform;
                //Debug.Log(p.transform.position);
            }
        }
    }
    private void SetRandomDestination()
    {
        //1. pick a points
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

        //Fluffy only works when connected to multiplayer
        if (PhotonNetwork.IsMasterClient) {
            SetRandomDestination(); 
        }
        
        target = null;
    }

    void Update()
    {

        if (PhotonNetwork.IsMasterClient)
        {      
            if (!nma.hasPath && !flag)
            {
                flag = true;
                SetRandomDestination();
            }
            if (target == null) //nobody is in the enemy's range
            {
                
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange); //OverlapSphere(Vector3 position, float radius,
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
}s

*/