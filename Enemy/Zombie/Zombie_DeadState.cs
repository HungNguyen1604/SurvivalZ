using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zombie_DeadState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    public override void OnEnter()
    {
        base.OnEnter();
        parent.dataBinding.Dead = true;
        parent.OnDead();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
