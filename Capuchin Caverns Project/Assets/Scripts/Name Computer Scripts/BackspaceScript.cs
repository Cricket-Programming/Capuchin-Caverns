using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackspaceScript : MonoBehaviour
{
    public NameScript NameScript;
    
    private void OnTriggerEnter(Collider other)
    {
        int nameLength = NameScript.NameVar.Length;
        
        if (nameLength > 0 && other.transform.tag == "HandTag") {
            NameScript.NameVar = NameScript.NameVar.Remove(nameLength - 1);
        }
    }
}
