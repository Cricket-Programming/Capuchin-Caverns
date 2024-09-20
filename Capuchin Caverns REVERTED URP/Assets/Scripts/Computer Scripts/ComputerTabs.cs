using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTabs : MonoBehaviour
{
    [SerializeField] private List<GameObject> OldTabs = new List<GameObject>();
    [SerializeField] private GameObject NewTab;
    private void OnTriggerEnter(){
        foreach (GameObject obj in OldTabs) {
            obj.SetActive(false);
        }  
        NewTab.SetActive(true);
    }
}