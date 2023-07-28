using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetter : MonoBehaviour
{
    public NameScript NameScript;

    [Tooltip("Check this if you want to turn the letter into the name of this gameobject and disregard the public letter blank below.")]
    public bool overrideLetterWithGameObjectName;
    public string Letter;

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
            NameScript.NameVar += Letter;
        }
    }
}
