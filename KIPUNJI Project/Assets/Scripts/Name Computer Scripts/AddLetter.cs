using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetter : MonoBehaviour
{
    public NameScript nameScript;
    public string letter;

    private void OnTriggerEnter(Collider other)
    {
        //HandTag is on Left and Right hand controllers.
        if (other.transform.tag == "HandTag") 
        {
            nameScript.NameVar += letter;
        }
    }
}
