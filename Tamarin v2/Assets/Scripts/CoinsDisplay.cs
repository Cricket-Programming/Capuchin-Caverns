using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{

    public coinsScripts coins; //importing other scripts
    
    //public TextMeshPro text; //TextMeshPro3dObject
    private TMP_Text text;
    public string name = "coins"; //name of the currenct

    void Start() {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = "You Have\n" + coins.coins + " " + name;
    }
}