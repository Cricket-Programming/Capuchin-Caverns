using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    //CHANGE THE SPEED OF THE ENEMY IN THE NAVMESHAGENT COMPONENT
    public NavMeshAgent Enemy;
    public Transform Player; 
    public Transform Floor;
    public float EnemyRange = 4.0f; //changes the range of the enemy

    Vector3 newRandLocation;

    void Start() {
        StartCoroutine(wait());
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position); //returns the distance between A and B, transform.position is the Enemy (basically the position of the player this script is part of)

        if (distance < EnemyRange) //if the player is in range to the enemy, 
        {
            Enemy.SetDestination(Player.position);
        }
        else 
        {
            
        }
    }
    IEnumerator wait()
    {
        newRandLocation = new Vector3(Random.Range(-61, -30), 21, Random.Range(-2, 24));
        Debug.LogWarning(newRandLocation);
        Enemy.SetDestination(newRandLocation);

        yield return new WaitForSeconds(3);
        StartCoroutine(wait());
    }

}

/*
    //CHANGE THE SPEED OF THE ENEMY IN THE NAVMESHAGENT COMPONENT
    public NavMeshAgent Enemy;
    public Transform Player; 
    public Transform Floor;
    public float EnemyRange = 4.0f; //changes the range of the enemy

    Vector3 newRandLocation;

    private void Start() {
        StartCoroutine(wait());
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position); //returns the distance between A and B, transform.position is the Enemy (basically the position of the player this script is part of)

        if (distance < EnemyRange) //if the player is in range to the enemy, 
        {
            Enemy.SetDestination(Player.position);
        }
        else 
        {
            
            
        }
    }
    IEnumerator wait()
    {
        newRandLocation = new Vector3(Random.Range(-61, -30), 21, Random.Range(-2, 24));
        Debug.LogWarning(newRandLocation);
        Enemy.SetDestination(newRandLocation);

        yield return new WaitForSeconds(10);
    }









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
