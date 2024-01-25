using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script disables (hides) the object specified by the objectDisable variable when the player touches it.
public class Disable : MonoBehaviour
{
   [SerializeField] private GameObject objectDisable;
   
   private void OnTriggerEnter()
   {
        objectDisable.SetActive(false);
   }
}