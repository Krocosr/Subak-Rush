using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField]
    private List<GateHandler> _gateManager;
    [SerializeField]
    private List<GameObject> _gateState = new List<GameObject>();
    [SerializeField]
    private List<Transform> _waterObj;

    public int id;



    public void Start()
    {
        newPos = oldPos + addPos;
        StartCoroutine("GetChild");
    }

    IEnumerator GetChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObj = transform.GetChild(i).gameObject;
            _gateState.Add(childObj);
            yield return null;
        }
        StopCoroutine("GetChild");
    }

    public void onEnter() 
    {
        Action(id);

    }
    public void onExit() 
    { 
    
    }

    public void Action(int id) 
    {
        if (id == 0)
        {
            _gateManager[0].HasWater = !_gateManager[0].HasWater;
            _gateState[0].SetActive(false);
            _gateState[1].SetActive(true);
            Debug.Log(_gateManager[0].HasWater);
            StartCoroutine("WaterFill", id);

            this.id = 3;
        }
        if (id == 1 && _gateManager[0].HasWater == true)
        {
            _gateState[0].SetActive(false);
            _gateState[1].SetActive(true);
            foreach (GateHandler gate in _gateManager) {
                gate.HasWater = !gate.HasWater;
                Debug.Log("Switched");
                Debug.Log(gate.HasWater);
            }
            StartCoroutine("WaterFill", id);
            this.id = 3;
        }
    }


    private Vector3 oldPos;
    private Vector3 addPos = new Vector3(0, -6f, 0);
    private Vector3 newPos;


    IEnumerator WaterFill(int id)
    {
        Transform descend;
        Transform childObj;
        id = this.id;

        for (int i = 0; i < _gateManager[id].transform.childCount; i++)
        {
            childObj = _gateManager[id].transform;
            
            descend = childObj.GetChild(i).Find("water");
            _waterObj.Add(descend);
            yield return null;
  
        }
        foreach (Transform child in _waterObj)
        {
            child.transform.localPosition = Vector3.Lerp(oldPos, newPos, Time.fixedDeltaTime* .5f);

         yield return new WaitForSeconds(.05f);
        }
        Debug.Log("Coroutine Stoped!");

        StopCoroutine("WaterFill");
    }

    IEnumerator WaterDrain()
    {
        foreach (Transform child in _waterObj)
        {
            child.transform.localPosition = Vector3.Lerp(newPos, oldPos, Time.fixedDeltaTime * .5f);

            yield return new WaitForSeconds(.05f);
        }
        StopCoroutine("WaterDrain");
    }
}
