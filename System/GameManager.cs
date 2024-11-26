using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BySingleton<GameManager>
{
    public ConfigMissionRecord cur_cf_Mission;

    public void InitMission(ConfigMissionRecord cur_cf_Mission)
    {
        this.cur_cf_Mission = cur_cf_Mission;

        GameObject mission_manager = Instantiate(Resources.Load("Mission/MissionManager_" + cur_cf_Mission.Mission_type.ToString(), typeof(GameObject))) as GameObject;
        MissionManager.instance.InitMission(cur_cf_Mission);
    }
}
