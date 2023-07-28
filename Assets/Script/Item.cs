using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using originalTimer;
using System;
using Project.Data;


public class Item : MonoBehaviour, IInteractable
{ 
    [SerializeField]
    private ItemData _itemData;
    [SerializeField] 
    private float _duration = 1f;
    private Inventory _inventory;
    [SerializeField]
    private float _amplitude;
    [SerializeField]
    private float _animSpeed;

    public void Start()
    {
        _inventory = Inventory.instance;
    }

    public void Update()
    {
        transform.localPosition += new Vector3(0, GetSine(), 0);
    }

    public float GetSine()
    {
       return Mathf.Sin(Time.time * _animSpeed) * _amplitude;
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
        Action(_itemData.id);
        yield return new WaitForSeconds(1);

        yield return 39;
    }
        

    public void onEnter()
    {
        StartCoroutine("StartTimer", new Timer(_duration));
    }

    public void onExit()
    {
        StopCoroutine("StartTimer");
    }
    public void Action(int id)
    {
        
        if (_inventory.hasItem != true && id == _itemData.id)
        {
            _inventory.hasItem = true;  
        }else _inventory.RemoveItem();
        _inventory.AddItem(_itemData);     

        Destroy(gameObject);
    }


}
