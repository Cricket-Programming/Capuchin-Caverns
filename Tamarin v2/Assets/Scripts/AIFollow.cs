using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//networking should work, if there is a photon view and photon transform view.
public class AIFollow : MonoBehaviour
{
    //CHANGE THE SPEED OF THE ENEMY IN THE NAVMESHAGENT COMPONENT
    public NavMeshAgent Enemy;
    public Transform Player; 
    public AudioSource HorrorBG;
    
    public float EnemyRange = 4.0f; //changes the range of the enemy

    Vector3 newRandLocation;   
    float sec = 0;
    void Start() {
        StartCoroutine(wait());
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position); //returns the distance between A and B, transform.position is the Enemy (basically the position of the player this script is part of)

        if (distance < EnemyRange && Player.position.z < 15) //if the player is in range to the enemy, and is in the horror area
        {
            Enemy.SetDestination(Player.position);
            sec = 5.99f; //this is so that if player leaves horror area (goes to the entrance), StartCoroutine(wait()); can be triggered and the enemy will right away leave the horror entrance
            
        }

        Debug.Log(Player.position.z);

        sec += Time.deltaTime;

    }
    IEnumerator wait()
    {
        sec = 0;
        newRandLocation = new Vector3(Random.Range(-61, -17), 21, Random.Range(-3, 24)); //this is where you put in the corners of the floor
        //Debug.Log(newRandLocation); uncomment this to see the newRandLocation in the console

        Enemy.SetDestination(newRandLocation);

        yield return new WaitUntil( () =>  (Vector3.Distance(transform.position, newRandLocation) < 2 || Mathf.RoundToInt(sec) > 6) ); //&& Vector3.Distance(transform.position, Player.position) > EnemyRange);
        StartCoroutine(wait());
    }

}

/*
/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//networking should work, if there is a photon view and photon transform view.
public class AIFollow : MonoBehaviour
{
    //CHANGE THE SPEED OF THE ENEMY IN THE NAVMESHAGENT COMPONENT
    public NavMeshAgent Enemy;
    public Transform Player; 
    public AudioSource HorrorBG;
    
    public float EnemyRange = 4.0f; //changes the range of the enemy

    Vector3 newRandLocation;   
    float sec = 0;
    void Start() {
        StartCoroutine(wait());
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position); //returns the distance between A and B, transform.position is the Enemy (basically the position of the player this script is part of)

        if (distance < EnemyRange && Player.position.z < 15) //if the player is in range to the enemy, and is in the horror area
        {
            Enemy.SetDestination(Player.position);
            sec = 5.99f; //this is so that if player leaves horror area (goes to the entrance), StartCoroutine(wait()); can be triggered and the enemy will right away leave the horror entrance
            
        }

        Debug.Log(Player.position.z);

        sec += Time.deltaTime;

    }
    IEnumerator wait()
    {
        sec = 0;
        newRandLocation = new Vector3(Random.Range(-61, -17), 21, Random.Range(-3, 24)); //this is where you put in the corners of the floor
        //Debug.Log(newRandLocation); uncomment this to see the newRandLocation in the console

        Enemy.SetDestination(newRandLocation);

        yield return new WaitUntil( () =>  (Vector3.Distance(transform.position, newRandLocation) < 2 || Mathf.RoundToInt(sec) > 6) ); //&& Vector3.Distance(transform.position, Player.position) > EnemyRange);
        StartCoroutine(wait());
    }

}







SCRIPT INFO: Original script from the school tutorial
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    private NavMeshAgent Enemy;

    public GameObject Player; //Gameobject serialized field (but public) where you can put in the player object

    public float EnemyRange = 4.0f; //changes the range of the enemy
     
    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position); //returns the difference between A and B, transform.position is the Enemy (basically the position of the player this script is part of)

        if (distance < EnemyRange) //if the player is in range to the enemy, 
        {
            //enemy runs toward the player
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            Enemy.SetDestination(newPos);
        }
    }
}
*/
