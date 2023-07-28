public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        :base (currentContext, playerStateFactory) {
        IsRootState = true; 
    }
    public override void EnterState() {
        InitializeSubState();
    }
    public override void UpdateState() { 
        CheckSwitchState(); }
    public override void ExitStage() { }
    public override void CheckSwitchState() {
    } 
    public override void InitializeSubState() { 
    if (!Ctx.IsMoving && !Ctx.IsRunInput)
        {
            SetSubState(Factory.Idle());
        }
    else if (Ctx.IsMoving && !Ctx.IsRunInput)
        {
            SetSubState(Factory.Walk());
        } 
    else if (Ctx.IsInteracting)
        {
            SetSubState(Factory.Interact());
        } 
    else
        {
            SetSubState(Factory.Run());
        }
    }

}
