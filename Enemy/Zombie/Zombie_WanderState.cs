using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zombie_WanderState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
}

//private Transform trans_point;
/*public float speed_move = 1;

public Vector3 cur_point;

public Vector3 GetPoint()
{
    Vector2 p = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(2f, 10f);
    return parent.trans.position + new Vector3(p.x, 0, p.y);
}
public override void OnEnter()
{
    //***
    parent.agent_.Warp(parent.trans.position);
    cur_point = GetPoint();
    base.OnEnter();
    parent.time_delay_agent = 0;
    //trans_point = SceneConfig.instance.GetPointMove();
    parent.agent_.speed = 1;
    parent.agent_.isStopped = false;

}
public override void OnEnter(object data)
{
    //***
    parent.agent_.Warp(parent.trans.position);
    cur_point = GetPoint();
    parent.agent_.speed = 1;
    parent.agent_.isStopped = false;
    parent.time_delay_agent = 0;
    base.OnEnter(data);

}
public override void UpdateState()
{

    parent.agent_.SetDestination(cur_point);

    float dis = Vector3.Distance(parent.trans.position, cur_point);
    if (parent.agent_.remainingDistance <= 0.1f && parent.time_delay_agent > 0.5f)
    {
        cur_point = GetPoint();
    }
    parent.RotateAgent();
    parent.dataBinding.Speed = parent.agent_.velocity.magnitude;


    parent.time_delay_agent += Time.deltaTime;

    if (dis <= parent.range_Detect)
    {
        parent.time_delay_agent = 0;
        parent.GotoState(parent.chaseState);
    }*/
/*float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);
if (dis <= parent.range_Detect)
{
    parent.time_delay_agent = 0;
    parent.GotoState(parent.chaseState);
}
else
{
    if (parent.isMoveAgent)
    {
        parent.agent_.SetDestination(trans_point.position);//di vong qua chuong ngai vat tren navmesh

        if (parent.agent_.remainingDistance <= 0.1f && parent.time_delay_agent > 0.3f)// ve ra duong di con lai cua muc tieu dang den
        {
            trans_point = SceneConfig.instance.GetPointMove();
        }
        parent.dataBinding.Speed = parent.agent_.velocity.magnitude;
        //RotateAgent();
    }
    else
    {
        Vector3 dir = trans_point.position - parent.trans.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            //lam cho zombie xoay tu tu
            Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);
            parent.trans.localRotation = Quaternion.Slerp(parent.trans.localRotation, q, Time.deltaTime * 2);
        }
        parent.trans.Translate(Vector3.forward * Time.deltaTime * speed_move);
        parent.dataBinding.Speed = 1;
        if (Vector3.Distance(parent.trans.position, trans_point.position) <= 0.1f)
        {
            trans_point = SceneConfig.instance.GetPointMove();
        }
    }
}*/

/*public override void Exit()
{
    base.Exit();
    parent.agent_.isStopped = true;
}*/
