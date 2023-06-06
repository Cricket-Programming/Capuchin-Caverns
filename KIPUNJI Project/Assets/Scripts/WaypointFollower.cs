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
