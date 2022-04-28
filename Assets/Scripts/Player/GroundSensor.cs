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

    //Update artificial
    public bool GroundSensorUpdate()
    {
        //Crea una esfera como trigger y guarda colliders que la toquen o esten adentro.
        Collider[] colliders = Physics.OverlapSphere(this._transform.position, _radius, _groundLayer);

        if (colliders.Length > 0)
        {
            return _isGrounded = true;
        }
        else
        {
            return _isGrounded = false;
        }
    }
}