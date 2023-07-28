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
        if (id == 1)
        {
            _gateManager[0].HasWater = !_gateManager[0].HasWater;
            _gateState[0].SetActive(false);
            _gateState[1].SetActive(true);
            Debug.Log(_gateManager[0].HasWater);
            StartCoroutine("WaterDrain", id);

            this.id = 2;


            this.enabled = false;
        }
        if (id == 2 && _gateManager[0].HasWater == true)
        {
            _gateState[0].SetActive(false);
            _gateState[1].SetActive(true);
            foreach (GateHandler gate in _gateManager) {
                gate.HasWater = !gate.HasWater;
                Debug.Log("Switched");
                Debug.Log(gate.HasWater);
            }
        }
    }


    private Vector3 oldPos;
    private Vector3 addPos = new Vector3(0, -6f, 0);
    private Vector3 newPos;


    IEnumerator WaterDrain()
    {
        Transform descend;
        Transform childObj;

        for (int i = 0; i < _gateManager[0].transform.childCount; i++)
        {
            childObj = _gateManager[0].transform;
            
            descend = childObj.GetChild(i).Find("water");
            _waterObj.Add(descend);
            Debug.Log(childObj);
            yield return null;
  
        }
        foreach (Transform child in _waterObj)
        {
            child.transform.localPosition = Vector3.Lerp(oldPos, newPos, Time.fixedDeltaTime* .5f);

         yield return new WaitForSeconds(.1f);
        }
        

        Debug.Log("Coroutine Stoped!");
        StopCoroutine("WaterDrain");

    }
}
