using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{

    public coinsScripts coins;
    //if this was a textmesh pro 
    public TextMeshPro text;
    public string name = "coins";

    void Update()
    {
        text.text = "You Have\n" + coins.coins + " " + name;
    }
}