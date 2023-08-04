using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{

    public coinsScripts coinsScripts; //importing other scripts
    
    private TMP_Text textComponent;
    [SerializeField] private string currencyName = "marbles"; //changed this to currecyName from name to prevent the new keyword hiding intending.

    private void Start() {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        textComponent.text = "You Have\n" + coinsScripts.coins + " " + currencyName;
    }
}