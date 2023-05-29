 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script has the object move between two other gameObject (wayPoints).
public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints; //gets the gameobject access propery attributes with . ex .position
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 1f;

    void Update()
    {
        //transform.position is the position of the thing moving (Ex: platform)
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)  //calculates distance between 2 game objects
        {
            currentWaypointIndex++; //it goes 0, 1 and then when reaches to goes back to one.
            if (currentWaypointIndex >= waypoints.Length) 
            {
                currentWaypointIndex = 0;
            } 
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime); //The time from last frame to current frame
    } 
}
