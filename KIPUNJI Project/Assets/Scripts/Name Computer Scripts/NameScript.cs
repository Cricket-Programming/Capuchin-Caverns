using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.VR;
using TMPro;
public class NameScript : MonoBehaviour
{
    public string NameVar;
    public TextMeshPro NameText;
    void Start() {
        if (NameVar.Length == 0)
        {
            NameVar = "Monkey" + Random.Range(100, 1000);
        }
    }
    private void Update()
    {   
        if (NameVar.Length > 12)
        {
            NameVar = NameVar.Substring(0, 12);
        }
        NameText.text = NameVar;
        PhotonVRManager.SetUsername(NameVar);
    }
}
