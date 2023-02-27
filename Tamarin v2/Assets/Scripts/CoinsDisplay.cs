using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public coinsScripts coins;
    //if this was a textmesh pro 
    public TextMeshPro text;
    public string name = "coins";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "You Have " + coins.coins + " " + name;
    }
}