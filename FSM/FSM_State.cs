using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_State
{
    public virtual void OnEnter()
    {

    }
    public virtual void OnEnter(object data)
    {

    }

    public virtual void UpdateState()
    {

    }
    public virtual void LateUpdateState()
    {

    }
    public virtual void FixedUpdateState()
    {

    }
    public virtual void Exit()
    {

    }
    #region Animation
    public virtual void OnAnimationEnter()
    {

    }
    public virtual void OnAnimationMiddle()
    {

    }
    public virtual void OnAnimationExit()
    {

    }
    #endregion
}
