using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
//networking should work, if there is a photon view and photon transform view set to the settings of https://www.youtube.com/watch?v=hOQ11Es8Ehg&t=254s
//This script currently makes Fluffy work only when connected to multiplayer. 
public class NetworkedEnemyFollow : MonoBehaviourPunCallbacks
{
    //credit omarVision for some of the SetRandomDestination() code, credit FlimcyVR for part of the findClosestPlayerTransform() code.
    [Header("Set the speed of Fluffy in the NavMeshAgent component.")]
    public float detectRange = 10f;
    private NavMeshAgent nma = null; //nma stands for navMeshAgent

    private Bounds bndFloor;
    private Vector3 moveto;
    private bool flag = false;

    private Transform target;
    private GameObject[] players; //maybe change to Transform[] or something like that
    private float distanceToClosestPlayer;


    void Start()
    {
        target = null;
        nma = this.GetComponent<NavMeshAgent>();
        bndFloor = GameObject.Find("Horror Floor1").GetComponent<Renderer>().bounds;

        //the MasterClient is the first player in the room. It will switch automatically if he leaves.
        if (PhotonNetwork.IsMasterClient) {
            SetRandomDestination(); 
        }
          
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

            findClosestPlayersTransform();
            
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            //if target is in horror area and is in range to the enemy
            if (target.position.z < 15.5 && distanceToTarget < detectRange) { 
                nma.SetDestination(target.position);
            }  
        }
    }
    //sets the target variable to the closest player transform position
    void findClosestPlayersTransform() {
        distanceToClosestPlayer = Mathf.Infinity;
        players = GameObject.FindGameObjectsWithTag("Player"); //this gets all the gameObjects with a tag of `Player`. This tag is ONLY on the head of the photonVR player. If there is 3 people in the room, then the length of this array will be 3.
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
        //the masterclient is the first player in the room. It will switch automatically if he leaves.
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

            findClosestPlayersTransform();
           
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            //if target is in horror area and is in range to the enemy
            if (target.position.z < 15.5 && distanceToTarget < detectRange) { 
                nma.SetDestination(target.position);
            }  
        }
    }
    //sets the target variable to the closest player transform position
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

*/