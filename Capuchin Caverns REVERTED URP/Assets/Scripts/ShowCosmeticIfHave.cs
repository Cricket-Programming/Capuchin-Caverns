using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script will set the cosmetic stand as active only if the player owns the cosmetic/skin.
// This is mainly for players to put on seasonal cosmetics.
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
