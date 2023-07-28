using originalTimer;
using UnityEngine;
using System.Collections;
using System;


public class NpcBase : MonoBehaviour, IInteractable
{
    [SerializeField] private string _seedType;
    [SerializeField] private float _duration = 2f;

    public Timer timer;

    public int id;

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
     
        StartCoroutine("StartTimer", new Timer(_duration));
    }

    public void Action(int id)
    {
        Debug.Log("<color=blue> Seedtype :</color>" + _seedType);
    }

    public void onEnter()
    {
        StartCoroutine("StartTimer", new Timer(_duration));
    }

    public void onExit()
    {
        StopCoroutine("StartTimer");
    }

}
