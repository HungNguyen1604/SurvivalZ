using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionManager : BySingleton<MissionManager>
{
    public ConfigMissionRecord cf_mission;
    public int index_wave = -1;
    public List<int> wave_ids;
    public int number_enemy_dead_wave;
    public ConfigWaveRecord cf_wave;

    public UnityEvent<int, int> OnWaveChange;
    public virtual void InitMission(ConfigMissionRecord cf)
    {
        cf_mission = cf;
        //create player
        GameObject logic = Instantiate(Resources.Load("Player/Logic", typeof(GameObject))) as GameObject;
        logic.transform.position = SceneConfig.instance.player_pos.position;
    }

    public virtual void OnObjectCollect(object data)
    {

    }
    public virtual void EnemyOnDead(EnemyControl enemyControl)
    {

    }
    private void OnDisable()
    {
        OnWaveChange.RemoveAllListeners();
    }
}
