using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script enables (shows) the object specified by the objectEnable variable when the player touches it.
public class Enable : MonoBehaviour
{
    [SerializeField] private GameObject objectEnable;
    private void OnTriggerEnter() { 
        objectEnable.SetActive(true); 
    } 
} 
