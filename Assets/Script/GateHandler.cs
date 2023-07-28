using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHandler : MonoBehaviour
{
    public static GateHandler instance;

    public bool _hasWater;
    public int id;
    public bool HasWater { get { return _hasWater; } set { _hasWater = value; } }

    void Awake() => instance = this;

}
