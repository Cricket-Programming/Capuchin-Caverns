using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.VR;
using System.Collections.Generic;

// this script synchronizes the display name of a player with PlayFab, a cloud-based game backend service created by SamSam.
public class PlayfabNameSync : MonoBehaviour
{
    public NameScript namepc;
    private string oldUsername;

    private void Start()
    {
        oldUsername = namepc.NameVar;
    }

    private void Update()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            if (namepc.NameVar != oldUsername)
            {
                oldUsername = namepc.NameVar;

                PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
                {
                    DisplayName = namepc.NameVar
                }, OnUpdateDisplayNameSuccess, OnUpdateDisplayNameError); //callback methods
            }
        }
    }

    private void OnUpdateDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display Name Changed!");
    }

    private void OnUpdateDisplayNameError(PlayFabError error)
    {
        if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            Debug.Log("Error, Account Is Banned!");
            oldUsername = "false";
        }
        else if (error.Error == PlayFabErrorCode.AccountNotFound)
        {
            Debug.Log("Error, Account Is Not Found!");
            oldUsername = "false";
        }
        else if (error.Error == PlayFabErrorCode.AccountDeleted)
        {
            Debug.Log("Error, Account Is Deleted!");
            oldUsername = "false";
        }
        else if (error.Error == PlayFabErrorCode.APIClientRequestRateLimitExceeded)
        {
            Debug.Log("Error, You Are Being Rate Limited!");
            oldUsername = "false";
        }
        else if (error.Error == PlayFabErrorCode.NotAuthenticated)
        {
            Debug.Log("Error, You Are Not Logged In!");
            oldUsername = "false";
        }
    }
}


// using UnityEngine;
// using PlayFab;
// using PlayFab.ClientModels;
// using Photon.VR;
// using System.Collections.Generic;

// public class PlayfabNameSync : MonoBehaviour
// {
//     public NameScript namepc;
//      string oldusername;

//     void Start()
//     {
//         oldusername = namepc.NameVar;
//     }

//     void Update()
//     {
//          if (PlayFabClientAPI.IsClientLoggedIn())
//             {
//            if (namepc.NameVar != oldusername)
//          {

//          oldusername = namepc.NameVar;

//         PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
//         {
//             DisplayName = namepc.NameVar
//         }, delegate (UpdateUserTitleDisplayNameResult result)
//         {
//             Debug.Log("Display Name Changed!");
//         }, delegate (PlayFabError error)
//         {

//             if (error.Error == PlayFabErrorCode.AccountBanned)
//             {
//                 Debug.Log("Error, Account Is Banned!");
//                 oldusername = "false";
//             }
//             if (error.Error == PlayFabErrorCode.AccountNotFound)
//             {
//                 Debug.Log("Error, Account Is Not Found!");
//                 oldusername = "false";
//             }
//             if (error.Error == PlayFabErrorCode.AccountDeleted)
//             {
//                 Debug.Log("Error, Account Is Deleted!");
//                 oldusername = "false";
//             }
//             if (error.Error == PlayFabErrorCode.APIClientRequestRateLimitExceeded)
//             {
//                 Debug.Log("Error, You Are Being Rate Limited!");
//                 oldusername = "false";
//             }
//             if (error.Error == PlayFabErrorCode.NotAuthenticated)
//             {
//                 Debug.Log("Error, You Are Not Logged In!");
//                 oldusername = "false";
//             }
//         }); ;
//            }
//       }
//     }
          
    


