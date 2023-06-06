using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWaypointFollower : MonoBehaviour
{
    private GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 1f;

    void Start() {
        waypoints = GameObject.FindGameObjectsWithTag("TrainTrackEnd");
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f) //this means the elevator reaches the waypoint
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);

    }
}
