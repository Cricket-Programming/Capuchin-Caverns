using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.VR;
using System.Collections.Generic;

public class PlayfabNameSync : MonoBehaviour
{
    public NameScript namepc;
     string oldusername;
    // Start is called before the first frame update
    void Start()
    {
        oldusername = namepc.NameVar;
    }

    // Update is called once per frame
    void Update()
    {
         if (PlayFabClientAPI.IsClientLoggedIn())
            {
           if (namepc.NameVar != oldusername)
         {

         oldusername = namepc.NameVar;

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = namepc.NameVar
        }, delegate (UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("Display Name Changed!");
        }, delegate (PlayFabError error)
        {

            if (error.Error == PlayFabErrorCode.AccountBanned)
            {
                Debug.Log("Error, Account Is Banned!");
                oldusername = "false";
            }
            if (error.Error == PlayFabErrorCode.AccountNotFound)
            {
                Debug.Log("Error, Account Is Not Found!");
                oldusername = "false";
            }
            if (error.Error == PlayFabErrorCode.AccountDeleted)
            {
                Debug.Log("Error, Account Is Deleted!");
                oldusername = "false";
            }
            if (error.Error == PlayFabErrorCode.APIClientRequestRateLimitExceeded)
            {
                Debug.Log("Error, You Are Being Rate Limited!");
                oldusername = "false";
            }
            if (error.Error == PlayFabErrorCode.NotAuthenticated)
            {
                Debug.Log("Error, You Are Not Logged In!");
                oldusername = "false";
            }
        }); ;
           }
      }
    }
          
    





}

