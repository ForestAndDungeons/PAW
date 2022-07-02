using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    MovementSO _data;
    Rigidbody _myRigidBody;
    Transform _transform;
    float _speed;
    float _speedNormal;
    float _forceJump;

    //Contructor; Player instancia esta clase y le pasa los parametros.
    public Movement(MovementSO dataMovement, Rigidbody myRigidBody, Transform transform)
    {
        _data = dataMovement;
        _speed = _data.speed;
        _forceJump = _data.forceJump;
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
        _myRigidBody.MovePosition(_transform.position + (_transform.forward * input.magnitude) * _speed * Time.deltaTime);
    }

    public void SetSpeedBonus(float add)
    {
        _speed += add;
    }

    public void SetSlowSpeed()
    {
        _speedNormal = _speed;
        _speed = _speed * 0.5f;
    }

    public void SetNormalSpeed()
    {
        _speed = _speedNormal;
    }

    public void SetForceJump(float forceJump)
    {
        _forceJump = forceJump;
    }

    public float GetForceJump()
    {
        return _forceJump;
    }
}