using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, ICharacterBase
{
    Renderer _renderer;
    Collider _collider;
    [SerializeField] Player _otherPlayer;
    [SerializeField] float _timeOfImmune;
    [SerializeField] SKeyCode[] _sKeyCode;

    //Movement Variables.
    [Header("Movement Variables")]
    [SerializeField] MovementSO _dataMovement;
    [SerializeField] float _turnSpeed;
    [SerializeField] Rigidbody _myRigidBody;

    //Control Variables.
    [Header("Control Variables")]
    [SerializeField] ControlSO _controlSO;
    [SerializeField] bool _isDead;
    [SerializeField] bool _canMove;

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

    [Header("Pause")]
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] GameObject _resumeButton;


    //Clases Variables.
    Control _control;
    Movement _movement;
    GroundSensor _groundSensor;
    [HideInInspector] public UIPlayer _uiPlayer;
    [HideInInspector] public PlayerSoundManager _playerSoundManager;
    [HideInInspector] public AnimationController _animationController;
    [HideInInspector] public PlayerBase _playerBase;

    public Weapon weapon;
    public Block block;

    //Instancia las clases y le pasa los parametros.
    private void Start()
    {
        _renderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
        _isDead = false;
        _canMove = true;

        _playerSoundManager = new PlayerSoundManager(_audioSource, _audioClip);

        _movement = new Movement(_dataMovement ,_myRigidBody, transform);

        _animationController = new AnimationController(_myAnimator);

        _control = new Control(_controlSO, _movement, transform, _sKeyCode, _playerSoundManager);
            
        _groundSensor = new GroundSensor(_radius, _groundLayer, transform);

        _playerBase = new PlayerBase(_playerBaseSO, _name, _haveAKey, _playerSoundManager , _audioClip , _audioSource, _particleOnDamage, this, _animationController);

        _uiPlayer = new UIPlayer(_imageUIHearts, _spriteHeart, _imageUIArmor, _spriteArmor);

        _currentHealth = _playerBase.GetCurrentHealth();
        _uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth, _armor);
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

        if (!_isDead)
        {
            if (_canMove)
            {
                _animationController.InputUpdate(_control._verticalInput, _control._horizontalInput);
                _control.Movements(_isGrounded);
            }
            else if (!_canMove)
            {
                _animationController.InputUpdate(0, 0);
            }
        }
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

    private void FixedUpdate()
    {
        if (!_isDead && _canMove)
        {
            _control.IsometricMovement();
        }
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

    public void SoundHit()
    {
        _playerSoundManager.playOnHit();
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
}