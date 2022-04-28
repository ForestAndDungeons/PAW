using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool player2;

    //Variables para Movement.
    [SerializeField] float speed;
    [SerializeField] float forceJump;
    [SerializeField] Rigidbody myRigidBody;

    //Variables para GroundSensor.
    [SerializeField] float radius = 0.1f;
    [SerializeField] bool _isGrounded;
    [SerializeField] LayerMask groundLayer;

    //Variables para PlayerBase.
    [SerializeField] string _name;
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;
    [SerializeField] int _attackPower;
    [SerializeField] int _armor;

    //Variables de Clases.
    Control _control;
    Movement _movement;
    GroundSensor _groundSensor;
    [HideInInspector] public PlayerBase _playerBase;

    //Instancia las clases y le pasa los parametros.
    private void Start()
    {
        _movement = new Movement(speed, forceJump, myRigidBody, transform);
        
        if (!player2)
            _control = new Control(_movement, transform, "Vertical", "Horizontal", player2);
        else
            _control = new Control(_movement, transform, "Vertical2", "Horizontal2", player2);
            
        _groundSensor = new GroundSensor(radius, groundLayer, transform);

        _playerBase = new PlayerBase(_name, _maxHealth, _attackPower, _armor);

    }

    //Llama a metodos de Artificial Updates.
    void Update()
    {
        //Mantiene actualizado los datos de las variables.
        _isGrounded = _groundSensor.isGroundedGetter();
        _currentHealth = _playerBase.currentHealthGetter();

        _groundSensor.GroundSensorUpdate();

        _control.IsometricMovement(_isGrounded);

        //Alternativa de movimiento.
        //_control.MovementUpdate(isGrounded);
    }
}
