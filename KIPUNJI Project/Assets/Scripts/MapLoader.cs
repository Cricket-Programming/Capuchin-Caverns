using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public GameObject newmap;
    public GameObject oldmap;
    //SET THE OBJECT THIS IS ON TO A NEW LAYER

    void OnTriggerEnter() 
    {
        newmap.SetActive(true);
        oldmap.SetActive(false);
        
    }
}
