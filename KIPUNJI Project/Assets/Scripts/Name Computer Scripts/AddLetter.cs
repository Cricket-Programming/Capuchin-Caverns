using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetter : MonoBehaviour
{
    public NameScript nameScript;
    public string handTag;
    public string letter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == handTag) 
        {
            nameScript.NameVar += letter;
        }
    }
}
