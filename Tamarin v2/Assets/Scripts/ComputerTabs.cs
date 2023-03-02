using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTabs : MonoBehaviour
{
    public List<GameObject> OldTabs = new List<GameObject>();
public GameObject NewTab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(){
            NewTab.SetActive(true);
            foreach (var obj in OldTabs)
                obj.SetActive(false);



    }
}