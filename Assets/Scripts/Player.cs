using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

//Variables para Movement.
    [SerializeField] float speed;
    [SerializeField] float forceJump;
    [SerializeField] Rigidbody myRigidBody;

//Variables para GroundSensor.
    [SerializeField] float radius = 0.1f;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;

//Clases.
    Control _control;
    Movement _movement;
    GroundSensor _groundSensor;

//Instancia las clases y le pasa los parametros.
    private void Start()
    {
        _movement = new Movement(speed, forceJump, myRigidBody, transform);
        _control = new Control(_movement, transform);
        _groundSensor = new GroundSensor(radius, groundLayer, transform);
    }

//Llama al metodo de Update artificial.
    void Update()
    {
        isGrounded = _groundSensor._isGrounded;

        _groundSensor.GroundSensorUpdate();
        //_control.MovementUpdate(isGrounded);

        _control.IsometricMovement(isGrounded);
    }
}
