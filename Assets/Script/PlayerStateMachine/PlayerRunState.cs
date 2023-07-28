using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        Debug.Log("Run!");
        Ctx.M_Speed = 3.2f;
    }
    public override void UpdateState() { 
        
        CheckSwitchState();
        Ctx.CharacterController.Move(Ctx.DesiredMoveDirection * Time.deltaTime * Ctx.M_Speed);
        Ctx.Animator.SetFloat("Blend", Ctx.StartAnimTime = 1f, Ctx.StopAnimTime = 0.5f, Time.deltaTime);
    }
    public override void ExitStage() { }
    public override void CheckSwitchState() {
        if (Ctx.IsMoving && !Ctx.IsRunInput)
        {
            SwitchState(Factory.Walk());
        }
        else if (!Ctx.IsMoving)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState() { }
}
