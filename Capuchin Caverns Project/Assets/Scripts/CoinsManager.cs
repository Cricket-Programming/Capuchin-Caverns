using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinsManager : MonoBehaviour
{
    private int coins;

    [SerializeField] private int HowMuchADay = 100;
    [SerializeField] private int startingCoins = 800;
    private string todayDate;

    //accessor method for the coins instance variable. (It will be accessed in CoinsDisplay.cs)
    public int GetCoins() {
        return coins;
    }
    private void Start() 
    {
        todayDate = DateTime.Today.ToBinary().ToString();
        if (PlayerPrefs.GetInt("existingUsewr") == 0)
        {
            PlayerPrefs.SetInt("coins", startingCoins);

            //if new user,set the date
            PlayerPrefs.SetString("previousDate", todayDate);  

            PlayerPrefs.SetInt("existingUser", 1); 
        }
        
        UpdateDailyRewards();
    }

    private void UpdateDailyRewards()
    {
        if (PlayerPrefs.GetString("previousDate") != todayDate)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + HowMuchADay);
            print("Currency = " + coins);
            PlayerPrefs.SetString("previousDate", todayDate);
        }
    }

    private void Update()
    {
        coins = PlayerPrefs.GetInt("coins");
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System;

// public class coinsScripts : MonoBehaviour
// {
//     public int coins;
//     public int HowMuchADay = 100;
//     public void Start() 
//     {
//         if (PlayerPrefs.GetInt("coin") == 0)
//         {
//             PlayerPrefs.SetInt("coins", 500); 
//             PlayerPrefs.SetInt("coin", 1);
//         }
//         time();
//     }

//     public void time()
//     {
//         if (PlayerPrefs.GetInt("coin") == 1)
//         {
//             PlayerPrefs.SetString("a", DateTime.Today.ToBinary().ToString());
//             PlayerPrefs.SetInt("coin", 2);
//         }
//         if (PlayerPrefs.GetString("a") != DateTime.Today.ToBinary().ToString())
//         {
//             PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + HowMuchADay);
//             print("Currency = " + coins);
//             PlayerPrefs.SetInt("coin", 1);
//         }
//         //print("" + PlayerPrefs.GetString("a"));
//     }

//     public void Update()
//     {
//         coins = PlayerPrefs.GetInt("coins");
//     }
// }