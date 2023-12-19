using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; //for DateTime
//these namespaces are for the SubtractAndAddUserVirtualCurrencyRequest.
using PlayFab; //for PlayFabError type
using PlayFab.ClientModels; //for ModifyUserVirtualCurrencyResult type

//to view the old script that syncronizes currency using playerprefs, go to the coinsmanager.cs

//THERE SHOULD ONLY BE ONE CURRENCYMANAGER IN THE SCENE
//CurrencyManager adds 100 marbles to PlayFab for every day that the player logs in.
//CurrencyManager   also provides a lot of methods for adding and subtracting currency from PlayFab.
public class CurrencyManager : MonoBehaviour
{
    [Header("Set the starting coins in PlayFab itself in Engage > economy > currency (legacy) > Click on Marbles Display Name > Change Initial Deposit")]
    [SerializeField] private int HowMuchADay = 100;
    
    private string todayDate;

    // Invoked after the player gets logged in by the PlayFabLogin script.
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
            AddPlayFabCurrency(HowMuchADay);
            PlayerPrefs.SetString("previousDate", todayDate);
        }
    }

    public static void AddPlayFabCurrency(int amount) {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HS",
            Amount = amount       
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnAddCurrencyFailure);
    }

    private static void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result) {
        Debug.Log("Currency added: " + result.Balance);
        PlayFabLogin.instance.GetVirtualCurrencies();
    }

    private static void OnAddCurrencyFailure(PlayFabError error) {
        Debug.LogError("Failed to add currency: " + error.ErrorMessage);
    }

    public static void SubtractPlayFabCurrency(int amount) {
        var request = new SubtractUserVirtualCurrencyRequest {
            VirtualCurrency = "HS",
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCurrencySuccess, OnSubtractCurrencyFailure);
    } 
    private static void OnSubtractCurrencySuccess(ModifyUserVirtualCurrencyResult result) {
        Debug.Log("Currency subtracted: " + result.Balance);
        PlayFabLogin.instance.GetVirtualCurrencies(); //this refreshes it so the user can see the decrease in currency right away.
    }
    private static void OnSubtractCurrencyFailure(PlayFabError error) {
        Debug.Log("Error Subtracting Virtual Currency: " + error.ErrorMessage);
    }

}

