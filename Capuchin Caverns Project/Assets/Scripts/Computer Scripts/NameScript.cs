using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.VR;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
//NameScript manages all the stuff for setting and saving the player name. 
//It sets a textmeshpro to the NameVar (Player's display name). It syncs NameVar with PlayFab and PhotonVRManager. PhotonVRManager saves NameVar to PlayerPrefs as "Username" key 
//and sets the Photon Network Player Nickname to NameVar
public class NameScript : MonoBehaviour
{
    [HideInInspector] public string NameVar;
    private TextMeshPro nameText;
    private string oldUsername;
    private void Start() {
        nameText = GetComponent<TextMeshPro>();
        
        NameVar = PlayerPrefs.GetString("Username"); //PhotonVRManager sets this
        oldUsername = NameVar;
        if (NameVar == "") SetDefaultName();

        nameText.text = NameVar;    
    }
    private void Update() {  
        //if NameVar got changed 
        if (NameVar != oldUsername) {
            oldUsername = NameVar;

            //change the text display to reflect the new NameVar
            nameText.text = NameVar;
            
            //sync the new NameVar with PlayFab
            if (PlayFabClientAPI.IsClientLoggedIn()) {
                PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
                {
                    DisplayName = NameVar
                }, OnUpdateDisplayNameSuccess, OnUpdateDisplayNameError); //callback methods for PlayFab.
            }

            //Sync the new NameVar with PhotonVRManager. PhotonVRManager will then sync NameVar with PlayerPrefs and PhotonNetwork.LocalPlayer.NickName
            //inside of photonvrmanager, it sets the username key in playerprefs to the NameVar, meaning
            PhotonVRManager.SetUsername(NameVar);

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
    private void SetDefaultName() {
        NameVar = "Monkey" + Random.Range(100, 1000);
    }









        //Getting display name from playfab which we don't do bc we have playerprefs in photonvrmanager.
    // public void GetDisplayName() {
    //     var request = new GetPlayerProfileRequest();
    //     PlayFabClientAPI.GetPlayerProfile(request, OnGetDisplayNameSuccess, OnGetDisplayNameFailure);
    // }
    // private void OnGetDisplayNameSuccess(GetPlayerProfileResult result) {
    //     NameVar = result.PlayerProfile.DisplayName();
    // }
}
