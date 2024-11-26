using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using BestHTTP;
using UnityEngine.Purchasing.Extension;
using Unity.Services.Core;

[System.Serializable]
public class PurchaseReceipts
{
    public string Store;
    public string TransactionID;
    public string Payload;

    public PurchaseReceipts()
    {

    }
}

[System.Serializable]
public class AppleReceipts
{

    public string receipt;

    public AppleReceipts()
    {

    }
}

[System.Serializable]
public class GoogleReceipt
{
    public string json;
    public string signature;

    public GoogleReceipt()
    {

    }
}
[System.Serializable]
public class GoogleReceiptJson
{
    public string orderId;
    public string packageName;
    public string productId;
    public string purchaseTime;

    public GoogleReceiptJson()
    {

    }
}
[System.Serializable]
public class GoogleReceiptData
{
    public string packageName;
    public string productId;
    public long purchaseTime;
    public int purchaseState;
    public string purchaseToken;

    public GoogleReceiptData()
    {

    }
}

public class Purchaser : BySingleton<Purchaser>, IDetailedStoreListener
{
    public static IStoreController m_StoreController;
    // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider;
    public Action<bool> callBackBuy = null;
    private Product currentProduct = null;
    private List<string> lsProductConsumable = new List<string>();





    public void InitPurchase()
    {

        if (m_StoreController == null)
        {
            //begin to configure our connection to purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        lsProductConsumable = ConfigManager.instance.configShop.GetAllStoreID();


        foreach (string e in lsProductConsumable)
        {
            builder.AddProduct(e, ProductType.Consumable);

        }

        UnityPurchasing.Initialize(this, builder);

    }

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        Debug.Log(" Unity IAP init done!");
    }

    public void BuyConsumable(string productID, Action<bool> callback)
    {
        callBackBuy = callback;
        BuyProductID(productID);
    }

    private void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    //
    // --- IStoreListener
    //


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //
        //  DataAPIController.instance.AddCash()
        // return PurchaseProcessingResult.Complete;
        //2

        // A consumable product has been purchased by this user.
        if (lsProductConsumable.Contains(args.purchasedProduct.definition.id))
        {
            return PurchaseChecking(args, args.purchasedProduct.definition.id, 1);
        }
        //if (String.Equals(args.purchasedProduct.definition.id, kProductIDPrice_GoldPackage_1, StringComparison.Ordinal))
        //    return PurchaseChecking(args, kProductIDPrice_GoldPackage_1, 1);

        return PurchaseProcessingResult.Pending;

    }

    public PurchaseProcessingResult PurchaseChecking(PurchaseEventArgs args, string kProduct, int type)
    {

        currentProduct = args.purchasedProduct;
        PurchaseReceipts purchaseReceipts = JsonUtility.FromJson<PurchaseReceipts>(args.purchasedProduct.receipt);

        Debug.Log("iap : " + purchaseReceipts.Store);


        if (purchaseReceipts.Store == "GooglePlay")
        {
            // GoogleReceipt googleReceipt = JsonUtility.FromJson<GoogleReceipt>(purchaseReceipts.Payload);
            // GoogleReceiptData data = JsonUtility.FromJson<GoogleReceiptData>(googleReceipt.json);
            Uri uri = new Uri(URLConfig.ROOT_CLOUD_URL + "/inapp/google");
            HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
            {
                if (res.StatusCode == 200)
                {
                    GoogleReceipt googleReceipt = JsonUtility.FromJson<GoogleReceipt>(purchaseReceipts.Payload);
                    GoogleReceiptJson googleReceiptJson = JsonUtility.FromJson<GoogleReceiptJson>(googleReceipt.json);
                    Debug.Log("oderid : " + googleReceiptJson.orderId);
                    UpdatePurchase(kProduct, type, googleReceiptJson.orderId);
                    m_StoreController.ConfirmPendingPurchase(args.purchasedProduct);
                    {
                        callBackBuy?.Invoke(true);
                    }
                }
                else
                {
                    if (callBackBuy != null)
                    {
                        callBackBuy(false);
                    }
                }
            });
            request.SetHeader("Content-Type", "application/json; charset=UTF-8");
            request.RawData = System.Text.Encoding.UTF8.GetBytes(purchaseReceipts.Payload);
            request.Send();
            return PurchaseProcessingResult.Pending;
        }
        else if (purchaseReceipts.Store == "AppleAppStore")
        {

            Debug.Log(" playload : " + purchaseReceipts.Payload);
            //PurchaseReceipts purchaseReceipts = JsonUtility.FromJson<PurchaseReceipts> (args.purchasedProduct.receipt);
            Uri uri = new Uri(URLConfig.ROOT_CLOUD_URL + "/inapp/apple");
            HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
            {
                if (res.StatusCode == 200)
                {
                    UpdatePurchase(kProduct, type, "Orderios");
                    m_StoreController.ConfirmPendingPurchase(args.purchasedProduct);
                    {
                        Debug.Log(" playload : true ");
                        callBackBuy?.Invoke(true);
                    }
                }
                else
                {
                    if (callBackBuy != null)
                    {
                        Debug.Log(" play fail :");
                        callBackBuy(false);
                    }
                }
            });
            request.SetHeader("Content-Type", "application/json; charset=UTF-8");
            request.RawData = System.Text.Encoding.UTF8.GetBytes(args.purchasedProduct.receipt);
            request.Send();
            return PurchaseProcessingResult.Pending;
        }
        else if (purchaseReceipts.Store == "fake")
        {
            Uri uri = new Uri(URLConfig.ROOT_CLOUD_URL + "/inapp/editor");
            HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
            {
                if (res.StatusCode == 200)
                {
                    Debug.Log("log unity inapp editor : " + args.purchasedProduct.receipt);
                    UpdatePurchase(kProduct, type, "Editor");
                    m_StoreController.ConfirmPendingPurchase(args.purchasedProduct);
                    callBackBuy?.Invoke(true);

                }
                else
                {
                    if (callBackBuy != null)
                    {
                        callBackBuy(false);
                    }
                }
            });
            request.SetHeader("Content-Type", "application/json; charset=UTF-8");
            request.RawData = System.Text.Encoding.UTF8.GetBytes(args.purchasedProduct.receipt);
            request.Send();
            return PurchaseProcessingResult.Pending;
        }
        return PurchaseProcessingResult.Pending;
    }

    private void UpdatePurchase(string kProduct, int type, string orderID)
    {
        //  DataAPIController.instance.UpdateResultStorePurchase(kProduct, orderID);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        if (callBackBuy != null)
            callBackBuy(false);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureDescription));
        if (callBackBuy != null)
            callBackBuy(false);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
}
