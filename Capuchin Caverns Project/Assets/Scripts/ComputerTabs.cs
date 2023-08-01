using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTabs : MonoBehaviour
{
    public List<GameObject> OldTabs = new List<GameObject>();
    public GameObject NewTab;
    private void OnTriggerEnter(){
        foreach (var obj in OldTabs) {
            obj.SetActive(false);
        }  
        NewTab.SetActive(true);


    }
}