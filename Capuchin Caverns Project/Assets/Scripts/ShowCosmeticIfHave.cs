using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will set the cosmetic stand as active only if the player owns the cosmetic.
//this is so that players can put on their seasonal cosmetics.
public class ShowCosmeticIfHave : MonoBehaviour
{

    [SerializeField] private string CosmeticName;
    private void Start()
    {
        if (PlayerPrefs.GetInt(CosmeticName) == 1) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
        
    }


}
