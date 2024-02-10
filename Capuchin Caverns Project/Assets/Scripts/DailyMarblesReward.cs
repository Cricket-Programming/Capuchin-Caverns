using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// When the user presses this object, they will get 100 Marbles. They can press the button only once a day, else the button will destroy itself.
public class DailyMarblesReward : MonoBehaviour
{
    [Tooltip("IE: Claimed!")]
    [SerializeField] private GameObject showAfter;
    [SerializeField] private int howMuchADay = 100;     
    private string todayDate;
    private void Start() {
        todayDate = DateTime.Today.ToString("yyyy-MM-dd");
        if (PlayerPrefs.GetInt("existingUser") == 0)
        {
            // If is a new user, set the date.
            PlayerPrefs.SetString("previousDate", todayDate);  

            PlayerPrefs.SetInt("existingUser", 1); 
        }

        // It's the same day so deactivate the button.
        if (PlayerPrefs.GetString("previousDate").Equals(todayDate)) {
            RemovePurchaseButton();
        }
    }
    private void Update() {


    }

    // Invoked after the player gets logged in by the PlayFabLogin script.
    // public void SetUpDailyRewardsData() 
    // {

    //     GetVirtualCurrencies();
    //     UpdateDailyRewards(); 
    // }
    private void OnTriggerEnter() {
        // Give player currency
        CurrencyManager.Instance.AddPlayFabCurrency(howMuchADay);
        PlayerPrefs.SetString("previousDate", todayDate);
        RemovePurchaseButton();
    }
    private void RemovePurchaseButton() {
        showAfter?.SetActive(true);
        Destroy(gameObject);
    }
}

