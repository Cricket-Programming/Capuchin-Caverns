using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public NameScript NameScript;

    void Start()
    {
        //Photon Username Saver
        PlayerPrefs.GetString("PhotonUsername");
        NameScript.NameVar = PlayerPrefs.GetString("PhotonUsername");
    }

    void Update()
    {
        //Photon Username Loader
        PlayerPrefs.SetString("PhotonUsername", NameScript.NameVar);
    }
}
    