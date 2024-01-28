using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System; // For DateTime class
// These namespaces are for the Subtract/AddUserVirtualCurrency request stuff.
using PlayFab; // for PlayFabError type
using PlayFab.ClientModels; // for ModifyUserVirtualCurrencyResult type

// To view the old script that synchronized currency using playerprefs, go to the coinsmanager.cs.

// THERE SHOULD ONLY BE ONE CURRENCYMANAGER IN THE SCENE!
// CurrencyManager adds 100 marbles to PlayFab for every day that the player logs in.
// CurrencyManager also provides AddPlayFabCurrency() and SubtractPlayFabCurrency() methods used in multiple other scripts.
public class CurrencyManager : MonoBehaviour
{
    [HideInInspector] public static CurrencyManager Instance;
    [HideInInspector] public int coins; // The player's coins. Accessed in Purchase.cs from singleton pattern

    [Header("Set the starting coins in PlayFab itself in Engage > economy > Currency (legacy) > Click on Marbles Display Name > Change Initial Deposit")]
    [SerializeField] private int howMuchADay = 100;
    [SerializeField] private string currencyName;
    [SerializeField] private TextMeshPro currencyText;
    private void Awake() {
        Instance = this;
    }
    private string todayDate;

    // Invoked after the player gets logged in by the PlayFabLogin script.
    public void SetUpDailyRewardsData() 
    {
        todayDate = DateTime.Today.ToBinary().ToString();
        if (PlayerPrefs.GetInt("existingUser") == 0)
        {
            // If is a new user, set the date.
            PlayerPrefs.SetString("previousDate", todayDate);  

            PlayerPrefs.SetInt("existingUser", 1); 
        }
        GetVirtualCurrencies();
        UpdateDailyRewards(); 
    }

    private void UpdateDailyRewards()
    {
        if (PlayerPrefs.GetString("previousDate") != todayDate)
        {
            // Give player currency
            AddPlayFabCurrency(howMuchADay);
            PlayerPrefs.SetString("previousDate", todayDate);
        }
    }

    private void GetVirtualCurrencies() {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnGetCurrencyError);
    }
    private void OnGetUserInventorySuccess(GetUserInventoryResult result) {
        coins = result.VirtualCurrency["HS"];
        currencyText.text = "You have " + coins.ToString() + " " + currencyName;
    }
    private void OnGetCurrencyError(PlayFabError error) {
        Debug.Log("Could not get the PlayFab User inventory" + error);
    }



    public void AddPlayFabCurrency(int amount) {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HS",
            Amount = amount       
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnAddCurrencyFailure);
    }
    private void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result) {
        Debug.Log("Currency added: " + result.Balance);
        GetVirtualCurrencies();
    }
    private void OnAddCurrencyFailure(PlayFabError error) {
        Debug.LogError("Failed to add currency: " + error.ErrorMessage);
    }

    public void SubtractPlayFabCurrency(int amount) {
        var request = new SubtractUserVirtualCurrencyRequest {
            VirtualCurrency = "HS",
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCurrencySuccess, OnSubtractCurrencyFailure);
    } 
    private void OnSubtractCurrencySuccess(ModifyUserVirtualCurrencyResult result) {
        Debug.Log("Currency subtracted: " + result.Balance);
        GetVirtualCurrencies(); // This refreshes it so the user can see the decrease in currency right away.
    }
    private void OnSubtractCurrencyFailure(PlayFabError error) {
        Debug.Log("Error Subtracting Virtual Currency: " + error.ErrorMessage);
    }

}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using System; //for DateTime
// using PlayFab; //for PlayFabError type
// using PlayFab.ClientModels; //for ModifyUserVirtualCurrencyResult type

// //to view the old script that syncronizes currency using playerprefs, go to the coinsmanager.cs

// //this script adds 100 marbles to PlayFab for every day that the player logs in.
// public class CurrencyManager : MonoBehaviour
// {
//     [Header("Set the starting coins in PlayFab itself in Engage > economy > currency (legacy) > Click on Marbles Display Name > Change Initial Deposit")]
//     [SerializeField] private int HowMuchADay = 100;

//     private string todayDate;

//     //called after player logs in in the PlayFabLogin script.
//     public void SetUpDailyRewardsData() 
//     {
//         todayDate = DateTime.Today.ToBinary().ToString();
//         if (PlayerPrefs.GetInt("existingUser") == 0)
//         {
//             //if new user, set the date
//             PlayerPrefs.SetString("previousDate", todayDate);  

//             PlayerPrefs.SetInt("existingUser", 1); 
//         }
        
//         UpdateDailyRewards();
//     }

//     private void UpdateDailyRewards()
//     {
//         if (PlayerPrefs.GetString("previousDate") != todayDate)
//         {
//             //give player currency
//             AddPlayFabCurrency();
//             PlayerPrefs.SetString("previousDate", todayDate);
//         }
//     }

//     private void AddPlayFabCurrency() {
//         var request = new AddUserVirtualCurrencyRequest
//         {
//             Amount = HowMuchADay,
//             VirtualCurrency = "HS"
//         };
//         PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnAddCurrencyFailure);
//     }

//     private void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
//     {
//         Debug.Log("Currency added: " + result.Balance);
//         PlayFabLogin.instance.GetVirtualCurrencies();
//     }

//     private void OnAddCurrencyFailure(PlayFabError error)
//     {
//         Debug.LogError("Failed to add currency: " + error.ErrorMessage);
//     }


// }

