using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

public class Colorer : MonoBehaviour
{
    [Header("Choose between setting a custom color \nor using the mesh renderer's color (default).")]
    [SerializeField] private bool useCustomColor;
    [SerializeField] private Color customColor;
    
    private Color setColor;

    private void Start() {
        if (useCustomColor) {
            setColor = customColor;
        }
        else {
            setColor = GetComponent<MeshRenderer>().material.color;
        }
    }
    
    private void OnTriggerEnter() {
        PhotonVRManager.SetColour(setColor);
    }  
}
