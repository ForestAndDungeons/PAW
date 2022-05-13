using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //[SerializeField] bool player2;
    Renderer _renderer;
    Collider _collider;
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] GameObject _resumeButton;

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
    [SerializeField] bool _isDead;

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
    [HideInInspector] public UIPlayer _uiPlayer;
    [HideInInspector] public PlayerSoundManager _playerSoundManager;
    [HideInInspector] public AnimationController _animationController;
    [HideInInspector] public PlayerBase _playerBase;

    public Weapon weapon;

    //Instancia las clases y le pasa los parametros.
    private void Start()
    {
        _renderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
        _isDead = false;

        _playerSoundManager = new PlayerSoundManager(_audioSource,_audioClip);

        _movement = new Movement(_speed, _forceJump, _myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_movement, transform, _verticalAxis, _horizontalAxis, _animationController, _turnSpeed, _keyJump, _keyAttack, _playerSoundManager, _isDead);
            
        _groundSensor = new GroundSensor(_radius, _groundLayer, transform);

        _playerBase = new PlayerBase(_name, _maxHealth, _attackPower, _armor, _playerSoundManager , _audioClip , _audioSource, _particleSystem, this, _animationController);

        _uiPlayer = new UIPlayer(_hearts, _spriteHeart);

        _currentHealth = _playerBase.currentHealthGetter();
        _uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth);
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
    }

    private void FixedUpdate()
    {
        _control.IsometricMovement(_isGrounded, _isDead);
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

    public IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(4f);
        _pauseManager.TogglePermanentPause();
        _resumeButton.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void DisableThisObject()
    {
        _isDead = true;
        _control.isDeadSetter(_isDead);
        _collider.enabled = false;

        StartCoroutine(WaitForDeath());
    }
}