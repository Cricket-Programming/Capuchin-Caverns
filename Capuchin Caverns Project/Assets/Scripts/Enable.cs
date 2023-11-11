using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : MonoBehaviour
{
    [SerializeField] private GameObject objectEnable;

    private void OnTriggerEnter()
    { 

        objectEnable.SetActive(true); 
    } 



} 
