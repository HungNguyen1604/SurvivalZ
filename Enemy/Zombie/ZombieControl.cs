using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EnemyControl
{

    public Zombie_AttackState attackState;
    public Zombie_ChaseState chaseState;
    public Zombie_WanderState wanderState;
    public Zombie_StartState startState;

    public ZombieDataBinding dataBinding;
    public Zombie_DeadState deadState;
    public float dot_attack = 0.8f;
    public override void Setup(ConfigEnemyRecord configEnemy)
    {
        base.Setup(configEnemy);


        attackState.parent = this;
        chaseState.parent = this;
        wanderState.parent = this;
        startState.parent = this;
        deadState.parent = this;

        agent_.updateRotation = false;
        GotoState(startState);


        StartCoroutine("LookCheckAttack");
    }
    public void RotateAgent()
    {
        //chia diem A->B thanh nhieu doan bang steeringTarget
        Vector3 dir = agent_.steeringTarget - trans.position;
        dir.Normalize();
        if (dir.magnitude > 0)//if nay de tranh gap loi vector is zero
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            trans.localRotation = Quaternion.Slerp(trans.localRotation, q, Time.deltaTime * 20);
        }
    }

    IEnumerator LookCheckAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;

            Vector3 target_character = characterControl.trans.position;
            float dis = Vector3.Distance(trans.position, target_character);
            Vector3 dir = target_character - trans.position;
            dir.Normalize();
            float dot = Vector3.Dot(trans.forward, dir);

            if (dis <= range_Detect && dot > dot_attack)
            {
                if (current_state == startState)
                    GotoState(chaseState);

            }
        }

    }
    public override void OnDamage(ArrowData arrowData)
    {
        if (hp <= 0)
            return;
        this.arrowdata = arrowData;
        hp = hp - arrowData.damage;
        if (hp <= 0)
        {
            hp = 0;
            if (current_state != deadState)
            {
                GotoState(deadState);
                Invoke("ImpactPhysic", 0.1f);
            }
        }
        base.OnDamage(arrowData);
    }
    public override void OnDamage(MacheteData macheteData)
    {
        if (hp <= 0)
            return;
        this.machetedata = macheteData;
        hp = hp - macheteData.damage;
        Debug.Log(damage);
        if (hp <= 0)
        {
            hp = 0;
            if (current_state != deadState)
            {
                GotoState(deadState);
            }
        }
        base.OnDamage(macheteData);
    }
    private void ImpactPhysic()
    {
        arrowdata.rig_body.AddForceAtPosition(arrowdata.force, arrowdata.point_impact, ForceMode.Impulse);
    }

}
