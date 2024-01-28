using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

// This script connects and logs in the player with PlayFab.

// IF YOU ARE TRYING TO ACCESS ONE OF THE VARIABLES YOU MIGHT HAVE TO MAKE IT PUBLIC.
public class PlayFabLogin : MonoBehaviour
{
    [Header("COSMETICS")]
    [HideInInspector] public static PlayFabLogin instance;
    [HideInInspector] public string MyPlayFabID;
    [SerializeField] private TMP_Text MyPlayFabIDDisplaySpot;
    [SerializeField] private  string CatalogName;
    [SerializeField] private List<GameObject> specialitems;
    [SerializeField] private  List<GameObject> disableitems;

    [Header("BANNED")]
    [SerializeField] private string bannedSceneName;
    [Header("TITLE DATA")]
    [SerializeField] private TextMeshPro MOTDText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully to PlayFab");
        GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);

        GetMOTD();
        CurrencyManager.Instance.SetUpDailyRewardsData();
    }

    private void AccountInfoSuccess(GetAccountInfoResult result)
    {
        MyPlayFabID = result.AccountInfo.PlayFabId;
        MyPlayFabIDDisplaySpot.text = MyPlayFabID;
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        (result) =>
        {
            foreach (var item in result.Inventory)
            {
                if (item.CatalogVersion == CatalogName)
                {
                    for (int i = 0; i < specialitems.Count; i++)
                    {
                        if (specialitems[i].name == item.ItemId)
                        {
                            specialitems[i].SetActive(true);
                        }
                    }
                    for (int i = 0; i < disableitems.Count; i++)
                    {
                        if (disableitems[i].name == item.ItemId)
                        {
                            disableitems[i].SetActive(false);
                        }
                    }
                }
            }
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
        }); 
    }







    private void OnError(PlayFabError error)
    {
        if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            SceneManager.LoadScene(bannedSceneName);
        }
    }
    // This is currently not being used, but if MOTD were to be located in PlayFab (instead of GitHub right now), it would retrieve the MOTD from the TitleData

    private void GetMOTD()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), MOTDGot, OnError);
    }

    private void MOTDGot(GetTitleDataResult result)
    {
        if (result.Data == null || result.Data.ContainsKey("MOTD") == false)
        {
            Debug.Log("No MOTD");
            return;
        }
        MOTDText.text = result.Data["MOTD"];
        
    }


}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using PlayFab;
// using System.Threading.Tasks;
// using PlayFab.ClientModels;
// using UnityEngine.SceneManagement;
// using TMPro;

// // This script connects and logs in the player with PlayFab.
// public class PlayFabLogin : MonoBehaviour
// {
//     [Header("COSMETICS")]
//     [HideInInspector] public static PlayFabLogin instance;
//     [HideInInspector] public string MyPlayFabID;
//     [SerializeField] private TMP_Text MyPlayFabIDDisplaySpot;
//     public string CatalogName;
//     public List<GameObject> specialitems;
//     public List<GameObject> disableitems;
//     [Header("CURRENCY")]
//     public string CurrencyName;
//     public TextMeshPro currencyText;
//     [SerializeField] private CurrencyManager CurrencyManager;

//     public int coins; //the player's coins. Accessed in Purchase.cs from singleton pattern
//     [Header("BANNED")]
//     public string bannedscenename;
//     [Header("TITLE DATA")]
//     public TextMeshPro MOTDText;
//     [Header("PLAYER DATA")]
//     public TextMeshPro UserName;
//     public string StartingUsername;
//     public string playerName; // changed from name

//     public bool UpdateName;
    

//     public void Awake()
//     {
//         instance = this;
//     }

//     private void Start()
//     {
//         login();
        
//     }

//     public void login()
//     {

//         var request = new LoginWithCustomIDRequest
//         {
//             CustomId = SystemInfo.deviceUniqueIdentifier,
//             CreateAccount = true,
//             InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
//             {
//                 GetPlayerProfile = true
//             }
//         };
//         PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
//     }

//     public void OnLoginSuccess(LoginResult result)
//     {
//         Debug.Log("Logged in successfully to PlayFab");
//         GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
//         PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);
//         GetVirtualCurrencies();
//         GetMOTD();
//         CurrencyManager.SetUpDailyRewardsData();
//     }

//     public void AccountInfoSuccess(GetAccountInfoResult result)
//     {
//         MyPlayFabID = result.AccountInfo.PlayFabId;
//         MyPlayFabIDDisplaySpot.text = MyPlayFabID;
//         PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
//         (result) =>
//         {
//             foreach (var item in result.Inventory)
//             {
//                 if (item.CatalogVersion == CatalogName)
//                 {
//                     for (int i = 0; i < specialitems.Count; i++)
//                     {
//                         if (specialitems[i].name == item.ItemId)
//                         {
//                             specialitems[i].SetActive(true);
//                         }
//                     }
//                     for (int i = 0; i < disableitems.Count; i++)
//                     {
//                         if (disableitems[i].name == item.ItemId)
//                         {
//                             disableitems[i].SetActive(false);
//                         }
//                     }
//                 }
//             }
//         },
//         (error) =>
//         {
//             Debug.LogError(error.GenerateErrorReport());
//         }); 
//     }



//     public void GetVirtualCurrencies()
//     {
//         PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
//     }

//     private void OnGetUserInventorySuccess(GetUserInventoryResult result)
//     {
//         coins = result.VirtualCurrency["HS"];
//         currencyText.text = "You have " + coins.ToString() + " " + CurrencyName;
//     }

//     private void OnError(PlayFabError error)
//     {
//         if (error.Error == PlayFabErrorCode.AccountBanned)
//         {
//             SceneManager.LoadScene(bannedscenename);
//         }

//     }
//     //Get TitleData

//     public void GetMOTD()
//     {
//         PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), MOTDGot, OnError);
//     }

//     public void MOTDGot(GetTitleDataResult result)
//     {
//         if (result.Data == null || result.Data.ContainsKey("MOTD") == false)
//         {
//             Debug.Log("No MOTD");
//             return;
//         }
//         MOTDText.text = result.Data["MOTD"];
        
//     }


// }

