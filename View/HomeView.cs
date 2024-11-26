using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeView : BaseView
{
    public TMP_Text cash_lb;
    public override void Setup(ViewParam data)
    {
        int cash = DataAPIController.instance.GetCash();
        cash_lb.text = cash.ToString();
        DataAPIController.instance.GetCashfirebase(cash =>
        {
            Debug.LogError("cash : " + cash);
        });
    }
    // Start is called before the first frame update
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataPath.CASH, OnCashChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnCashChange);
    }
    private void OnCashChange(object dataChange)
    {
        cash_lb.text = ((int)dataChange).ToString();
    }
    public void OnAddCash()
    {
        DataAPIController.instance.AddCash(50, () =>
        {
            Debug.LogError("cash add");
        });
    }
    public void OnWeaponView()
    {
        WeaponViewParam weaponViewParam = new WeaponViewParam();
        weaponViewParam.weaponID = 2;
        ViewManager.instance.SwitchView(ViewIndex.WeaponView, weaponViewParam);
    }
    public void LoadGame()
    {
        ConfigMissionRecord configMissionRecord = ConfigManager.instance.configMission.GetRecordByKeySearch(1);
        // Sử dụng id để load scene bằng index
        LoadSceneManager.instance.LoadSceneByName(configMissionRecord.Scene_name, () =>
        {
            gameObject.SetActive(false);
            GameManager.instance.InitMission(configMissionRecord);
        });
    }
    public void Setting()
    {
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog);
    }
    public void OnShop()
    {
        DialogManager.instance.ShowDialog(DialogIndex.ShopDialog);
    }
    public void OnMission()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
}
