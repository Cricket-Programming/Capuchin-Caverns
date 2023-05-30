using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 1f;
    [SerializeField] float waitTime = 2f; // Delay time at each waypoint
    float timer = 0f; // Timer for tracking the delay

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f) //this means the elevator reaches the waypoint
        {
            // Stop and wait at the current waypoint
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                timer = 0f; // Reset the timer
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);

    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //this script has the object move between two other gameObject (wayPoints).
// public class WaypointFollower : MonoBehaviour
// {
//     [SerializeField] GameObject[] waypoints; //gets the gameobject access propery attributes with . ex .position
//     int currentWaypointIndex = 0;

//     [SerializeField] float speed = 1f;

//     void Update()
//     {
//         //transform.position is the position of the thing moving (Ex: platform)
//         if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)  //calculates distance between 2 game objects
//         {
//             //yield return new WaitForSeconds(2f);
//             currentWaypointIndex++; //it goes 0, 1 and then when reaches to goes back to one.
//             if (currentWaypointIndex >= waypoints.Length) 
//             {
//                 currentWaypointIndex = 0;

//                 //Invoke("MoveToFirstWaypoint", 3.0f);
//             } 
//         }
//         if (currentWaypointIndex < waypoints.Length) {
//             transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime); //The time from last frame to current frame
//         }

 
//     } 


// }

//  using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //this script has the object move between two other gameObject (wayPoints).
// public class WaypointFollower : MonoBehaviour
// {
//     [SerializeField] GameObject[] waypoints; //gets the gameobject access propery attributes with . ex .position
//     int currentWaypointIndex = 0;

//     [SerializeField] float speed = 1f;

//     void Update()
//     {
//         StartCoroutine()
//         //transform.position is the position of the thing moving (Ex: platform)
//         if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)  //calculates distance between 2 game objects
//         {
//             currentWaypointIndex++; //it goes 0, 1 and then when reaches to goes back to one.
//             if (currentWaypointIndex >= waypoints.Length) 
//             {
//                 Invoke("MoveToFirstWaypoint", 3.0f);
//             } 
//         }
//         if (currentWaypointIndex < waypoints.Length) {
//             transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime); //The time from last frame to current frame
//         }
 
//     } 
//     void MoveToFirstWaypoint() {
//         currentWaypointIndex = 0;
//     }
// }
