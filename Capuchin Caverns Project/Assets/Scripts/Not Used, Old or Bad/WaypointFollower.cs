using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitTime = 2f; // Delay time at each waypoint

    private int currentWaypointIndex = 0;
    private float timer = 0f; // Timer for tracking the delay

    private void Start() {
        if (waypoints == null || waypoints.Length == 0) {
            for (int i = 0; i < 1000; i++) {
                Debug.LogWarning("No waypoints assigned to WaypointFollower script.");
            }
            
        }
    }
    private void Update()
    {               
        Vector3 waypointPosition = waypoints[currentWaypointIndex].transform.position; // example of a local variable.
        if (Vector3.Distance(transform.position, waypointPosition) < 0.1f) //this means the elevator reaches the waypoint
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

        transform.position = Vector3.MoveTowards(transform.position, waypointPosition, speed * Time.deltaTime);

    }
}
