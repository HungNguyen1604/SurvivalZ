using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponView : BaseView
{
    public TMP_Text cash_lb;

    public Text weapon_id;
    public Text weapon_lv;
    public Text weapon_name;
    private ConfigWeaponRecord cf;
    private WeaponData weaponData;
    public TapWeaponButton tap_buttons;

    public WeaponViewListItem Cur_wp_item_list;
    [SerializeField]
    private WeaponViewDetail wp_detail;
    public override void Setup(ViewParam data)
    {
        int cash = DataAPIController.instance.GetCash();
        cash_lb.text = cash.ToString();

        WeaponViewParam param = data as WeaponViewParam;
        weaponData = DataAPIController.instance.GetWeaponDataById(param.weaponID);
        cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(param.weaponID, weaponData.level);
        //weapon_id.text = param.weaponID.ToString();
        //weapon_lv.text = weaponData.level.ToString();
        //weapon_name.text = cf.Name.ToString();
        tap_buttons.Init();
    }
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataPath.CASH, OnCashChange);
        DataTrigger.RegisterValueChange(DataPath.DIC_WEAPON + "/" + cf.id.Tokey(), OnWeaponDataChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnCashChange);
        DataTrigger.UnRegisterValueChange(DataPath.DIC_WEAPON + "/" + cf.id.Tokey(), OnWeaponDataChange);
    }
    private void OnCashChange(object dataChange)
    {
        cash_lb.text = ((int)dataChange).ToString();
    }
    private void OnWeaponDataChange(object data)
    {
        weaponData = data as WeaponData;
        weapon_lv.text = weaponData.level.ToString();
    }
    public void OnHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    public void UpgradeWP()
    {
        DataAPIController.instance.UpgradeLevelWeaponDataById(weaponData.id);
    }
    public void OnTap()
    {
        Cur_wp_item_list = null;
    }
    public void OnWeaponSelected(WeaponViewListItem wp_item_list)
    {
        Cur_wp_item_list?.OnDeSelect();
        Cur_wp_item_list = wp_item_list;
        wp_detail.Setup(wp_item_list);
    }
    public void OnShop()
    {
        DialogManager.instance.ShowDialog(DialogIndex.ShopDialog);
    }

}
