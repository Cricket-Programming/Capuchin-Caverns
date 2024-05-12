using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This means that all the scripts that implement IAddLetterable must have the AddLetter() and Backspace() methods.
public interface IAddLetterable {
    void AddLetter(string letter);
    void Backspace();
}

public class AddLetter : MonoBehaviour
{
    [SerializeField] private GameObject addLetterableScript; 

    [Tooltip("Check this if you want to turn the letter into the name of this gameobject and disregard the public letter blank below.")]
    [SerializeField] private bool overrideLetterWithGameObjectName;
    [Tooltip("Put -1 in this blank for backspace")]
    [SerializeField] private string letter;

    private void Start()
    {
        if (overrideLetterWithGameObjectName)
        {
            letter = gameObject.name;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "HandTag")
        {
            // Check if the assigned script implements the ILetterReceiver interface
            var addLetterable = addLetterableScript.GetComponent<IAddLetterable>();    
            // Guard clauses - simplifies unnecessarily complicated nested control flow     
            if (addLetterable == null)
            {
                Debug.LogError("The assigned script does not implement IAddLetterable interface.");
                return;
            }

            if (letter.Equals("-1"))  {
                addLetterable.Backspace();
                return;
            }
            addLetterable.AddLetter(letter);
        }
    }
}

