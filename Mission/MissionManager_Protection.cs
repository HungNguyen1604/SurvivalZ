using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager_Protection : MissionManager
{

    public override void InitMission(ConfigMissionRecord cf)
    {
        base.InitMission(cf);
        Debug.LogError("MissionManager_Protection");
    }
}
