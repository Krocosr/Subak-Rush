public abstract class Field

{
    private bool _isRootState = false;
    private FieldBase _ctx;
    private FieldManager _states;
    private Field _currentSuperState;
    private Field _currentSubState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected FieldBase Ctx { get { return _ctx; } }
    protected FieldManager States { get { return _states; } }

    public Field(FieldBase currentContext, FieldManager FieldStateFactory)
    {
        _ctx = currentContext;
        _states = FieldStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitStage();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }
    protected void SwitchState(Field newState)
    {
            ExitStage();

        newState.EnterState();

        _ctx.CurrentState = newState;

    }
}
