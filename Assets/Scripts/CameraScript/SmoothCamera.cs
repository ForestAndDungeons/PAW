using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    Vector3 _offset;
    [SerializeField] Transform _target;
    [SerializeField] float _smooth;
    Vector3 _CurrentVelocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _CurrentVelocity, _smooth);
    }
}
