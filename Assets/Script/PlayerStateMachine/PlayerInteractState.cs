using UnityEngine;

public class PlayerInteractState : PlayerBaseState
{

    public PlayerInteractState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    private NpcBase npc;

    public override void EnterState() {
        Debug.Log("<color=green>Interact!</color>");
    }

    public override void UpdateState() { 
        CheckSwitchState();
    }

    public override void ExitStage() 
    {
        if (!Ctx.IsMoving)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void CheckSwitchState()
    {
        if (Ctx.IsMoving) { 
         SwitchState(Factory.Walk());
        // npc.CancelAction();
        }
    }
    public override void InitializeSubState() { 
    
    
    }
}
