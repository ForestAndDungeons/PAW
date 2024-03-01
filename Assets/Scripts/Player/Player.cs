using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IDamage
{
    //Control Variables.
    [Header("Control Variables")]
    [SerializeField] ControlSO _controlSO;
    bool _isDead;
    public delegate void ControlDelegate(bool isGrounded);
    public ControlDelegate _controlDelegate;

    [SerializeField] bool _isPlayer1;
    public bool isPlayer1 { get { return _isPlayer1; } }
    Collider _collider;

    Player _otherPlayer;
    public Player otherPlayer { get { return _otherPlayer; } private set { } }

    Camera _myCamera;
    public Camera myCamera { get { return _myCamera; } set { _myCamera = value; } }

    [SerializeField] Camera _minimap;
    public Rect minimapRect { set { _minimap.rect = value; } }

    [SerializeField] float _timeOfImmune;
    [SerializeField] TMP_Text _coinsUI;
    [SerializeField] TMP_Text _keyUI;
    public TMP_Text _infoUI;

    public SKeyCode[] _sKeyCode;
    int _combo;
    public int combo { get { return _combo; } set { _combo = value; } }

    //Movement Variables.
    [Header("Movement Variables")]
    [SerializeField] float _turnSpeed;
    Rigidbody _myRigidBody;
    Animator _myAnimator;
    public Animator myAnimator { get { return _myAnimator; } }
    public delegate void MovementDelegate();
    public MovementDelegate _movementDelegate;

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
    [SerializeField] float _forceJump;
    [SerializeField] float _coins;
    [SerializeField] float _keysCollected;
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

    //Interface.
    [Header("Pause")]
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] GameObject _resumeButton;
    [SerializeField] GameObject _stats;

    //Clases Variables.
    Control _control;
    GroundSensor _groundSensor;
    [HideInInspector] public Movement _movement;
    [HideInInspector] public UIPlayer _uiPlayer;
    [HideInInspector] public PlayerSoundManager _playerSoundManager;
    [HideInInspector] public AnimationController _animationController;
    [HideInInspector] public PlayerBase _playerBase;
    [HideInInspector] public Teleport _teleport;

    public Weapon weapon;
    public Block block;

    //Instancia las clases y le pasa los parametros.
    private void Awake()
    {
        GameManager.Instance._players.Add(this);

        foreach (Player player in GameManager.Instance._players)
        {
            if (player != this)
                _otherPlayer = player;
        }

        _myRigidBody = this.GetComponent<Rigidbody>();
        _collider = this.GetComponent<Collider>();
        _myAnimator = this.GetComponentInChildren<Animator>();
        _isDead = false;

        _playerBase = new PlayerBase(_playerBaseSO, _name, _keysCollected, _playerSoundManager, _audioClip, _audioSource, _particleOnDamage, this, _animationController);

        _playerSoundManager = new PlayerSoundManager(_audioSource, _audioClip);

        _movement = new Movement(_playerBase.movementSpeed, _playerBase.jumpForce, _myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_controlSO, _movement, transform, _sKeyCode, _playerSoundManager);

        _groundSensor = new GroundSensor(_radius, _groundLayer, transform);

        _uiPlayer = new UIPlayer(_imageUIHearts, _spriteHeart, _imageUIArmor, _spriteArmor);

        _teleport = new Teleport(this);

        _currentHealth = _playerBase.currentHealth;
        _uiPlayer.UIUpdate(_maxHealth, _currentHealth, _armor);

        _controlDelegate = _control.Movements;
        _movementDelegate = _control.IsometricMovement;

        _attackPower = _playerBase.attackPower;
        weapon.SetAttackPower(_attackPower);
    }

    void Start()
    {
        if (GameManager.Instance.isSinglePlayer)
        {
            if (!_isPlayer1)
            {
                GameManager.Instance._players.Remove(this);
                _isDead = true;
                _collider.enabled = false;
                this.gameObject.SetActive(false);
                _myCamera.enabled = false;
            }
        }
    }

    //Llama a metodos de Artificial Updates.
    private void Update()
    {
        //Mantiene actualizado los datos de las variables para verlos en Inspector y pasarlos como parametros.
        _maxHealth = _playerBase.maxHealth;
        _currentHealth = _playerBase.currentHealth;
        _attackPower = _playerBase.attackPower;
        _armor = _playerBase.armor;
        _keysCollected = _playerBase.keysCollected;
        _isGrounded = _groundSensor.GroundSensorUpdate();
        _forceJump = _movement.jumpForce;
        _coins = _playerBase.coins;
        _coinsUI.text = System.Convert.ToString(_coins);
        _keyUI.text = System.Convert.ToString(_keysCollected);

        if (Input.GetKeyDown(_sKeyCode[0].key))
        {
            combo++;
        }

        if (!_isDead)
        {
            _animationController.InputUpdate(_control._verticalInput, _control._horizontalInput);
            _controlDelegate(_isGrounded);
        }

        if(Input.GetKey(KeyCode.Tab))
        {
            _stats.SetActive(true);
        }
        else
        {
            _stats.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _movementDelegate();
    }

    public void onDamage(float value)
    {
        var health = _currentHealth;
        var armor = _armor;

        _playerBase.onDamage(value);

        if (health > _playerBase.currentHealth || armor > _playerBase.armor)
        {
            _animationController.onHit();
            _playerSoundManager.playOnCollision(_audioSource, _audioClip[0]);
            _particleOnDamage.Play();
            _playerSoundManager.playOnHit();
        }
        if (_playerBase.currentHealth <= 0)
        {        
            _playerSoundManager.playOnDeath();
            _animationController.onDeath();
            DisableThisObject();
        }
        _uiPlayer.UIUpdate(_maxHealth, _currentHealth, _armor);
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
        _myCamera.enabled = false;
        _minimap.enabled = false;
        _otherPlayer._myCamera.rect = new Rect(0f, 0f, 1f, 1f);
        _otherPlayer.minimapRect = new Rect(0.8f, 0.8f, 0.2f, 0.2f);
        this.gameObject.SetActive(false);

        if (_otherPlayer._isDead)
        {
            _pauseManager.TogglePermanentPause();
            _resumeButton.SetActive(false);
        }
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
        if(_isGrounded)
        _movement.Jump();
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