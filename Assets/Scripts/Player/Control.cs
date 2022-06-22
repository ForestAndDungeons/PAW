using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control 
{
    //Class.
    Movement _movement;
    PlayerSoundManager _playerSoundManager;

    Transform _transform;

    //Save name that Class Player gives, sets if it's player1 or player2.
    string _verticalAxis;
    string _horizontalAxis;

    public float _verticalInput;
    public float _horizontalInput;

    SKeyCode[] _sKeyCode;

    //Isometric variables.
    Vector3 _input;
    float _turnSpeed;
    bool _isDead;
    bool _canMove;

    AnimationController _animationController;

    //Contructor; Player instancia esta clase y le pasa los parametros.
    public Control(Movement movement, Transform transform, string verticalAxis, string horizontalAxis, float turnSpeed, SKeyCode[] sKeyCode, PlayerSoundManager playerSoundManager)
    {
        _movement = movement;
        _transform = transform;
        _verticalAxis = verticalAxis;
        _horizontalAxis = horizontalAxis;
        _turnSpeed = turnSpeed;
        _sKeyCode = sKeyCode;
        _playerSoundManager = playerSoundManager;
    }

    //Update artificial, se llama en el Update de Player.

    public void Movements(bool isGrounded)
    {
        _input = new Vector3 (Input.GetAxisRaw(_horizontalAxis), 0, Input.GetAxisRaw(_verticalAxis)).normalized;
        _verticalInput = Input.GetAxis(_verticalAxis);
        _horizontalInput = Input.GetAxis(_horizontalAxis);

        if (_input != Vector3.zero)
        {
            //Matriz offset con un angulo de 45 grados para que los ejes cartesianos coincidan con la camara isometrica.
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);

            var relative = (_transform.position + skewedInput) - _transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

        foreach (SKeyCode keyCode in _sKeyCode)
        {
            if (Input.GetKeyDown(keyCode.key))
            {
                keyCode.method.Invoke();
            }
        }
    }

    public void IsometricMovement()
    {
        _movement.IsometricMove(_input);
    }
}