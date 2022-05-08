using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    
    [SerializeField] Transform _targetP1;
    [SerializeField] Transform _targetP2;
    Transform _actualTarget;
    Vector3 _offset;
    [SerializeField] float _smoothSpeed;
    Vector3 _CurrentVelocity = Vector3.zero;

    private void Awake()
    {
        _actualTarget = _targetP1;
        _offset = transform.position - _actualTarget.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = _actualTarget.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _CurrentVelocity, _smoothSpeed * Time.deltaTime);

    }

    private void Update()
    {
        if (!_targetP1)
        {
            _actualTarget = _targetP2;
        }
    }
}