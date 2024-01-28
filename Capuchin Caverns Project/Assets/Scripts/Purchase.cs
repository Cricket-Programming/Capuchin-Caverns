using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Purchase : MonoBehaviour
{
    [SerializeField] private GameObject enable;
    [SerializeField] private GameObject disable;
    [SerializeField] private string CosmeticName;
    [SerializeField] private int price;

    private void Start()
    {
        if (PlayerPrefs.GetInt(CosmeticName) == 1)
        {
            ActivateCosmeticObjects();
        }
    }

    private void OnTriggerEnter()
    {
        //Purchase cosmetic.
        int marbles = CurrencyManager.Instance.coins;
        if (marbles >= price)
        {
            CurrencyManager.Instance.SubtractPlayFabCurrency(price);
            PlayerPrefs.SetInt(CosmeticName, 1);
            ActivateCosmeticObjects();
        }
        
    }
    private void ActivateCosmeticObjects() {
        enable?.SetActive(true);
        disable?.SetActive(true);
        gameObject.SetActive(false); //same as this.gameObject
    }

}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// //these namespaces are for the SubtractUserVirtualCurrencyRequest
// using PlayFab;
// using PlayFab.ClientModels;

// public class Purchase : MonoBehaviour
// {
//     [SerializeField] private GameObject enable;
//     [SerializeField] private GameObject disable;
//     [SerializeField] private string CosmeticName;
//     [SerializeField] private int price;



//     private void Start()
//     {

//         if (PlayerPrefs.GetInt(CosmeticName) == 1)
//         {
//             enable.SetActive(true);
//             disable.SetActive(true);
//             gameObject.SetActive(false);
//         }
//     }

//     private void OnTriggerEnter()
//     {
//         //Purchase cosmetic.
//         int marbles = PlayFabLogin.instance.coins;
//         if (marbles >= price)
//         {
//             var request = new SubtractUserVirtualCurrencyRequest {
//                 VirtualCurrency = "HS",
//                 Amount = price
//             };
//             PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractSuccess, OnSubtractFailure);

//             PlayerPrefs.SetInt(CosmeticName, 1);


//             enable.SetActive(true);
//             disable.SetActive(true);
//             gameObject.SetActive(false); //same as this.gameObject


//         }
//     }

//     private void OnSubtractSuccess(ModifyUserVirtualCurrencyResult result) {
//         Debug.Log("Currency subtracted" + result.Balance);
//         PlayFabLogin.instance.GetVirtualCurrencies(); //this refreshes it so the user can see the decrease in currency right away.
//     }
//     private void OnSubtractFailure(PlayFabError error) {
//         Debug.Log("Error Subtracting Virtual Currency " + error.ErrorMessage);
//     }


// }




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Purchase : MonoBehaviour
// {
//     [SerializeField] private GameObject enable;
//     public GameObject disable;
//     public string CosmeticName;
//     public int price;

//     private void Start()
//     {
//         if (PlayerPrefs.GetInt(CosmeticName) == 1)
//         {
//             enable.SetActive(true);
//             disable.SetActive(true);
//             gameObject.SetActive(false);
//         }
//     }
    
//     private void OnTriggerEnter()
//     {
//         //Purchase cosmetic.
//         if (PlayerPrefs.GetInt("coins") >= price)
//         {
//             int s = PlayerPrefs.GetInt("coins");
//             s -= price;
//             PlayerPrefs.SetInt("coins", s);
            
//             enable.SetActive(true);
//             disable.SetActive(true);
//             gameObject.SetActive(false); //same as this.gameObject

//             PlayerPrefs.SetInt(CosmeticName, 1);
//         }
//     }


// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Purchase : MonoBehaviour
// {
//     public GameObject enable;
//     public GameObject disable;
//     public string CosmeticName;
//     public int price;

//     private void Start()
//     {
//         if (PlayerPrefs.GetInt(CosmeticName) == 1)
//         {
//             enable.SetActive(true);
//             disable.SetActive(true);
//             gameObject.SetActive(false);
//         }
//     }
    
//     private void OnTriggerEnter()
//     {
//         //Purchase cosmetic.
//         if (PlayerPrefs.GetInt("coins") >= price)
//         {
//             if (PlayerPrefs.GetInt(CosmeticName) != 1)
//             {
//                 int s = PlayerPrefs.GetInt("coins");
//                 PlayerPrefs.SetInt(CosmeticName, 1);
//                 s -= price;
//                 PlayerPrefs.SetInt("coins", s);
//             }
//             if (PlayerPrefs.GetInt(CosmeticName) == 1)
//             {
//                 enable.SetActive(true);
//                 disable.SetActive(true);
//                 gameObject.SetActive(false); //same as this.gameObject
//             }
//         }
//     }


// }