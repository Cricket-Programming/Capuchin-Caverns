using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] Transform[] waypoints; //camelcase
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length > 0)
        {
            MoveAlongTracks();
        }
    }

    void MoveAlongTracks()
    {
        if (waypoints[currentWaypointIndex] != null) {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                IncrementWaypointIndex();
            }
                
        }
        else { //this means the space in the inspector is blandk missing a value or something.
            IncrementWaypointIndex();
        }
    }
    void IncrementWaypointIndex() {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; //0 1 2  0 1 2  0 1 2  0 1 2, this resets when currenWaypointIndex equals waypoints.Length
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TrainController : MonoBehaviour
// {
//     //[SerializeField] Transform[] waypoints; //camelcase
//     private GameObject[] waypoints;
//     [SerializeField] float speed;
//     [SerializeField] float rotationSpeed = 5f;
//     [Header("Set name of tag of all waypoint inside the code")]
//     private int currentWaypointIndex = 0;

//     private void Start() {
//         waypoints = GameObject.FindGameObjectsWithTag("Train Track Waypoint");
//     }
//     private void Update()
//     {
//         if (waypoints.Length > 0)
//         {
//             MoveAlongTracks();
//         }
//     }

//     void MoveAlongTracks()
//     {
//         Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;
//         Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        
//         transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
//         if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
//         {
//             currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
//         }
//     }

// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TrainController : MonoBehaviour
// {
//     //[SerializeField] Transform[] waypoints; //camelcase
//     [SerializeField] float speed;
//     [SerializeField] float rotationSpeed = 5f;
    
//     private int currentWaypointIndex = 0;

//     private void Update()
//     {
//         if (waypoints.Length > 0)
//         {
//             MoveAlongTracks();
//         }
//     }

//     void MoveAlongTracks()
//     {
//         Vector3 targetPosition = waypoints[currentWaypointIndex].position;
//         Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        
//         transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
//         if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
//         {
//             currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
//         }
//     }
// }



// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// // public class TrainController : MonoBehaviour
// // {
// //     [SerializeField] Transform[] waypoints; //camelcase
// //     [SerializeField] float speed;
// //     public float rotationSpeed = 5f;
    
// //     private int currentWaypointIndex = 0;
// //     private void Update() {
// //         if (waypoints.Length > 0) {
// //             MoveAlongTracks();
// //         }
        
// //     }

// //     //target means the place you want to get to.
// //     void MoveAlongTracks() {
// //         //remember, vector3's are 3d coordinates in 3d space
// //         Vector3 targetVector3 = waypoints[currentWaypointIndex].position - transform.position;
// //         Quaternion targetRotation = Quaternion.LookRotation(targetVector3); //this finds the right quaternion rotation of the target coordinate. (LookAt does same thing)
// //         transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //lerp moves the rotation in specified intervals. I use Time.deltaTime becausewhen it increases, the object rotates gradually.
         
// //         if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < .1f) {
// //             currentWaypointIndex++;
// //         }

// //         if (currentWaypointIndex >= waypoints.Length) {
// //             currentWaypointIndex = 0;
// //         }
// //         transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);//MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta);

// //     }
    
// // }













