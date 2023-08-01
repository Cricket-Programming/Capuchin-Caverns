using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{

    public coinsScripts coinsScripts; //importing other scripts
    
    //public TextMeshPro text; //TextMeshPro3dObject
    private TMP_Text text;
    public string currencyName = "marbles"; //changed this to currecyName from name to prevent the new keyword hiding intending.

    void Start() {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = "You Have\n" + coinsScripts.coins + " " + currencyName;
    }
}