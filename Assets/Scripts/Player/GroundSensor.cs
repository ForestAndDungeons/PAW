using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor
{
    float _radius;
    bool _isGrounded;
    LayerMask _groundLayer;
    Transform _transform;

    //Contructor; Player instancia esta clase y le pasa los parametros.
    public GroundSensor(float radius, LayerMask groundLayer, Transform transform)
    {
        _radius = radius;
        _groundLayer = groundLayer;
        _transform = transform;
    }

    public bool isGroundedGetter()
    {
        return _isGrounded;
    }

    //Update artificial
    public void GroundSensorUpdate()
    {
        //Crea una esfera como trigger y guarda colliders que la toquen o esten adentro.
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