using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackspaceScript : MonoBehaviour
{
    public NameScript NameScript;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "HandTag" && NameScript.NameVar.Length > 0) //the last condition makes sure the backspace does not backspace nothing ad generate an error
        {
            NameScript.NameVar = NameScript.NameVar.Remove(NameScript.NameVar.Length - 1);
        }
    }
}
