using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using Photon.Pun; //all these classes inside of this is PascalCase. Ex: PhotonNetwork

//networking should work, if there are photon view and photon transform view components set to the settings of https://www.youtube.com/watch?v=hOQ11Es8Ehg&t=254s.
//this script works with navmesh's component based workflow. Follow this tutorial to get it set up: https://www.youtube.com/watch?v=aHFSDcEQuzQ.
//credit omarVision for some of the SetRandomDestination() code, credit FlimcyVR for part of the findClosestPlayerTransform() code.

public enum HorrorDirection {
    LessThan,
    GreaterThan,

}
public class NetworkedEnemyFollow : MonoBehaviour 
{
    
    [Header("Set the speed of the creature in the NavMeshAgent component.")]
    [SerializeField] private float detectRange = 10f;

    [Tooltip("This is the position between being in the safe area and the horror area. Go into code and fix > or < and x, y, or z direction")]
    [SerializeField] private float divideLine = 15.5f;
    [SerializeField] private HorrorDirection directionToHorror;
    [SerializeField] private string horrorFloorName;
    private NavMeshAgent nma = null; //nma stands for navMeshAgent

    private Bounds bndFloor;
    private Vector3 moveto;
    private bool flag = false;

    private Transform target; //also the closest player

    private GameObject[] players;
    private float distanceToClosestPlayer;

    //Everything regarding networking should be controlled by the MasterClient.
    private void Start()
    {
        //this needs to be accessed by every player, not only the MasterClient.
        nma = GetComponent<NavMeshAgent>();

        //this gets the bounds of the horror floor
        if (GameObject.Find(horrorFloorName) == null) {
            Debug.LogError("You typed in the name of the horror floor wrong");
        } else {
            bndFloor = GameObject.Find(horrorFloorName).GetComponent<Renderer>().bounds;  
        }


         
    }

    private void Update()
    {
        //the MasterClient (also called the host) is the first player in the room. The MasterClient will switch automatically if the current one leaves.
        //you don't need MonoBehaviourPunCallbacks to access this anything from PhotonNetwork such as the PhotonNetwork.IsMasterClient.
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected) { //The !PhotonNetwork.IsConnected part makes it so that Fluffy moves when the player is not connected to multiplayer.      

            if (!nma.hasPath && !flag)
            {
                flag = true;
                SetRandomDestination();
            
            }
            
            // this method sets distanceToClosestPlayer and target (target is the closest player)
            findClosestPlayersTransform(); 

            //If target is in the horror area and is in range to the enemy, go to the target player.
            if (target != null) { //if the player for some reason disconnects from the server making the target destroyed, this code makes sure the target exists before trying to access the transform component preventing a nullreferenceexception.
                if (TargetInRangeAndInHorror()) {  
                    nma.SetDestination(target.position);
                } 
            }
            //Debug.Log(target.position.z); //if you don't know what to put for the 15.5 above, you can uncomment this to find the value of the beginning of the horror area.

        }
    }
    private bool TargetInRangeAndInHorror() {
        return (distanceToClosestPlayer < detectRange && 
                    (directionToHorror == HorrorDirection.LessThan && target.position.z < divideLine) ||
                    (directionToHorror == HorrorDirection.GreaterThan && target.position.z > divideLine)
                    );
    }
    //sets target to closest player transform position, sets distanceToClosestPlayer to distance to closest player.
    private void findClosestPlayersTransform() {
        distanceToClosestPlayer = Mathf.Infinity;
        players = GameObject.FindGameObjectsWithTag("Player"); //this gets all the gameObjects with a tag of `Player`. This tag is ONLY on the head of the photonVR player. If there is 3 players in the room, then the length of this array will be 3. The reason I put this on the head instead of the parent is because the parent always stays at Vector3(0, 0 ,0), NOT TRUE ANYMORE BC OF this transfomr thing in PhotonVRPlayer
        foreach (GameObject p in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if (distanceToPlayer < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToPlayer;
                target = p.transform;
                //Debug.Log(p.transform.position.z);
            }
        }
    }
    //SetRandomDestination picks a random x and z and sets the enemy's destination to that point.
    private void SetRandomDestination()
    {
        float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
        float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);
        moveto = new Vector3(rx, transform.position.y, rz); 
        nma.SetDestination(moveto); 

        flag = false;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using UnityEngine.AI;
