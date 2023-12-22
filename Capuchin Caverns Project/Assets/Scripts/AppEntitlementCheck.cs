using UnityEngine;
using Oculus.Platform;
using TMPro;
public class AppEntitlementCheck : MonoBehaviour
{
    [SerializeField] private TMP_Text x;
    private void Awake()
    {
        //Oculus.Platform.Core.Initialize();
        try
        {
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
        }
        catch (UnityException e)
        {
            Debug.LogError("Platform failed to initialize due to exception.");
            Debug.LogException(e);
        }
    }


    private void EntitlementCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("You are NOT entitled to use this app.");
            x.text = "You are NOT entitled to use this app.";
        }
        else
        {
            Debug.Log("You are entitled to use this app.");
            x.text = "You are entitled to use this app.";
        }
    }
}