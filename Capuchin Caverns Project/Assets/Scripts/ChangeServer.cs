using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;

public class ChangeServer : MonoBehaviour
{
    [SerializeField] private string AppID;
    [SerializeField] private string VoiceID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag"))
        {   
            PhotonVRManager.ChangeServers(AppID, VoiceID);
        }
    }
}
