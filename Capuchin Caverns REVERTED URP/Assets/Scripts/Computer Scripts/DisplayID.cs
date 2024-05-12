using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayID : MonoBehaviour
{
    private void Start() {
        TextMeshPro IDText = GetComponent<TextMeshPro>();
        
        if (PlayFabLogin.instance != null) {
            string playFabID = "dd";//PlayFabLogin.instance.MyPlayFabID;
            IDText.text = playFabID;
        } else {
            Debug.LogError("PlayFabLogin instance is null. Can't access it.");
        }

    }
    private void Update() {
        Debug.Log(PlayFabLogin.instance.MyPlayFabID);
    }
}
