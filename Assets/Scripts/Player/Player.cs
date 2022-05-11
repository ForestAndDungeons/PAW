using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //[SerializeField] bool player2;

    //Movement Variables.
    [Header("Movement Variables")]
    [SerializeField] float _speed;
    [SerializeField] float _forceJump;
    [SerializeField] float _turnSpeed;
    [SerializeField] Rigidbody _myRigidBody;

    //Control Variables.
    [Header("Control Variables")]
    [SerializeField] string _verticalAxis;
    [SerializeField] string _horizontalAxis;
    [SerializeField] KeyCode _keyJump;
    [SerializeField] KeyCode _keyAttack;

    //GroundSensor Variables.
    [Header("GroundSensor Variables")]
    [SerializeField] float _radius;
    [SerializeField] bool _isGrounded;
    [SerializeField] LayerMask _groundLayer;

    //PlayerBase Variables.
    [Header("PlayerBase Variables")]
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;
    [SerializeField] ParticleSystem _particleSystem;

    //PlayerSoundManager Variables.
    [Header("Sounds Manager")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    //UIPlayer Variables.
    [Header("UI Player")]
    [SerializeField] Image[] _hearts;
    [SerializeField] Sprite[] _spriteHeart;

    //AnimationController Variable.
    [SerializeField] Animator _myAnimator;

    //Clases Variables.
    Control _control;
    Movement _movement;
    GroundSensor _groundSensor;
    UIPlayer _uiPlayer;
    [HideInInspector] public PlayerSoundManager _playerSoundManager;
    [HideInInspector] public AnimationController _animationController;
    [HideInInspector] public PlayerBase _playerBase;

    public Weapon weapon;

    //Instancia las clases y le pasa los parametros.
    private void Start()
    {
        _playerSoundManager = new PlayerSoundManager(_audioSource,_audioClip);

        _movement = new Movement(_speed, _forceJump, _myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_movement, transform, _verticalAxis, _horizontalAxis, _animationController, _turnSpeed, _keyJump, _keyAttack, _playerSoundManager);
            
        _groundSensor = new GroundSensor(_radius, _groundLayer, transform);

        _playerBase = new PlayerBase(_name, _maxHealth, _attackPower, _armor, _playerSoundManager , _audioClip , _audioSource, _particleSystem);

        _uiPlayer = new UIPlayer(_hearts, _spriteHeart);
    }

    //Llama a metodos de Artificial Updates.
    private void Update()
    {
        //Mantiene actualizado los datos de las variables para verlos en Inspector y pasarlos como parametros.
        _currentHealth = _playerBase.currentHealthGetter();
        _attackPower = _playerBase.attackPowerGetter();
        _armor = _playerBase.armorGetter();
        _isGrounded = _groundSensor.GroundSensorUpdate();

        _animationController.InputUpdate(_control._verticalInput, _control._horizontalInput);

        _uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
            this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
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

    public void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }
}