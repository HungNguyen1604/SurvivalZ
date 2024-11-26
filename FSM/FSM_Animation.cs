using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Animation : StateMachineBehaviour
{
    private FSM_System system;

    public float time_middle = 0.5f;
    private float time_count;
    private bool is_call;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        time_count = 0;
        is_call = false;
        if (system == null)
        {
            system = animator.GetComponent<FSM_System>();
        }
        system.OnAnimationEnter();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        time_count += Time.deltaTime;
        if (time_count >= time_middle && !is_call)
        {
            system.OnAnimationMiddle();
            is_call = true;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        system.OnAnimationExit();
    }
}
