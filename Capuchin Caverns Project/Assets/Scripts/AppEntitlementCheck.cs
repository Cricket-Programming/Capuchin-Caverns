using UnityEngine;
using Oculus.Platform;
// The Entitlement Check supposedly verifies the user purchased or obtained my app legitimately.
public class AppEntitlementCheck : MonoBehaviour
{
    private void Awake()
    {
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
        }
        else
        {
            Debug.Log("You are entitled to use this app.");
        }
    }
}