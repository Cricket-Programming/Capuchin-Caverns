using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class RewardMarbles : MonoBehaviour
{
    [Tooltip("IE: Claimed!")]
    [SerializeField] private GameObject enable;
    [SerializeField] private string rewardName;
    [SerializeField] private int increaseAmount;
    private void Start() {
        if (PlayerPrefs.GetInt(rewardName) == 1) {
            ActivateCosmeticObjects();
        }
    }
    private void OnTriggerEnter() {
        CurrencyManager.Instance.AddPlayFabCurrency(increaseAmount);
        PlayerPrefs.SetInt(rewardName, 1);
        ActivateCosmeticObjects();
    }

    private void ActivateCosmeticObjects() {
        enable?.SetActive(true);
        gameObject.SetActive(false); //same as this.gameObject
    }

}
