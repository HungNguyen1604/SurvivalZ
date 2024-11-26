using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.EssentialKit;

public class GameServiceManager : BySingleton<GameServiceManager>
{
    public bool isEndGameService = false;
    public ILocalPlayer localPlayer;
    private void OnEnable()
    {
        GameServices.OnAuthStatusChange += OnAuthStatusChange;

        // register for events
        CloudServices.OnUserChange += OnUserChange;
        CloudServices.OnSavedDataChange += OnSavedDataChange;
        CloudServices.OnSynchronizeComplete += OnSynchronizeComplete;
    }

    private void OnDisable()
    {
        GameServices.OnAuthStatusChange -= OnAuthStatusChange;

        // unregister from events
        CloudServices.OnUserChange -= OnUserChange;
        CloudServices.OnSavedDataChange -= OnSavedDataChange;
        CloudServices.OnSynchronizeComplete -= OnSynchronizeComplete;
    }

    public string UUID()
    {
        if (GameServices.IsAuthenticated)
        {
            return localPlayer.Id;
        }
        else
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }
    public void InitGameService()
    {
        if (GameServices.IsAvailable() == false)
        {
            isEndGameService = true;
        }
        else
        {
            if (GameServices.IsAuthenticated)
            {
                isEndGameService = true;

                localPlayer = GameServices.LocalPlayer;
            }
            else
            {
                GameServices.Authenticate();
            }
        }

    }
    private void OnAuthStatusChange(GameServicesAuthStatusChangeResult result, VoxelBusters.CoreLibrary.Error error)
    {
        if (result.AuthStatus == LocalPlayerAuthStatus.Authenticated)
        {
            localPlayer = GameServices.LocalPlayer;
        }
        else
        {
            localPlayer = null;
        }
        isEndGameService = true;

    }
    public string GetDataCloud()
    {
        return CloudServices.GetString("DATA");
    }
    public void SetDataCloud(string data)
    {
        CloudServices.SetString("DATA", data);
    }

    private void OnUserChange(CloudServicesUserChangeResult result, VoxelBusters.CoreLibrary.Error error)
    {
        var user = result.User;
        Debug.Log("Received user change callback.");
        Debug.Log("User id: " + user.UserId);
        Debug.Log("User status: " + user.AccountStatus);
    }

    private void OnSavedDataChange(CloudServicesSavedDataChangeResult result)
    {
        var changedKeys = result.ChangedKeys;
        Debug.Log("Received saved data change callback.");
        Debug.Log("Reason: " + result.ChangeReason);
        Debug.Log("Total changed keys: " + changedKeys.Length);
        Debug.Log("Here are the changed keys:");
        for (int iter = 0; iter < changedKeys.Length; iter++)
        {
            //Fetching local and cloud values for value type which is string in this example.
            string cloudValue;
            string localCacheValue;
            CloudServicesUtility.TryGetCloudAndLocalCacheValues(changedKeys[iter], out cloudValue, out localCacheValue, "default");
            Debug.Log(string.Format("[{0}]: {1}  [Cloud Value] : {2} [Local Cache Value] : {3}  ", iter, changedKeys[iter], cloudValue, localCacheValue));
        }
    }

    private void OnSynchronizeComplete(CloudServicesSynchronizeResult result)
    {
        Debug.Log("Received synchronize finish callback.");
        Debug.Log("Status: " + result.Success);
    }

    public void ReportAchievement(string achievementId)
    {
        double percentageCompleted = 100;// This is in the range [0 - 100]
        GameServices.ReportAchievementProgress(achievementId, percentageCompleted, (success, error) =>
        {
            if (success)
            {
                Debug.Log("Request to submit progress finished successfully.");
            }
            else
            {
                Debug.Log("Request to submit progress failed with error. Error: " + error);
            }
        });
    }
    public void ShowAchievement()
    {
        GameServices.ShowAchievements((result, error) =>
        {
            Debug.Log("Achievements view closed");
        });
    }

    public void ReportScrore()
    {
        long score = 164;
        string leaderboardId = "L_001";// Value from setup done in inspector
        GameServices.ReportScore(leaderboardId, score, (success, error) =>
        {
            if (success)
            {
                Debug.Log("Request to submit score finished successfully.");
            }
            else
            {
                Debug.Log("Request to submit score failed with error: " + error.Description);
            }
        });
    }
    public void ShowScore()
    {
        GameServices.ShowLeaderboards(callback: (result, error) =>
        {
            Debug.Log("Leaderboards UI closed");
        });
    }

}