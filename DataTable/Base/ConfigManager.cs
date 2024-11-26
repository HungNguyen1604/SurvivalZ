using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BySingleton<ConfigManager>
{
    public ConfigWeapon configWeapon;
    public ConfigEnemy configEnemy;
    public ConfigWave configWave;
    public ConfigMission configMission;
    public ConfigDefault configDefault;
    public ConfigShop configShop;

    public void InitConfig(Action callback)
    {
        StartCoroutine("LoadLocal", callback);
    }

    private IEnumerator LoadLocal(Action callback)
    {
        configWeapon = Resources.Load("DataTable/ConfigWeapon", typeof(ScriptableObject)) as ConfigWeapon;
        yield return new WaitUntil(() => configWeapon != null);

        configEnemy = Resources.Load("DataTable/ConfigEnemy", typeof(ScriptableObject)) as ConfigEnemy;
        yield return new WaitUntil(() => configEnemy != null);

        configWave = Resources.Load("DataTable/ConfigWave", typeof(ScriptableObject)) as ConfigWave;
        yield return new WaitUntil(() => configWave != null);

        configMission = Resources.Load("DataTable/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);

        configDefault = Resources.Load("DataTable/ConfigDefault", typeof(ScriptableObject)) as ConfigDefault;
        yield return new WaitUntil(() => configDefault != null);

        configShop = Resources.Load("DataTable/ConfigShop", typeof(ScriptableObject)) as ConfigShop;
        yield return new WaitUntil(() => configShop != null);

        if (callback != null)
        {
            callback();
        }
    }
}
