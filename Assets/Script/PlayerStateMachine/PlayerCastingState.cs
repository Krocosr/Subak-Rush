using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingState : PlayerBaseState
{
    public PlayerCastingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory) {
            IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("Casting!");

    }
    public override void UpdateState()
    {
        CheckSwitchState();


    }
    public override void ExitStage() {}
    public override void CheckSwitchState()
    {

    }
    public override void InitializeSubState() {
        if (!Ctx.IsMoving && !Ctx.IsRunInput)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMoving && !Ctx.IsRunInput)
        {
            SwitchState(Factory.Walk());
        }
        else
        {
            SwitchState(Factory.Run());
        }
    }
}
