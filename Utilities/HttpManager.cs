using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;
using Newtonsoft.Json;
using VoxelBusters.CoreLibrary;

public class HttpManager : BySingleton<HttpManager>
{

    private void Start()
    {

    }

    //private string userID = "";
    public void OnGet(string urlAdd, object dataSend, bool isShowLoading = false, Action<HTTPResponse> callback = null)
    {
        if (isShowLoading)
        {
            //showLoading
        }

        string url = URLConfig.ROOT_CLOUD_URL + "/" + urlAdd;
        Uri uri = new Uri(url);
        HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Get, (req, res) =>
        {
            //off loading 
            if (res.StatusCode == 200)
            {
                if (callback != null)
                    callback(res);
            }
            else
            {
                Debug.LogError("error : " + res.StatusCode);
                if (callback != null)
                    callback(null);
            }
        });

        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        if (dataSend != null)
        {
            //request.SetHeader("uuid", userID);
            request.RawData = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataSend));
        }

        request.Send();
    }

    public void OnPost(string urlAdd, object dataSend, bool isShowLoading = false, Action<HTTPResponse> callback = null)
    {
        if (isShowLoading)
        {
            //showLoading
        }

        string url = URLConfig.ROOT_CLOUD_URL + "/" + urlAdd;
        Uri uri = new Uri(url);
        HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
        {
            //off loading
            if (res != null)
            {
                if (res.StatusCode == 200)
                {
                    if (callback != null)
                        callback(res);
                }
                else
                {

                    if (callback != null)
                        callback(res);
                }
            }
            else
            {

            }

        });
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.SetHeader("uuid", FirebaseAuthenicationManager.instance.firebase_Account);
        if (dataSend != null)
            request.RawData = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataSend));
        request.Send();
    }

    public void OnPost(string userID, string urlAdd, string dataPath, object dataSend, bool isShowLoading = false, Action<HTTPResponse> callback = null, string key = "")
    {
        if (isShowLoading)
        {
            //showLoading
        }

        string url = URLConfig.ROOT_CLOUD_URL + "/" + urlAdd;
        Uri uri = new Uri(url);
        HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
        {
            //off loading 
            if (res.StatusCode == 200)
            {
                if (callback != null)
                    callback(res);
            }
            else
            {
                Debug.Log("url: " + url);
                Debug.Log("error : " + res.StatusCode);
                if (callback != null)
                    callback(null);
            }
        });
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.SetHeader("uuid", userID);
        request.SetHeader("dataPath", dataPath);
        request.SetHeader("dataPathKey", key);
        request.RawData = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataSend));
        request.Send();
    }
    public void OnPost(string userID, string urlAdd, object dataSend, bool isShowLoading = false, Action<HTTPResponse> callback = null)
    {
        if (isShowLoading)
        {
            //showLoading
        }

        string url = URLConfig.ROOT_CLOUD_URL + "/" + urlAdd;
        Uri uri = new Uri(url);
        HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
        {
            //off loading
            if (res != null)
            {
                if (res.StatusCode == 200)
                {
                    if (callback != null)
                        callback(res);
                }
                else
                {

                    if (callback != null)
                        callback(res);
                }
            }
            else
            {

            }

        });
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.SetHeader("uuid", userID);
        if (dataSend != null)
            request.RawData = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataSend));
        request.Send();
    }

    /* public void OnPost(string userID, string urlAdd, bool isShowLoading = false, Action<HTTPResponse> callback = null)
     {
         if (isShowLoading)
         {
             //showLoading
         }

         string url = URLConfig.ROOT_CLOUD_URL + "/" + urlAdd;
         Uri uri = new Uri(url);
         HTTPRequest request = new HTTPRequest(uri, HTTPMethods.Post, (req, res) =>
         {
             //off loading
             if (res != null)
             {

             }
             else
             {

             }
             if (callback != null)
                 callback(res);
         });
         request.SetHeader("Content-Type", "application/json; charset=UTF-8");
         request.SetHeader("uuid", userID);
         request.Send();
     }*/
}

public class PostOpenChestData
{
    public int idChest;
}
public class GoldUpdateServer
{
    public int gold;
}
public class GoldAndTrophyUpdateServer
{
    public int trophy;
    public int gold;
}

public class PostRoomChallenge
{
    public string token;
    public string friendName;
    public string room;
    public int idTournament;
    public string typeTournament;
}
public class Example
{
    public string key;
}

public class InviteEventData
{
    public string uuidSender;
    public string idEvent;
    public string invitationCode;
}