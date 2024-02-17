using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] bool isPlayer1;
    [SerializeField] GameObject _model;
    Collider _collider;
    [SerializeField] Player _otherPlayer;
    public Player otherPlayer { get { return _otherPlayer; } private set { } }
    [SerializeField] Camera _myCamera;
    [SerializeField] Camera _player2Minimap;
    [SerializeField] float _timeOfImmune;
    [SerializeField] TMP_Text _moneyUI;
    public TMP_Text _infoUI;

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
    [SerializeField] float _forceJump;
    [SerializeField] float _money;
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

        _collider = this.GetComponent<Collider>();
        _isDead = false;

        _playerSoundManager = new PlayerSoundManager(_audioSource, _audioClip);

        _movement = new Movement(_dataMovement, _myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_controlSO, _movement, transform, _sKeyCode, _playerSoundManager);

        _groundSensor = new GroundSensor(_radius, _groundLayer, transform);

        _playerBase = new PlayerBase(_playerBaseSO, _name, _haveAKey, _playerSoundManager, _audioClip, _audioSource, _particleOnDamage, this, _animationController);

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
            if (!isPlayer1)
            {
                GameManager.Instance._players.Remove(this);
                _isDead = true;
                _collider.enabled = false;
                this.gameObject.SetActive(false);
                _myCamera.enabled = false;
                _otherPlayer._myCamera.rect = new Rect(0f, 0f, 1f, 1f);
                _otherPlayer._player2Minimap.enabled = false;
                //_model.SetActive(false);
                //DisableThisObject();
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
        _haveAKey = _playerBase.haveKey;
        _isGrounded = _groundSensor.GroundSensorUpdate();
        _forceJump = _movement.GetForceJump();

        _moneyUI.text = System.Convert.ToString(_money);

        //TEMP para ver
        _money = _playerBase.money;

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

    public void AttackSpeedUp()
    {
        _myAnimator.SetBool("AttackSpeedUp", true);
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
        _playerBase.isImmune = false;
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