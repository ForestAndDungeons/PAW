using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor
{
    float _radius;
    public bool _isGrounded;
    LayerMask _groundLayer;
    Transform _transform;

    public GroundSensor(float radius, LayerMask groundLayer, Transform transform)
    {
        _radius = radius;
        _groundLayer = groundLayer;
        _transform = transform;
    }

//Update artificial
    public void GroundSensorUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(this._transform.position, _radius, _groundLayer);

        if (colliders.Length > 0)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
}