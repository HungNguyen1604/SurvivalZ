using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_System : MonoBehaviour
{
    public FSM_State current_state;
    public void GotoState(FSM_State newState)
    {
        if (current_state != null)
        {
            current_state.Exit();
        }
        current_state = newState;
        current_state.OnEnter();
    }
    public void GotoState(FSM_State newState, object data)
    {
        current_state?.Exit();
        current_state = newState;
        current_state.OnEnter();
    }

    public void OnAnimationEnter()
    {
        current_state?.OnAnimationEnter();
    }
    public void OnAnimationMiddle()
    {
        current_state?.OnAnimationMiddle();
    }
    public void OnAnimationExit()
    {
        current_state?.OnAnimationExit();
    }
    public virtual void Update()
    {
        current_state?.UpdateState();
    }

    public virtual void LateUpdate()
    {
        current_state?.LateUpdateState();
    }
    public virtual void FixedUpdate()
    {
        current_state?.FixedUpdateState();
    }
}
