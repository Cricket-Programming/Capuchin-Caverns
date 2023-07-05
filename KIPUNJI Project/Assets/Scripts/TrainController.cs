using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class TrainController : MonoBehaviour
{
    [SerializeField] Transform[] waypoints; //[SerializeField] is a decorator just like [PunRPC]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient || PhotonNetwork.IsConnected == false) {
            if (waypoints.Length > 0)
            {
                MoveAlongTracks();
            }
        }

    }

    private  void MoveAlongTracks()
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
    private void IncrementWaypointIndex() {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; //0 1 2  0 1 2  0 1 2  0 1 2, this resets when currenWaypointIndex equals waypoints.Length
    }
}
















