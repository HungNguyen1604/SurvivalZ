using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager_KillAll : MissionManager
{
    public override void InitMission(ConfigMissionRecord cf)
    {
        base.InitMission(cf);

        Debug.LogError("MissionManager_KillAll");
        StartCoroutine("Init");
    }
    IEnumerator Init()
    {
        yield return new WaitForSeconds(1);

        wave_ids = cf_mission.Waves;
        StartCoroutine("CreateNewWave");

    }
    IEnumerator CreateNewWave()
    {
        index_wave++;
        number_enemy_dead_wave = 0;
        if (index_wave >= wave_ids.Count)
        {
            Debug.LogError("Mission complete!");
        }
        else
        {
            OnWaveChange?.Invoke(index_wave + 1, wave_ids.Count);
            cf_wave = ConfigManager.instance.configWave.GetRecordByKeySearch(wave_ids[index_wave]);
            yield return new WaitForSeconds(cf_wave.Delay);

            List<int> id_enemies = cf_wave.Enemies;
            for (int i = 0; i < cf_wave.Total; i++)
            {
                CreateEnemy(id_enemies.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
                yield return new WaitForSeconds(cf_wave.Time_rate);
            }
        }
    }

    private void CreateEnemy(int id_Enemy)
    {
        ConfigEnemyRecord cf_enemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(id_Enemy);
        GameObject go_enemy = Instantiate(Resources.Load("Enemies/" + cf_enemy.Prefab, typeof(GameObject))) as GameObject;
        EnemyControl enemyControl = go_enemy.GetComponent<EnemyControl>();

        enemyControl.Setup(cf_enemy);
    }

    public override void EnemyOnDead(EnemyControl enemyControl)
    {
        base.EnemyOnDead(enemyControl);
        number_enemy_dead_wave++;
        if (number_enemy_dead_wave >= cf_wave.Total)
        {
            StopCoroutine("CreateNewWave");
            StartCoroutine("CreateNewWave");
        }
    }
}
