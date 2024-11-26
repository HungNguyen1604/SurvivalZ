using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary;

[CreateAssetMenu(fileName = "DataAPIController", menuName = "BY/DataAPIController", order = 1)]

public class DataAPIController : ScriptableObject
{
    public static DataAPIController instance;
    [SerializeField]
    private DataModel dataModel;

    public void Init()
    {
        instance = this;
    }
    public void CheckDataCloud(string data_string, Action callback)
    {
        if (data_string == null || data_string.Length < 10)
        {
            Debug.LogError(" data cloud empty");
            InitDataLocal(callback);
        }
        else
        {
            UserData data_cloud = JsonConvert.DeserializeObject<UserData>(data_string);
            Debug.LogError(" data cloud version " + data_cloud.version_data);
            Debug.LogError(" data cloud version " + data_cloud.version_data);
            dataModel.CheckDataLocalWithCloud(data_cloud);
            callback.Invoke();

        }
        // UserData dataCloud = JsonConvert.DeserializeObject<UserData>(data_string);
    }

    public void CheckDataFirebase(UserData userdata_, Action callback)
    {
        if (userdata_ == null)
        {
            Debug.LogError(" data cloud empty");
            InitDataLocal(callback);
        }
        else
        {
            Debug.LogError(" data cloud version " + userdata_.version_data);
            dataModel.CheckDataLocalWithCloud(userdata_);
            callback.Invoke();

        }
        FirebaseRealtimeDataControl.instance.SaveAllData();
    }
    public void InitDataLocal(Action callback)
    {
        if (dataModel.CheckDataLocal())
        {
            callback.Invoke();
        }


        dataModel.LoadDataLocal((isNew) =>
        {
            Debug.Log(isNew);
            callback?.Invoke();
        });
        UserData userData = new UserData();
        userData.version_data = 0;
    }

    public int GetCash()
    {
        return dataModel.ReadData<int>(DataPath.CASH);
    }
    public void AddCash(int cash, Action callback)
    {
        int num = dataModel.ReadData<int>(DataPath.CASH);
        num += cash;
        dataModel.UpdateData(DataPath.CASH, num, callback);
    }
    public WeaponData GetWeaponDataById(int id)
    {
        WeaponData wp = dataModel.ReadDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey());
        return wp;
    }
    public void UpgradeLevelWeaponDataById(int id)
    {
        int num = dataModel.ReadData<int>(DataPath.CASH);
        if (num >= 30)
        {
            num -= 30;
            dataModel.UpdateData(DataPath.CASH, num, null);
            WeaponData wp = dataModel.ReadDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey());
            wp.level++;
            dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey(), wp, null);
        }
    }

    public void GetDataServer(Action callback)
    {
        HttpManager.instance.OnPost(URLConfig.POST_GET_DATA, null, false, (res) =>
        {
            Debug.LogError("POST_GET_DATA : " + res.DataAsText);
            UserData data = JsonConvert.DeserializeObject<UserData>(res.DataAsText);
            dataModel.CreateNewData(data);
            callback();
        });
    }

    public void UpdateInfo(string name_)
    {
        UserInfo infoData = dataModel.ReadData<UserInfo>(DataPath.INFO);
        infoData.username = name_;
        dataModel.UpdateData(DataPath.INFO, infoData);

        UserData data_u = dataModel.GetAllData();

        HttpManager.instance.OnPost(URLConfig.POST_CREATE_DATA, data_u, false, (res) =>
        {
            Debug.LogError(" res change name " + res.StatusCode);
        });
    }

    public UserInfo GetUserInfo()
    {
        return dataModel.ReadData<UserInfo>(DataPath.INFO);
    }

    public void GetUserInfofirebase(Action<UserInfo> callback)
    {
        FirebaseRealtimeDataControl.instance.Read(DataPath.INFO, (s) =>
        {
            if (s != string.Empty)
            {
                UserInfo info = JsonConvert.DeserializeObject<UserInfo>(s);
                callback(info);
            }
        });

    }
    public void GetCashfirebase(Action<int> callback)
    {
        FirebaseRealtimeDataControl.instance.Read(DataPath.CASH, (s) =>
        {
            if (s != string.Empty)
            {
                Debug.Log(" s : " + s);
                int cash = int.Parse(s);
                callback(cash);
            }
        });

    }

    public bool CheckWeaponEquip(int id, out int index_)
    {
        UserInfo info = dataModel.ReadData<UserInfo>(DataPath.INFO);

        int i = info.guns_equip.IndexOf(id);
        if (i < 0)
        {
            index_ = -1;
            return false;
        }
        else
        {
            index_ = i + 1;
            return true;
        }
    }
    public void OnBuyShop(int id)
    {
        ConfigShopRecord cf = ConfigManager.instance.configShop.GetRecordByKeySearch(id);

        int num = dataModel.ReadData<int>(DataPath.CASH);
        num += cf.Value;
        dataModel.UpdateData(DataPath.CASH, num, null);
    }

    public void OnEquip(int id_wp, int slot)
    {

    }
    public void BuyWP(int id_wp, Action<WeaponData> callback)
    {
        ConfigWeaponRecord cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(id_wp, 1);
        int cash = GetCash();

        if (cf.Price <= cash)
        {
            WeaponData data = new WeaponData();
            data.id = id_wp;
            data.level = 1;
            dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, data.id.Tokey(), data);
            cash -= cf.Price;
            dataModel.UpdateData(DataPath.CASH, cash);
            callback(data);
        }
        else
        {
            callback(null);
        }
    }
    public void OnUpgrade(WeaponData data, Action<WeaponData> callback)
    {
        ConfigWeaponRecord cf_next_level = ConfigManager.instance.configWeapon.GetRecordByKeySearch(data.id, data.level);
        int cash = GetCash();
        if (cf_next_level != null)
        {
            if (cf_next_level.Price <= cash)
            {
                data.level++;
                dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, data.id.Tokey(), data);
                cash -= cf_next_level.Price;
                dataModel.UpdateData(DataPath.CASH, cash);
                callback(data);
            }
            else
            {
                callback(null);
            }
        }
        else
        {
            callback(null);
        }
    }
    public Dictionary<string, MissionData> GetMissionData()
    {
        return dataModel.ReadData<Dictionary<string, MissionData>>(DataPath.DIC_MISSION);
    }
}