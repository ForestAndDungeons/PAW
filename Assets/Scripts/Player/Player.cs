using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamage
{
    Renderer _renderer;
    Collider _collider;
    [SerializeField] Player _otherPlayer;
    [SerializeField] Camera _myCamera;
    [SerializeField] Camera _player2Minimap;
    [SerializeField] float _timeOfImmune;
    public SKeyCode[] _sKeyCode;
    public int combo;

    //Movement Variables.
    [Header("Movement Variables")]
    [SerializeField] MovementSO _dataMovement;
    [SerializeField] float _turnSpeed;
    [SerializeField] Rigidbody _myRigidBody;
    public delegate void MovementDelegate();
    public MovementDelegate _movementDelegate;

    //Control Variables.
    [Header("Control Variables")]
    [SerializeField] ControlSO _controlSO;
    [SerializeField] bool _isDead;
    [SerializeField] bool _canMove;
    public delegate void ControlDelegate(bool isGrounded);
    public ControlDelegate _controlDelegate;

    //GroundSensor Variables.
    [Header("GroundSensor Variables")]
    [SerializeField] float _radius;
    [SerializeField] bool _isGrounded;
    [SerializeField] LayerMask _groundLayer;

    //PlayerBase Variables.
    [Header("PlayerBase Variables")]
    [SerializeField] PlayerBaseSO _playerBaseSO;
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;
    [SerializeField] bool _haveAKey;
    [SerializeField] ParticleSystem _particleOnDamage;
    [SerializeField] ParticleSystem _particleWalk;

    //PlayerSoundManager Variables.
    [Header("Sounds Manager")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    //UIPlayer Variables.
    [Header("UI Player")]
    [SerializeField] Image[] _imageUIHearts;
    [SerializeField] Sprite[] _spriteHeart;
    [SerializeField] Image[] _imageUIArmor;
    [SerializeField] Sprite[] _spriteArmor;

    //AnimationController Variable.
    [Header("Animation")]
    [SerializeField] Animator _myAnimator;
    [SerializeField] float _attackSpeed;

    [Header("Pause")]
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] GameObject _resumeButton;


    //Clases Variables.
    Control _control;
    GroundSensor _groundSensor;
    [HideInInspector] public Movement _movement;
    [HideInInspector] public UIPlayer _uiPlayer;
    [HideInInspector] public PlayerSoundManager _playerSoundManager;
    [HideInInspector] public AnimationController _animationController;
    [HideInInspector] public PlayerBase _playerBase;

    public Weapon weapon;
    public Block block;

    //Instancia las clases y le pasa los parametros.
    private void Awake()
    {
        _renderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
        _isDead = false;
        _canMove = true;

        _playerSoundManager = new PlayerSoundManager(_audioSource, _audioClip);

        _movement = new Movement(_dataMovement, _myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_controlSO, _movement, transform, _sKeyCode, _playerSoundManager);

        _groundSensor = new GroundSensor(_radius, _groundLayer, transform);

        _playerBase = new PlayerBase(_playerBaseSO, _name, _haveAKey, _playerSoundManager, _audioClip, _audioSource, _particleOnDamage, this, _animationController);

        _uiPlayer = new UIPlayer(_imageUIHearts, _spriteHeart, _imageUIArmor, _spriteArmor);

        _currentHealth = _playerBase.GetCurrentHealth();
        _uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth, _armor);

        _controlDelegate = _control.Movements;
        _movementDelegate = _control.IsometricMovement;

        _attackPower = _playerBase.GetAttackPower();
        weapon.SetAttackPower(_attackPower);

        _attackSpeed = _myAnimator.speed;
    }

    //Llama a metodos de Artificial Updates.
    private void Update()
    {
        //Mantiene actualizado los datos de las variables para verlos en Inspector y pasarlos como parametros.
        _maxHealth = _playerBase.GetMaxHealth();
        _currentHealth = _playerBase.GetCurrentHealth();
        _attackPower = _playerBase.GetAttackPower();
        _armor = _playerBase.GetArmor();
        _haveAKey = _playerBase.GetKey();
        _isGrounded = _groundSensor.GroundSensorUpdate();

        if (Input.GetKeyDown(_sKeyCode[0].key))
        {
            combo++;
        }

        if (!_isDead)
        {
            _animationController.InputUpdate(_control._verticalInput, _control._horizontalInput);
            _controlDelegate(_isGrounded);
        }
    }

    private void FixedUpdate()
    {
        _movementDelegate();
    }

    public void onDamage(float damage)
    {
        _playerBase.onDamage(damage);
    }

    public void onAttack(Collision other)
    {
        _playerBase.onAttack(other);
    }

    public void HealthUp(float add)
    {
        _playerBase.HealthUp(add);
    }

    public void OnTriggerEnter(Collider other)
    {
        var pickUp = other.GetComponent<PickUp>();

        if (pickUp != null)
            pickUp.Pick(_playerBase);
    }

    public void DisableThisObject()
    {
        _isDead = true;
        _collider.enabled = false;

        StartCoroutine(WaitForDeath());
    }

    public IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(4f);
        this.gameObject.SetActive(false);
        _myCamera.enabled = false;
        _otherPlayer._myCamera.rect = new Rect(0f, 0f, 1f, 1f);
        _otherPlayer._player2Minimap.enabled = false;

        if (_otherPlayer._isDead)
        {
            _pauseManager.TogglePermanentPause();
            _resumeButton.SetActive(false);
        }
    }
    public IEnumerator TimeOfImmune()
    {
        yield return new WaitForSeconds(_timeOfImmune);
        _playerBase.SetIsImmune(false);
    }

    public void CanMoveSetter(bool canMove)
    {
        _canMove = canMove;
    }

    //PlayerSoundManager Methods
    public void SoundAttack()
    {
        _playerSoundManager.playOnAttack();
    }

    //AnimationController Methods
    public void EndAttack()
    {
        _animationController.onAttackEnd();
    }

    //Methods for SKeyCode
    public void Attack()
    {
        _animationController.onAttack();
    }
    public void Special()
    {
        _animationController.onSpecial();
    }
    public void Block()
    {
        _animationController.onBlockStart();
    }
    public void Jump()
    {
        _movement.Jump();
    }

    public void SetAttackSpeed(float add)
    {
        _attackSpeed += add;
    }

    public void SetEmptyAnimationInput(bool isGrounded)
    {
        _animationController.InputUpdate(0, 0);
    }
    public void SetControlDelegate()
    {
        _controlDelegate = _control.Movements;
    }
    public void SetEmptyControlDelegate()
    {
        _controlDelegate = SetEmptyAnimationInput;
    }

    public void SetMovementDelegate()
    {
        _movementDelegate = _control.IsometricMovement;
    }
    public void SetEmptyMovementDelegate()
    {
        _movementDelegate = delegate { };
    }
}