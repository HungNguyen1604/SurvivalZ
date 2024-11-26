using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDialog : BaseDialog
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
    public void OnHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }

    public void ReportAchievement()
    {
        GameServiceManager.instance.ReportAchievement("A_001");
    }
    public void ShowAchievement()
    {
        GameServiceManager.instance.ShowAchievement();
    }
    public void ReportScore()
    {
        GameServiceManager.instance.ReportScrore();
        AnalyticsManager.instance.Log("Update_Score");
    }
    public void ShowScore()
    {
        // GameServiceManager.instance.ShowScore();
        /*HttpManager.instance.OnGet(URLConfig.POST_GETSERVERTIME, null, false, (res) =>
        {
            Debug.LogError("POST_GETSERVERTIME : " + res.DataAsText);
        });*/

        /* HttpManager.instance.OnPost(FirebaseAuthenicationManager.instance.firebase_Account, URLConfig.POST_GET_DATA, null, false, (res) =>
         {
             Debug.LogError("POST_GET_DATA : " + res.DataAsText);
             UserData data = JsonConvert.DeserializeObject<UserData>(res.DataAsText);
         });*/

        DataAPIController.instance.UpdateInfo(" new user hung nguyen");
    }
}
