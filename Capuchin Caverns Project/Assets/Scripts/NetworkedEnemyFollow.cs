using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using Photon.Pun; // classes such as the PhotonNetwork in this namespace are PascalCase.

// Networking should work, if there are photon view and photon transform view components set to the settings Fluffy Networked.
// This script works with navmesh's component based workflow. Follow this tutorial to get it set up: https://www.youtube.com/watch?v=aHFSDcEQuzQ.
// Credits to omarVision for some of the SetRandomDestination() code, credits to FlimcyVR for part of the findClosestPlayerTransform() code.

// Note: If there is a shadow thing to produce darkness in horror, that is the same as in that area disabling the directional light/ setting directional light to baked instead of realtime.
// Bug: If MasterClient disconnects, then Fluffy stops moving.

// These enums are for horror direction stuff.
public enum HorrorDirection {
    LessThan,
    GreaterThan,
}
public enum Axes {
    X,
    Y,
    Z
}
public class NetworkedEnemyFollow : MonoBehaviour // MonoBehaviour is the class this class inherits from, thereby giving it the functions such as Update(), OnCollisionEnter(), etc.
{
    
    [Header("Set the speed of the creature in the NavMeshAgent component.")]
    [SerializeField] private float detectRange = 10f;

    [Tooltip("This is the position between being in the safe area and the horror area. Go into code and fix x, y, or z direction.")]
    [SerializeField] private float divideLine = 15.5f;
    [SerializeField] private HorrorDirection directionToHorror;
    [SerializeField] private Axes directionAxis = Axes.Z;
    [SerializeField] private string horrorFloorName;
    [Tooltip("If you don't know what to put for the divideLine, you can uncomment this to Debug.Log the value of the beginning of the horror area.")]
    [SerializeField] private bool PrintPlayerPosForDivLineTesting;
    private NavMeshAgent nma; //nma stands for navMeshAgent

    private Bounds bndFloor;
    private bool flag = false;


    // The closest player
    private Transform target; 

    private GameObject[] players;
    private float distanceToClosestPlayer;

    // Everything regarding networking should be controlled by the MasterClient.
    private void Start()
    {
        // This needs to be accessed by every player, not only the MasterClient.
        nma = GetComponent<NavMeshAgent>();

        // Gets the bounds of the horror floor.
        if (GameObject.Find(horrorFloorName) == null) {
            Debug.LogError("You typed in the name of the horror floor wrong");
        } else {
            bndFloor = GameObject.Find(horrorFloorName).GetComponent<Renderer>().bounds;  
        }
    }

    private void Update()
    {
        // The MasterClient (also called the host) is the first player in the room. The MasterClient will switch automatically if the current one leaves.
        // MonoBehaviourPunCallbacks to access this anything from PhotonNetwork such as the PhotonNetwork.IsMasterClient.
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected) { //The !PhotonNetwork.IsConnected part makes it so that Fluffy moves when the player is not connected to multiplayer. NOTE: Fluffy won't chase player though.     

            if (!nma.hasPath && !flag)
            {
                flag = true;
                SetRandomDestination();
            
            }
            
            // This method sets distanceToClosestPlayer and target (target is the closest player)
            findClosestPlayersTransform(); 

            //If target is in the horror area and is in range to the enemy, go to the target player.
            if (target != null) { // If the player is not connected to multiplayer or the target gets destroyed, target will be null. Not having this will cause a NullReferenceException.
                if (TargetInRangeAndInHorror()) {  
                    nma.SetDestination(target.position);
                } 
            }
            if (PrintPlayerPosForDivLineTesting) {
                Debug.Log((directionAxis == Axes.X) ? target.position.x : (directionAxis == Axes.Y) ? target.position.y : target.position.z);
            }
        }

    }
    private bool TargetInRangeAndInHorror() {
        bool dirIsLessThan = directionToHorror == HorrorDirection.LessThan;
        bool dirIsGreaterThan = directionToHorror == HorrorDirection.GreaterThan;
        bool closestPlayerInDetectRange = distanceToClosestPlayer < detectRange;
        // Target not in range
        if (distanceToClosestPlayer > detectRange) {
            return false;
        }
        // C# Ternary operator - condition ? expressionIfTrue : expressionIfFalse;
        float targetPosition = (directionAxis == Axes.X) ? target.position.x : (directionAxis == Axes.Y) ? target.position.y : target.position.z;
        return ((dirIsLessThan && targetPosition < divideLine) || (dirIsGreaterThan && targetPosition > divideLine));
    }
    // Sets target to closest player transform position, sets distanceToClosestPlayer to distance to closest player.
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
            }

        }

    }
    // SetRandomDestination picks a random x and z and sets the enemy's destination to that point.
    private void SetRandomDestination()
    {
        float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
        float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);
        Vector3 moveto = new Vector3(rx, transform.position.y, rz); 
        nma.SetDestination(moveto); 

        flag = false;
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using UnityEngine.AI;
// using Photon.Pun; // classes such as the PhotonNetwork in this namespace are PascalCase.

