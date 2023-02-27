using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
public NameScript NameScript;
    // Start is called before the first frame update
    void Start()
    {

    //Photon Username Saver
       PlayerPrefs.GetString("PhotonUsername");
       NameScript.NameVar = PlayerPrefs.GetString("PhotonUsername");
    }

    // Update is called once per frame
    void Update()
    {
    //Photon Username Loader
               PlayerPrefs.SetString("PhotonUsername", NameScript.NameVar);
    }
}
