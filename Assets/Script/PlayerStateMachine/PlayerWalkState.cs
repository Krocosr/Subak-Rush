using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        Debug.Log("<color=green>Walk!</color>");
        Ctx.M_Speed = 7;
    }
    public override void UpdateState() {
        CheckSwitchState();
        Ctx.CharacterController.Move(Ctx.DesiredMoveDirection * Time.deltaTime * Ctx.M_Speed);
        Ctx.Animator.SetFloat("Blend", Ctx.StartAnimTime = 0.4f, Ctx.StopAnimTime = 0.2f, Time.deltaTime);
    }
    public override void ExitStage() { }
    public override void CheckSwitchState()
    {
        if (Ctx.IsRunInput) { 
         SwitchState(Factory.Run());
        }
        else if (!Ctx.IsMoving)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState() { }
}
