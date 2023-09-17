using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayFabLogin : MonoBehaviour
{
    [Header("COSMETICS")]
    public static PlayFabLogin instance;
    public string MyPlayFabID;
    public string CatalogName;
    public List<GameObject> specialitems;
    public List<GameObject> disableitems;
    [Header("CURRENCY")]
    public string CurrencyName;
    public TextMeshPro currencyText;
    [SerializeField]
    public int coins;
    [Header("BANNED")]
    public string bannedscenename;
    [Header("TITLE DATA")]
    public TextMeshPro MOTDText;
    [Header("PLAYER DATA")]
    public TextMeshPro UserName;
    public string StartingUsername;
    public string name;
    [SerializeField]
    public bool UpdateName;
    



    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        login();
        
    }

    public void login()
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

    public void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("logging in");
        GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);
        GetVirtualCurrencies();
        GetMOTD();
    }

    public void AccountInfoSuccess(GetAccountInfoResult result)
    {
        MyPlayFabID = result.AccountInfo.PlayFabId;

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



    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        coins = result.VirtualCurrency["HS"];
        currencyText.text = "You have " + coins.ToString() + " " + CurrencyName;
    }

    private void OnError(PlayFabError error)
    {
        if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            SceneManager.LoadScene(bannedscenename);
        }

    }
    //Get TitleData

    public void GetMOTD()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), MOTDGot, OnError);
    }

    public void MOTDGot(GetTitleDataResult result)
    {
        if (result.Data == null || result.Data.ContainsKey("MOTD") == false)
        {
            Debug.Log("No MOTD");
            return;
        }
        MOTDText.text = result.Data["MOTD"];
        
    }


}

// using UnityEngine;
// using PlayFab;
// using PlayFab.ClientModels;
// using Photon.VR;
// using System.Collections.Generic;
// using System.Collections;
// using TMPro;
// public class Playfablogin : MonoBehaviour
// {
//     //changes made using https://www.youtube.com/watch?v=HR9PgoPREVA
//     public GameObject BanStuff;
//     public List<GameObject> specialitems;
//     public string CatalogName;
//     public string MyPlayFabID;
//     public TextMeshPro idText;
//     private void Start()
//     {
//         Login();
//     }


//     private void Login() { 
//         Debug.Log("Logging in/creating account now.");
//         var request = new LoginWithCustomIDRequest {
//             CustomId = SystemInfo.deviceUniqueIdentifier,
//             CreateAccount = true,
//             InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
//             {
//                 GetPlayerProfile = true
//             }
//         };
//         PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
//     }

//     void OnLoginSuccess(LoginResult result) {
//         Debug.Log("Account Login/Create successful!");
//         PhotonVRManager.Connect();
//         GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
//         PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);


//     }
//     public void AccountInfoSuccess(GetAccountInfoResult result)
//     {
//         MyPlayFabID = result.AccountInfo.PlayFabId;
//         idText.text = MyPlayFabID;

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
//                 }
//             }
//         },
//         (error) =>
//         {
//             Debug.LogError(error.GenerateErrorReport());
//         });
//     }


//     void OnError(PlayFabError error) {
//         Debug.Log("Error while logging in/creating account!");
//         Debug.Log(error.GenerateErrorReport());

//         if(error.Error == PlayFabErrorCode.AccountBanned)
//         {
//             BanStuff.SetActive(true);
//         }
//     }
// }   