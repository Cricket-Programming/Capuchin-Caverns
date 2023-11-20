using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using Oculus.Platform;
using Oculus.Platform.Models;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabShopManager : MonoBehaviour
{
    [SerializeField] private string skuToPurchase;
    [SerializeField] private int currencyAmount;

//
    private string[] skus = { "Marbles3000", "Marbles5000", "Marbles1000" };

    private void Start()
    {
        GetPrices();
        GetPurchases();
    }

    private void GetPrices()
    {
        IAP.GetProductsBySKU(skus).OnComplete(GetPricesCallback);
    }

    private void GetPricesCallback(Message<ProductList> msg)
    {
        if (msg.IsError) return;
    }

    private void GetPurchases()
    {
        IAP.GetViewerPurchases().OnComplete(GetPurchasesCallback);
    }

    private void GetPurchasesCallback(Message<PurchaseList> msg)
    {
        if (msg.IsError) return;
    }
    //

    public void BuyProduct()
    {

        IAP.LaunchCheckoutFlow(skuToPurchase).OnComplete(BuyProductCallback);
        IAP.ConsumePurchase(skuToPurchase); //Oncomplete() is optional and in this method I don't need it.
    }

    private void BuyProductCallback(Message<Oculus.Platform.Models.Purchase> msg)
    {
        if (msg.IsError) return;

        var request = new AddUserVirtualCurrencyRequest
        {   
            Amount = currencyAmount,
            VirtualCurrency = "HS"
        };
    
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnAddCurrencyFailure);
    }

    private void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Currency added: " + result.Balance);
        PlayFabLogin.instance.GetVirtualCurrencies();
    }

    private void OnAddCurrencyFailure(PlayFabError error)
    {
        Debug.LogError("Failed to add currency: " + error.ErrorMessage);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            BuyProduct();
            PlayFabLogin.instance.GetVirtualCurrencies();
        }
    }
}