using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if distance between transform.positions (declared with public Gameobject thing) is < something the set destintion)
        enemy.SetDestination(Player.position);
    }
}

/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    private NavMeshAgent Enemy;

    public GameObject Player; //I think gameobject serialized field where you can put in the player object

    public float EnemyDistanceRun = 4.0f; //changes the range of the enemy
     
    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position); //returns the difference between A and B, transform.position is the Enemy (basically the position of the player this script is part of)

        if (distance < EnemyDistanceRun) //if the player is in range to the enemy, 
        {
            //enemy runs toward the player
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            Enemy.SetDestination(newPos);
        }
    }
}
*/
