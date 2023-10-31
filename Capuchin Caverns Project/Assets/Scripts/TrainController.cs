using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime; //to access Player Object type

//this script needs to be on the same gameobject as the one with the photonview and PhotonTransformView.
public class TrainController : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] waypoints; //[SerializeField] is a decorator just like [PunRPC]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 5f;

    private Vector3 respawnLocation;
    private bool isNewMasterClient = false;

    private void Start() {
        respawnLocation = transform.position;
    }
    private int currentWaypointIndex = 0;

    //this is so that the train does not all restart and become positionally messed up. These isNewMasterClient stuff are to prevent the bug of the train pieces individually heading to first waypoint and colliding when the masterclient switches.
    public override void OnMasterClientSwitched(Player newMasterClient) {
        if (newMasterClient == PhotonNetwork.LocalPlayer) {
            transform.position = respawnLocation;
            isNewMasterClient = false;   
            currentWaypointIndex = 0; 
        }
    }

    private void Update()
    {

        //transform.position = respawnLocation;
        if ((PhotonNetwork.IsMasterClient || PhotonNetwork.IsConnected == false) && !isNewMasterClient) {
            if (waypoints.Length > 0)
            {
                MoveAlongTracks();
            }
        }

    }

    private void MoveAlongTracks()
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
        else { //this means the space in the inspector is blank missing a value or something.
            IncrementWaypointIndex();
        }
    }
    private void NotIsNewMasterClient() {
        isNewMasterClient = false;
    }
    private void IncrementWaypointIndex() {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; //0 1 2  0 1 2  0 1 2  0 1 2, this resets when currenWaypointIndex equals waypoints.Length
    }
}
















