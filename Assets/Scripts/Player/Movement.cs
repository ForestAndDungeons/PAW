using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement 
{
    float _speed;
    float _forceJump;

    Rigidbody _myRigidBody;
    Transform _transform;

    //Contructor; Player instancia esta clase y le pasa los parametros.
    public Movement(float speed, float forceJump, Rigidbody myRigidBody, Transform transform)
    {
        _speed = speed;
        _forceJump = forceJump;
        _myRigidBody = myRigidBody;
        _transform = transform;
    }

    //La clase Control llama a estos metodos y pasa los parametros de Input.
    public void Move(float verticalInput, float horizontalInput)
    {
        var direction = (_transform.forward * verticalInput) + (_transform.right * horizontalInput);

        _transform.position += direction * _speed * Time.deltaTime;
    }

    public void Jump()
    {
        _myRigidBody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
    }

    public void IsometricMove(Vector3 input)
    {
        _myRigidBody.MovePosition(_transform.position + (_transform.forward * input.magnitude)* _speed * Time.deltaTime);
    }
}
