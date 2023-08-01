using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BlockConnectionStatus : MonoBehaviour   
{

    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
 
        if (PhotonNetwork.IsConnected)
        {
            // Player is connected to a Photon server
            objectRenderer.material.color = Color.green;
        }
        else
        {
            // Player is not connected to a Photon server
            objectRenderer.material.color = Color.red;
        }
    }


}
