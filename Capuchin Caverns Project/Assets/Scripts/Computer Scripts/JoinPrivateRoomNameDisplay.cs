using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoinPrivateRoomNameDisplay : MonoBehaviour, IAddLetterable //interfaces define behaviors that the script has, meaning this script has the ability to add letters to it.
{

    private TMP_Text roomNameText;
    private void Start() {
        roomNameText = GetComponent<TMP_Text>();
    }
    //Invoked by AddLetter() script through IAddLetterable interface.
    public void AddLetter(string letter) {
        if (roomNameText.text.Equals("_"))
            Backspace();
        roomNameText.text += letter;
    }

    public void Backspace() {
        int nameLength = roomNameText.text.Length;
        if (nameLength > 0) 
            roomNameText.text = roomNameText.text.Remove(nameLength - 1);
    }
    //accessed by the JoinPrivateRoom class.
    public string GetRoomName() {
        return roomNameText.text;
    }
}
