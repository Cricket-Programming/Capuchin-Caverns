using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
using TMPro;
public class NameScript : MonoBehaviour
{
    [HideInInspector] public string NameVar;
    [SerializeField] private TextMeshPro nameText;
    private void Start() {
        NameVar = PlayerPrefs.GetString("PlayerUsername");
        if (NameVar == "") SetDefaultName();
    }
    private void Update() {   
        nameText.text = NameVar;
    
        PlayerPrefs.SetString("PlayerUsername", NameVar);
        PhotonVRManager.SetUsername(NameVar);
    }
    public void SetDefaultName() {
        NameVar = "Monkey" + Random.Range(100, 1000);
    }
}
