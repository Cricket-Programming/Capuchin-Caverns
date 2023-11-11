using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
   [SerializeField] private GameObject objectDisable;
   
   private void OnTriggerEnter()
   {
        objectDisable.SetActive(false);
   }


}