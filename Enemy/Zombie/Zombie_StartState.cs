using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zombie_StartState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;

    private float rise_speed = 2; // toc do chui len
    private float rise_dis = 2; // khoang cach zombie chui len
    private Vector3 cur_pos; // vi tri goc cua zombie
    private bool rising = true; // kiem tra neu zombie dang chui len

    public override void OnEnter()
    {
        parent.agent_.enabled = false;
        int index = -1;
        Transform spawn_point = SceneConfig.instance.GetPointSpawn(out index);
        cur_pos = spawn_point.position;

        // Dat zombie duoi dat
        parent.trans.position = new Vector3(cur_pos.x, cur_pos.y - rise_dis, cur_pos.z);
        base.OnEnter();

    }

    public override void OnEnter(object data)
    {
        parent.agent_.enabled = false;

        int index;
        Transform spawn_point = SceneConfig.instance.GetPointSpawn(out index);
        cur_pos = spawn_point.position;

        // Dat zombie duoi dat
        parent.trans.position = new Vector3(cur_pos.x, cur_pos.y - rise_dis, cur_pos.z);

        base.OnEnter(data);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (rising)
        {
            // Di chuyen zombie tu duoi dat len
            Vector3 target_position = cur_pos;
            parent.trans.position = Vector3.MoveTowards(parent.trans.position, target_position, rise_speed * Time.deltaTime);

            // Xoay zombie quanh truc Y
            parent.trans.Rotate(Vector3.up, 360 * Time.deltaTime);
            // Kiem tra xem zombie da dat den vi tri dich chua
            if (Vector3.Distance(parent.trans.position, target_position) < 0.1f)
            {
                parent.GotoState(parent.chaseState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        parent.dataBinding.Speed = 0;
        parent.agent_.enabled = true;
        parent.agent_.stoppingDistance = 0;
    }
}
