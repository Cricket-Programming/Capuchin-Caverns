using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
public class Colorer : MonoBehaviour
{
    public Color YourColor;
    void OnTriggerEnter() {
        Color myColour = YourColor;
        //PhotonVRManager.SetColour(myColour);
    }
}
