using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
public class Colorer : MonoBehaviour
{
public Color YourColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter()
    {
    Color myColour = YourColor;
PhotonVRManager.SetColour(myColour);
    
    
    }
}
