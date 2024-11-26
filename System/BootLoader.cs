using Firebase;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    public DataAPIController dataAPIController;
    // Start is called before the first frame update


    async void Awake()
    {
        var options = new InitializationOptions();

        options.SetEnvironmentName("production");
        await UnityServices.InitializeAsync(options);
        StartCoroutine("Start");
    }

    IEnumerator Start()
    {
        //1. Check game services
        // Giữ lại đối tượng khi chuyển cảnh
        DontDestroyOnLoad(this.gameObject);

        dataAPIController.Init();
        ConfigManager.instance.InitConfig(() =>
        {
            Purchaser.instance.InitPurchase();
            GameServiceManager.instance.InitGameService();
        });
        yield return new WaitUntil(() => GameServiceManager.instance.isEndGameService);
        //2. Login game services
        FirebaseAuthenicationManager.instance.CheckAuthenication();
        yield return new WaitUntil(() => FirebaseAuthenicationManager.instance.firebase_Account != string.Empty);

        //3. Load Data
        /* if (GameServiceManager.instance.localPlayer != null)
         {
             Debug.LogError("userid : " + GameServiceManager.instance.localPlayer.Id);
             string data_cloud = GameServiceManager.instance.GetDataCloud();
             DataAPIController.instance.CheckDataCloud(data_cloud, LoadConfigDone);
         }
         else
         {
             Debug.LogError("GameService fail");
             DataAPIController.instance.InitDataLocal(LoadConfigDone);

         }*/

        /////Datafirebase realtime
        // FirebaseRealtimeDataControl.instance.CheckDataServer();
        // yield return new WaitUntil(() => FirebaseRealtimeDataControl.instance.isGetDataDone);

        // DataAPIController.instance.CheckDataFirebase(FirebaseRealtimeDataControl.instance.GetData(), LoadConfigDone);

        DataAPIController.instance.GetDataServer(LoadConfigDone);

    }

    private void LoadConfigDone()
    {
        LoadSceneManager.instance.LoadSceneByName("Buffer", () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.HomeView);
            //FirebaseRealtimeDataControl.instance.Update_Version_Signin();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}