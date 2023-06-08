using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] Transform[] waypoints; //camelcase
    [SerializeField] float speed;
    public float rotationSpeed = 5f;
    
    private int currentWaypointIndex = 0;
    private void Update() {
        MoveAlongTracks();
    }

    //target means the place you want to get to.
    void MoveAlongTracks() {
        //remember, vector3's are 3d coordinates in 3d space
        Vector3 targetVector3 = waypoints[currentWaypointIndex].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetVector3); //this finds the right quaternion rotation of the target coordinate.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //lerp moves the rotation in intervals and since Time.deltaTime increases, the thing slowly (smoothly rotates)
         
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < .1f) {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= waypoints.Length) {
            currentWaypointIndex = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);//MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta);

    }
    
}















