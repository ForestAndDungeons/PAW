using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] bool player2;

    //Movement Variables.
    [Header("Movement Variables")]
    [SerializeField] float speed;
    [SerializeField] float forceJump;
    [SerializeField] float _turnSpeed;
    [SerializeField] Rigidbody myRigidBody;

    //Control Variables.
    [Header("Control Variables")]
    [SerializeField] string _verticalAxis;
    [SerializeField] string _horizontalAxis;
    [SerializeField] KeyCode _keyJump;
    [SerializeField] KeyCode _keyAttack;

    //GroundSensor Variables.
    [Header("GroundSensor Variables")]
    [SerializeField] float radius;
    [SerializeField] bool _isGrounded;
    [SerializeField] LayerMask groundLayer;

    //PlayerBase Variables.
    [Header("PlayerBase Variables")]
    [SerializeField] string _name;
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;
    [SerializeField] int _attackPower;
    [SerializeField] int _armor;

    [Header("Sounds Manager")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _aClip;

    //AnimationController Variable.
    [SerializeField] Animator _myAnimator;

    //Clases Variables.
    Control _control;
    Movement _movement;
    GroundSensor _groundSensor;
    PlayerSoundManager _playerSoundManager;
    AnimationController _animationController;
    [HideInInspector] public PlayerBase _playerBase;

    //Instancia las clases y le pasa los parametros.
    private void Start()
    {
        _playerSoundManager = new PlayerSoundManager(_audioSource,_aClip);

        _movement = new Movement(speed, forceJump, myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_movement, transform, _verticalAxis, _horizontalAxis, _animationController, _turnSpeed, _keyJump, _keyAttack, _playerSoundManager);
            
        _groundSensor = new GroundSensor(radius, groundLayer, transform);

        _playerBase = new PlayerBase(_name, _maxHealth, _attackPower, _armor, _playerSoundManager , _aClip , _audioSource);
    }

    //Llama a metodos de Artificial Updates.
    public void Update()
    {
        //Mantiene actualizado los datos de las variables para verlos en Inspector y pasarlos como parametros.
        _currentHealth = _playerBase.currentHealthGetter();
        _isGrounded = _groundSensor.GroundSensorUpdate();
        

        _animationController.InputUpdate(_control._verticalInput, _control._horizontalInput);

        if (_currentHealth <= 0)
            this.gameObject.SetActive(false);

        _control.IsometricMovement(_isGrounded);
    }

    public void OnTriggerEnter(Collider other)
    {
        var pickUp = other.GetComponent<PickUp>();

        if (pickUp != null)
            pickUp.Pick(_playerBase);
    }

    public void SoundAttack()
    {
        _playerSoundManager.playOnAttack();
    }

    public void SoundHit()
    {
        _playerSoundManager.playOnHit();
    }

    public void EndAttack()
    {
        _myAnimator.SetBool("onAttack", false);
    }
}