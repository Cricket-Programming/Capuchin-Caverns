using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] Transform[] waypoints; //camelcase
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 5f;
    
    private int currentWaypointIndex = 0;

    private void Update() {
        if (waypoints.Length > 0) {
            MoveAlongTracks();
        }
        
    }

    //target means the place you want to get to.
    void MoveAlongTracks() {
        //remember, vector3's are 3d coordinates in 3d space
        //targetPosition variables prevents us having to access the array twice.
        Vector3 targetPosition = waypoints[currentWaypointIndex].position; //the currentWaypoint's position. This is the target position meaning where the train wants to go, 
        
        Vector3 targetVector3 = waypoints[currentWaypointIndex].position - transform.position; //this finds the right quaternion rotation to look at the target coordinate. (LookAt does same thing);
        Quaternion targetRotation = Quaternion.LookRotation(targetVector3); // remember vectors can represent both directions (which is the case here I think) and points in 3D space, depending on how they are used.
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //lerp moves the rotation in specified intervals. I use Time.deltaTime becausewhen it increases, the object rotates gradually.
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);//MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta);
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; //If there are 3 waypoints: 0 1 2  0 1 2  0 1 2
            //the line with the modulus saves having to check if the waypoint index hits the length; it combines reseting and incrementing into 1 step.
        }


    }
    
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TrainController : MonoBehaviour
// {
//     [SerializeField] Transform[] waypoints; //camelcase
//     [SerializeField] float speed;
//     public float rotationSpeed = 5f;
    
//     private int currentWaypointIndex = 0;
//     private void Update() {
//         if (waypoints.Length > 0) {
//             MoveAlongTracks();
//         }
        
//     }

//     //target means the place you want to get to.
//     void MoveAlongTracks() {
//         //remember, vector3's are 3d coordinates in 3d space
//         Vector3 targetVector3 = waypoints[currentWaypointIndex].position - transform.position;
//         Quaternion targetRotation = Quaternion.LookRotation(targetVector3); //this finds the right quaternion rotation of the target coordinate. (LookAt does same thing)
//         transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //lerp moves the rotation in specified intervals. I use Time.deltaTime becausewhen it increases, the object rotates gradually.
         
//         if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < .1f) {
//             currentWaypointIndex++;
//         }

//         if (currentWaypointIndex >= waypoints.Length) {
//             currentWaypointIndex = 0;
//         }
//         transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);//MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta);

//     }
    
// }