// // Networking should work, if there are photon view and photon transform view components set to the settings Fluffy Networked.
// // This script works with navmesh's component based workflow. Follow this tutorial to get it set up: https://www.youtube.com/watch?v=aHFSDcEQuzQ.
// // Credits to omarVision for some of the SetRandomDestination() code, credits to FlimcyVR for part of the findClosestPlayerTransform() code.

// // Note: If there is a shadow thing to produce darkness in horror, that is the same as in that area disabling the directional light/ setting directional light to baked instead of realtime.
// // Bug: If MasterClient disconnects, then Fluffy stops moving.
// public enum HorrorDirection {
//     LessThan,
//     GreaterThan,

// }
// public class NetworkedEnemyFollow : MonoBehaviour // MonoBehaviour is the class this class inherits from, thereby giving it the functions such as Update(), OnCollisionEnter(), etc.
// {
    
//     [Header("Set the speed of the creature in the NavMeshAgent component.")]
//     [SerializeField] private float detectRange = 10f;

//     [Tooltip("This is the position between being in the safe area and the horror area. Go into code and fix > or < and x, y, or z direction.")]
//     [SerializeField] private float divideLine = 15.5f;
//     [SerializeField] private HorrorDirection directionToHorror;
//     [SerializeField] private string horrorFloorName;
//     [Tooltip("If you don't know what to put for the divideLine, you can uncomment this to Debug.Log the value of the beginning of the horror area.")]
//     [SerializeField] private bool PrintPlayerPosForDivLineTesting;
//     private NavMeshAgent nma = null; //nma stands for navMeshAgent

//     private Bounds bndFloor;
//     private Vector3 moveto;
//     private bool flag = false;


//     // The closest player
//     private Transform target; 

//     private GameObject[] players;
//     private float distanceToClosestPlayer;

//     // Everything regarding networking should be controlled by the MasterClient.
//     private void Start()
//     {
//         // This needs to be accessed by every player, not only the MasterClient.
//         nma = GetComponent<NavMeshAgent>();

//         // Gets the bounds of the horror floor.
//         if (GameObject.Find(horrorFloorName) == null) {
//             Debug.LogError("You typed in the name of the horror floor wrong");
//         } else {
//             bndFloor = GameObject.Find(horrorFloorName).GetComponent<Renderer>().bounds;  
//         }
//     }

//     private void Update()
//     {
//         // The MasterClient (also called the host) is the first player in the room. The MasterClient will switch automatically if the current one leaves.
//         // MonoBehaviourPunCallbacks to access this anything from PhotonNetwork such as the PhotonNetwork.IsMasterClient.
//         if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected) { //The !PhotonNetwork.IsConnected part makes it so that Fluffy moves when the player is not connected to multiplayer. NOTE: Fluffy won't chase player though.     

//             if (!nma.hasPath && !flag)
//             {
//                 flag = true;
//                 SetRandomDestination();
            
//             }
            
//             // This method sets distanceToClosestPlayer and target (target is the closest player)
//             findClosestPlayersTransform(); 

//             //If target is in the horror area and is in range to the enemy, go to the target player.
//             if (target != null) { // If the player is not connected to multiplayer or the target gets destroyed, this will cause a nullreferenceexception.
//                 if (TargetInRangeAndInHorror()) {  
//                     nma.SetDestination(target.position);
//                 } 
//             }
//             if (PrintPlayerPosForDivLineTesting) {
//                 Debug.Log(target.position.z);
//             }
//         }

//     }
//     private bool TargetInRangeAndInHorror() {
//         return (distanceToClosestPlayer < detectRange && 
//                     (directionToHorror == HorrorDirection.LessThan && target.position.z < divideLine) ||
//                     (directionToHorror == HorrorDirection.GreaterThan && target.position.z > divideLine)
//                     );
//     }
//     // Sets target to closest player transform position, sets distanceToClosestPlayer to distance to closest player.
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
//             }

//         }

//     }
//     // SetRandomDestination picks a random x and z and sets the enemy's destination to that point.
//     private void SetRandomDestination()
//     {
//         float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
//         float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);
//         moveto = new Vector3(rx, transform.position.y, rz); 
//         nma.SetDestination(moveto); 

//         flag = false;
//     }
// }
