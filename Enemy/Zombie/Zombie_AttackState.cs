using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zombie_AttackState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;

    public override void OnEnter()
    {
        base.OnEnter();
        parent.time_delay_agent = 0; // Khoi tao thoi gian delay agent
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // Kiem tra neu du thoi gian tan cong
        if (parent.time_count_attack >= parent.attack_speed)
        {
            parent.dataBinding.Attack = true; // Bat tinh nang tan cong
            parent.time_count_attack = 0; // Reset thoi gian tan cong
        }
    }
    public override void OnAnimationMiddle()
    {

        base.OnAnimationMiddle();

        float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);
        Vector3 dir = parent.characterControl.trans.position - parent.trans.position;
        float dot = Vector3.Dot(dir.normalized, parent.trans.forward);

        //truoc mat zombie danh thi moi mat hp
        if (dot > parent.dot_attack && dis <= parent.range_Attack)
        {
            parent.characterControl.OnDamage(parent.damage);
        }

    }
    public override void OnAnimationExit()
    {
        base.OnAnimationExit();
        float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);
        Vector3 dir = parent.characterControl.trans.position - parent.trans.position;
        float dot = Vector3.Dot(dir.normalized, parent.trans.forward);

        //truoc mat zombie danh thi moi mat hp
        if (dot > parent.dot_attack && dis <= parent.range_Attack)
        {
        }
        else
        {
            parent.GotoState(parent.chaseState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        parent.dataBinding.Attack = false; // Ngung tan cong khi thoat trang thai
    }
}