// using Photon.Pun; //all these classes inside of this is PascalCase. Ex: PhotonNetwork

// //networking should work, if there are photon view and photon transform view components set to the settings of https://www.youtube.com/watch?v=hOQ11Es8Ehg&t=254s.
// //this script works with navmesh's component based workflow. Follow this tutorial to get it set up: https://www.youtube.com/watch?v=aHFSDcEQuzQ.
// //credit omarVision for some of the SetRandomDestination() code, credit FlimcyVR for part of the findClosestPlayerTransform() code.
// public class NetworkedEnemyFollow : MonoBehaviour 
// {
    
//     [Header("Set the speed of the creature in the NavMeshAgent component.")]
//     [SerializeField] private float detectRange = 10f;

//     [Tooltip("This is the position between being in the safe area and the horror area. Go into code and fix > or < and x, y, or z direction")]
//     [SerializeField] private float divideLine = 15.5f;

//     [SerializeField] private string horrorFloorName;
//     private NavMeshAgent nma = null; //nma stands for navMeshAgent

//     private Bounds bndFloor;
//     private Vector3 moveto;
//     private bool flag = false;

//     private Transform target; //also the closest player

//     private GameObject[] players;
//     private float distanceToClosestPlayer;

//     //Everything regarding networking should be controlled by the MasterClient.
//     private void Start()
//     {
//         //this needs to be accessed by every player, not only the MasterClient.
//         nma = GetComponent<NavMeshAgent>();

//         //this gets the bounds of the horror floor
//         if (GameObject.Find(horrorFloorName) == null) {
//             Debug.LogError("You typed in the name of the horror floor wrong");
//         } else {
//             bndFloor = GameObject.Find(horrorFloorName).GetComponent<Renderer>().bounds;  
//         }


         
//     }

//     private void Update()
//     {
//         //the MasterClient (also called the host) is the first player in the room. The MasterClient will switch automatically if the current one leaves.
//         //you don't need MonoBehaviourPunCallbacks to access this anything from PhotonNetwork such as the PhotonNetwork.IsMasterClient.
//         if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected) { //The !PhotonNetwork.IsConnected part makes it so that Fluffy moves when the player is not connected to multiplayer.      

//             if (!nma.hasPath && !flag)
//             {
//                 flag = true;
//                 SetRandomDestination();
            
//             }
            
//             // this method sets distanceToClosestPlayer and target (target is the closest player)
//             findClosestPlayersTransform(); 

//             //If target is in the horror area and is in range to the enemy, go to the target player.
//             if (target != null) { //if the player for some reason disconnects from the server making the target destroyed, this code makes sure the target exists before trying to access the transform component preventing a nullreferenceexception.
//                 if (distanceToClosestPlayer < detectRange && target.position.z < divideLine) {  
//                     nma.SetDestination(target.position);
//                 } 
//             }
//             //  Debug.Log(target.position.z); //if you don't know what to put for the 15.5 above, you can uncomment this to find the value of the beginning of the horror area.

//         }
//     }
//     //sets target to closest player transform position, sets distanceToClosestPlayer to distance to closest player.
//     private void findClosestPlayersTransform() {
//         distanceToClosestPlayer = Mathf.Infinity;
//         players = GameObject.FindGameObjectsWithTag("Player"); //this gets all the gameObjects with a tag of `Player`. This tag is ONLY on the head of the photonVR player. If there is 3 players in the room, then the length of this array will be 3. The reason I put this on the head instead of the parent is because the parent always stays at Vector3(0, 0 ,0), NOT TRUE ANYMORE BC OF this transfomr thing in PhotonVRPlayer
//         foreach (GameObject p in players)
//         {
//             float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
//             if (distanceToPlayer < distanceToClosestPlayer)
//             {
//                 distanceToClosestPlayer = distanceToPlayer;
//                 target = p.transform;
//                 //Debug.Log(p.transform.position.z);
//             }
//         }
//     }
//     //SetRandomDestination picks a random x and z and sets the enemy's destination to that point.
//     private void SetRandomDestination()
//     {
//         float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
//         float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);
//         moveto = new Vector3(rx, transform.position.y, rz); 
//         nma.SetDestination(moveto); 

//         flag = false;
//     }
// }
