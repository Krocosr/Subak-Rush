using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField]
    private float _smoothTime = .5f;
    [SerializeField]
    private Vector3 p_pos;
    private float _offsetZ = 8f;
    [SerializeField]
    private float _offsetY;
    [SerializeField]
    private Transform _target;

    private void FixedUpdate()
    {
        camSmoothen();
    }

    void camSmoothen()
    {
        p_pos.x = 0;
        p_pos.z = -_offsetZ;
        p_pos.y = _offsetY;

        _offset = new Vector3(p_pos.x, p_pos.y, p_pos.z);
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _smoothTime);

    }
}
