using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; //for DateTime
using PlayFab; //for PlayFabError type
using PlayFab.ClientModels; //for ModifyUserVirtualCurrencyResult type

//to view the old script that syncronizes currency using playerprefs, go to the coinsmanager.cs

//this script adds 100 marbles to PlayFab for every day that the player logs in.
public class CurrencyManager : MonoBehaviour
{
    [Header("Set the starting coins in PlayFab itself in Engage > economy > currency (legacy) > Click on Marbles Display Name > Change Initial Deposit")]
    [SerializeField] private int HowMuchADay = 100;

    private string todayDate;

    //called after player logs in in the PlayFabLogin script.
    public void SetUpDailyRewardsData() 
    {
        todayDate = DateTime.Today.ToBinary().ToString();
        if (PlayerPrefs.GetInt("existingUser") == 0)
        {
            //if new user, set the date
            PlayerPrefs.SetString("previousDate", todayDate);  

            PlayerPrefs.SetInt("existingUser", 1); 
        }
        
        UpdateDailyRewards();
    }

    private void UpdateDailyRewards()
    {
        if (PlayerPrefs.GetString("previousDate") != todayDate)
        {
            //give player currency
            AddPlayFabCurrency();
            PlayerPrefs.SetString("previousDate", todayDate);
        }
    }

    private void AddPlayFabCurrency() {
        var request = new AddUserVirtualCurrencyRequest
        {
            Amount = HowMuchADay,
            VirtualCurrency = "HS"
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnAddCurrencyFailure);
    }

    private void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Currency added: " + result.Balance);
        PlayFabLogin.instance.GetVirtualCurrencies();
    }

    private void OnAddCurrencyFailure(PlayFabError error)
    {
        Debug.LogError("Failed to add currency: " + error.ErrorMessage);
    }


}

