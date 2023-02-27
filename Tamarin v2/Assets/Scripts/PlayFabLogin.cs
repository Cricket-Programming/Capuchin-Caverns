using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.VR;
using System.Collections.Generic;
public class PlayFabLogin : MonoBehaviour
{
    public GameObject BanStuff;
    public List<GameObject> specialitems;
    public string CatalogName;
        public string MyPlayFabID;
    void Start()
    {
        Login();
    }

    void Login() {
        Debug.Log("Logging in/creating account now.");
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result) {
        Debug.Log("Account Login/Create successful!");
        PhotonVRManager.Connect();
                GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);
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
                }
            }
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }


    void OnError(PlayFabError error) {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());

        if(error.Error == PlayFabErrorCode.AccountBanned)
        {
            BanStuff.SetActive(true);
        }
    }
}   