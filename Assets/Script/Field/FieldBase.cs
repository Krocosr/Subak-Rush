using Project.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using originalTimer;

public class FieldBase : MonoBehaviour, IInteractable
{
    
    Field _currentState;
    FieldManager _states;
    GateHandler _gateHandler;
    Inventory _itemType;
    private Transform _plowObj;

    private float _duration = 3f; 
    public int id;

    //Boolean
    private bool _isPlowed;
    private bool _isPlanted;
    private bool _isCounting;
    private bool _HasWater;

    #region Menangis
    public Field CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Transform PlowObj { get { return _plowObj = GetComponentInChildren<Transform>().Find("ladang_bump"); } }
    public bool HasSeed { get { return _hasSeed(); } }
    public bool HasPacul { get { return _hasPacul(); } }
    public bool IsPlowed { get { return _isPlowed; } set { _isPlowed = value; } }
    public bool IsCounting { get { return _isCounting; } }
    public bool IsPlanted { get { return _isPlanted; } }
    public bool HasWater { get { return _HasWater; } }
    public float Duration { get { return _duration; } set { _duration = value; } }
    protected int Id { get { return id; } }
    #endregion

    private void Awake()
    {
        // state setup
        _states = new FieldManager(this);
        _currentState = _states.Soil();
        _currentState.EnterState();
    }

    private void Start()
    {
        // Script Setup
        _itemType = Inventory.instance;
        _gateHandler = gameObject.GetComponentInParent<GateHandler>();
        _HasWater = _gateHandler.HasWater;
    }

    private void Update()
    {
        _currentState.UpdateState();
    }

    public bool _hasSeed()
    {
        if (_itemType.ItemData._itemType == "Seeds")
        {
            return true;
        }
        else return false;
    }

    public bool _hasPacul()
    {
        if (_itemType.ItemData._itemType == "Tools")
        {
            return true;
        }
        else return false;
    }

    IEnumerator StartTimer(Timer timer)
    {
        timer.isCounting = true;
        while (timer.isCounting)
        {
            timer.DecrementTimer(Time.deltaTime);
            Debug.Log(Math.Round(timer.RemainingSeconds));
            yield return null;
        }
        Action(id);
        yield return new WaitForSeconds(1);

        yield return 70;
    }

    public void onEnter()
    {
        if (_itemType.hasItem == true && (!_isPlowed || !_isPlanted))
        {
            StartCoroutine("StartTimer", new Timer(_duration));
        }
        else
        {
            Debug.Log("<color=red>NeedItem!</color>");
        }

    }
    public void onExit()
    {
        StopCoroutine("StartTimer");
    }

    public void Action(int id)
    {
        if (id == this.id)
        {
            if (_hasPacul() == true && !_isPlowed)
            {
                Debug.Log("Plowed");
                _isPlowed = true;
            }
            else if (_hasSeed() == true && _isPlowed && !_isPlanted)
            {
                Debug.Log("Planted");
                _isPlanted = true;
            }
        }
    }
}
