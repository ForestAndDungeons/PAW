using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control 
{
    Movement _movement;

    Transform _transform;

    string _verticalAxis;
    string _horizontalAxis;

    bool _player2;

    //Isometric variables.
    Vector3 _input;
    float _turnSpeed = 360;

    //Contructor; Player instancia esta clase y le pasa los parametros.
    public Control(Movement movement, Transform transform, string verticalAxis, string horizontalAxis, bool player2)
    {
        _movement = movement;
        _transform = transform;
        _verticalAxis = verticalAxis;
        _horizontalAxis = horizontalAxis;
        _player2 = player2;

    }

//Update artificial, se llama en el Update de Player.
    public void MovementUpdate(bool isGrounded)
    {
        var verticalInput = Input.GetAxis(_verticalAxis);
        var horizontalInput = Input.GetAxis(_horizontalAxis);

        if (verticalInput != 0 && isGrounded || horizontalInput != 0 && isGrounded)
            _movement.Move(verticalInput, horizontalInput);
        
        if (Input.GetKeyDown(KeyCode.RightControl) && isGrounded && !_player2)
            _movement.Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && _player2)
            _movement.Jump();
    }

    public void IsometricMovement(bool isGrounded)
    {
        _input = new Vector3(Input.GetAxisRaw(_horizontalAxis), 0, Input.GetAxisRaw(_verticalAxis));
        _movement.IsometricMove(_input);

        if(_input != Vector3.zero)
        {
            //Matriz offset con un angulo de 45 grados para que los ejes cartesianos coincidan con la camara isometrica.
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);
            
            var relative = (_transform.position + skewedInput) -_transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.RightControl) && isGrounded && !_player2)
            _movement.Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && _player2)
            _movement.Jump();
    }
}
