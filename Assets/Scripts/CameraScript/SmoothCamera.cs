using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    Vector3 _offset;
    [SerializeField] Transform _targetP1;
    [SerializeField] Transform _targetP2;
    Transform _actualTarget;
    [SerializeField] float _smooth;
    Vector3 _CurrentVelocity = Vector3.zero;
    [SerializeField]Player pj;

    private void Awake()
    {
        _actualTarget = _targetP1;
        _offset = transform.position - _actualTarget.position;
    }

    private void LateUpdate()
    {
            Vector3 targetPos = _actualTarget.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _CurrentVelocity, _smooth);

    }

    private void Update()
    {
        if (!pj.isActiveAndEnabled)
        {
            _actualTarget = _targetP2;
        }
    }

}
