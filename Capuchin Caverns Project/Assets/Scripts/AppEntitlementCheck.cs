using UnityEngine;
using Oculus.Platform;

public class AppEntitlementCheck : MonoBehaviour
{

    void Awake()
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


    void EntitlementCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("You are NOT entitled to use this app.");
        }
        else
        {
            Debug.Log("You are entitled to use this app.");
        }
    }
}