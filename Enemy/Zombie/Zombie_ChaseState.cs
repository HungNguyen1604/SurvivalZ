using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Zombie_ChaseState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;

    public override void OnEnter()
    {
        parent.agent_.Warp(parent.trans.position);
        parent.agent_.stoppingDistance = parent.range_Attack;
        base.OnEnter();

    }

    public override void UpdateState()
    {
        parent.time_delay_agent += Time.deltaTime;

        Vector3 cur_point = parent.characterControl.trans.position;
        float dis = Vector3.Distance(parent.trans.position, cur_point);


        parent.agent_.SetDestination(cur_point); // Đặt đích đến



        parent.dataBinding.Speed = parent.agent_.velocity.magnitude;
        parent.RotateAgent();

        if (dis <= parent.range_Attack)
        {
            parent.GotoState(parent.attackState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        parent.dataBinding.Speed = 0;
    }
}
