using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetter : MonoBehaviour
{
    [SerializeField] private NameScript NameScript;

    [Tooltip("Check this if you want to turn the letter into the name of this gameobject and disregard the public letter blank below.")]
    [SerializeField] private bool overrideLetterWithGameObjectName;
    [SerializeField] private string Letter;

    private void Start() {
        if (overrideLetterWithGameObjectName) {
            Letter = gameObject.name;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //HandTag is on Left and Right hand controllers.
        if (other.transform.tag == "HandTag") 
        {
            if (NameScript.NameVar.Length < 12) {
                NameScript.NameVar += Letter;
            } else {
                NameScript.NameVar = NameScript.NameVar.Substring(0, 12);
            }
        }
            
    }
}

