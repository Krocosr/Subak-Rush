using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {

        Debug.Log("<color=green>Idle!</color>");
        
    }
    public override void UpdateState() {
        CheckSwitchState();
        Ctx.Animator.SetFloat("Blend", Ctx.StartAnimTime = 0f, Ctx.StopAnimTime = 0.2f, Time.deltaTime);
    }
    public override void ExitStage() { }
    public override void CheckSwitchState() {
        if (Ctx.IsMoving && Ctx.IsRunInput)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsMoving)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState() { }
}
