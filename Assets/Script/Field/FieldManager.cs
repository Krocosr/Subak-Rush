
using System.Collections.Generic;
using UnityEngine;
using originalTimer;
using UnityEngine.UIElements;
using System;
using System.ComponentModel;

enum FieldStates
{
    BaseState,
    SoilState,
    PlowedState,
    PlantedState,
    BloomState,
}

public class FieldManager
{

    FieldBase _context;
    Dictionary<FieldStates, Field> _states = new Dictionary<FieldStates, Field>();
    public FieldManager(FieldBase currentContext)
    {
        _context = currentContext;
        _states[FieldStates.BaseState] = new FieldBaseState(_context, this);
        _states[FieldStates.SoilState] = new FieldSoilState(_context, this);
        _states[FieldStates.PlowedState] = new FieldPlowedState(_context, this);
        _states[FieldStates.PlantedState] = new FieldPlantedState(_context, this);
        _states[FieldStates.BloomState] = new FieldBloomState(_context, this);

    }
    public Field Base() { return _states[FieldStates.BaseState]; }
    public Field Soil() { return _states[FieldStates.SoilState]; }
    public Field Plowed() { return _states[FieldStates.PlowedState]; }
    public Field Planted() { return _states[FieldStates.PlantedState]; }
    public Field Bloom() { return _states[FieldStates.BloomState]; }

}

public class FieldBaseState : Field
{
    public FieldBaseState(FieldBase currentContext, FieldManager FieldStateFactory) 
    : base(currentContext, FieldStateFactory) 
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitStage() { }
    public override void CheckSwitchState()  
    {
        if (!Ctx.IsPlowed && !Ctx.IsPlanted)
        {
            SwitchState(States.Soil());
        }
    }
    public override void InitializeSubState()
    {
    }

}

public class FieldSoilState : Field
{
    public FieldSoilState(FieldBase currentContext, FieldManager FieldStateFactory) : base(currentContext, FieldStateFactory)
    {
    }
    public override void EnterState()
    {
        Vector3 _transformBaseValue = new Vector3 (0,-0.05f,0);
        Ctx.PlowObj.transform.localPosition = _transformBaseValue;
        Ctx.IsPlanted = false;
        Ctx.IsPlowed = false;

        Debug.Log("SoilState"); 
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitStage() { }
    public override void CheckSwitchState()
    {
        if (Ctx.IsPlowed && !Ctx.IsPlanted)
        {
            SwitchState(States.Plowed());
        }
    }
    public override void InitializeSubState()
    {
    }
}


public class FieldPlowedState : Field
{
    public FieldPlowedState(FieldBase currentContext, FieldManager FieldStateFactory) : base(currentContext, FieldStateFactory) 
    {
    }

    Vector3 oldPos;
    Vector3 addPos = new Vector3 (0, 0.07f, 0);
    Vector3 newPos;
    float speed;

    public override void EnterState() {

        oldPos = Ctx.PlowObj.transform.localPosition;
        newPos = oldPos + addPos;

        Debug.Log("\"<color=blue>PlowedState!</color>\"");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        
        while (speed < .5f) 
        {
        speed += Time.deltaTime * .04f;
        Ctx.PlowObj.transform.localPosition = Vector3.Lerp(oldPos, newPos, speed);
        }

    }
    public override void ExitStage() { }
    public override void CheckSwitchState() 
    {
        if (Ctx.IsPlowed && Ctx.IsPlanted)
        {
            SwitchState(States.Planted());
        }
    }
    public override void InitializeSubState() 
    {
    }
}

public class FieldPlantedState : Field
{
    public FieldPlantedState(FieldBase currentContext, FieldManager FieldStateFactory) : base(currentContext, FieldStateFactory)
    {}
    Timer timer;

    public override void EnterState() 
    {
        timer = new Timer(3);
        timer.isCounting = true; 
        Debug.Log("PlantedState");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        while (timer.isCounting)
        {
            timer.DecrementTimer(Time.deltaTime);
            Debug.Log(Math.Round(timer.RemainingSeconds));
            return;
        }
    }
    public override void ExitStage() { }
    public override void CheckSwitchState()
    {
        if (!timer.isCounting && !Ctx.HasWater)
        {
            SwitchState(States.Soil());
        }
        else if (!timer.isCounting && Ctx.HasWater) {
            SwitchState(States.Bloom());
        }
    }
    public override void InitializeSubState() { }
    }



public class FieldBloomState : Field
{
    public FieldBloomState(FieldBase currentContext, FieldManager FieldStateFactory) : base(currentContext, FieldStateFactory) { }

    GameObject _seedType;
    public override void EnterState()
    {
        Debug.Log("Blooming");
        _seedType = Ctx.SeedType;
        Debug.Log(_seedType);
        FieldBase.IntantiatePrefab(_seedType, Ctx.transform.position);
    }
    public override void UpdateState() 
    {
        CheckSwitchState();
    }
    public override void ExitStage() { }
    public override void CheckSwitchState() { }
    public override void InitializeSubState() { }


}
