using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.VR;

public class SliderScript : MonoBehaviour
{
    public Slider sliderRed;
    public Slider sliderGreen;
    public Slider sliderBlue;
    void Update()
    {
        Color myColour = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);
        PhotonVRManager.SetColour(myColour);
    }
}
