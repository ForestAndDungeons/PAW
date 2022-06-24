using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { idle, persuit, attack, die, escape };
public class Enemy : MonoBehaviour , ICharacterBase
{
    [Header("Variables Enemy")]
    public GameObject keyPrefab;
    [SerializeField] bool _isInvulerable;
    [SerializeField] bool _isRangeEnemy;
    [SerializeField] GameObject _ArrowPref;
    [SerializeField] Transform _shootPoint;

    [Header("Enemy State")]
    [SerializeField]State _state;

    [Header("Variables EnemyBase")]
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;
    [SerializeField] bool _haveAKey;
    [SerializeField] ParticleSystem _particleSystem;

    [Header("Variables Move")]
    [SerializeField] float _speed;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _knockbackForce;
    [SerializeField] float _knockbackTime;
                     float _knockbackCounter;
    [SerializeField] float _distanceBrake;
                     float _timeBtwShoot;
    [SerializeField] float _startTimeBtwShoot;
    [SerializeField] List<Transform> _targets = new List<Transform>();
    

    [Header("Animator Controller")]
    [SerializeField] Animator _enemyAnim;

    [Header("Sounds Manager")]
    [SerializeField] AudioSource _enemyAudioSource;
    [SerializeField] AudioClip[] _enemyAClip;



    EnemyMovement _enemyMove;
    EnemyAnimatorController _enemyAnimController;
    [HideInInspector] public EnemySoundsManager enemySoundsManager;
    [HideInInspector] public EnemyBase _enemyBase;
    EnemyState _enemyState;
    public EnemyAttack enemyAttack;
    public RoomEntity _roomEntity;

    public delegate void StateDelegate();
    public StateDelegate _stateDelegate;

    void Start()
    {
        _timeBtwShoot = _startTimeBtwShoot;
        enemySoundsManager = new EnemySoundsManager(_enemyAudioSource, _enemyAClip);
        _enemyAnimController = new EnemyAnimatorController(_enemyAnim);
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake, _enemyAnimController, _knockbackForce,_knockbackTime,_knockbackCounter);
        _enemyState = new EnemyState(_state,_enemyAnimController,_enemyMove,_targets,enemySoundsManager,this, _isRangeEnemy,_timeBtwShoot, _startTimeBtwShoot);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor, _haveAKey, enemySoundsManager, this,_enemyAudioSource,_enemyAClip, _particleSystem,_enemyMove, _targets,_enemyState);
        _name = this.gameObject.name;
        _enemyState.StateStart();
    }

    public List<Transform> GetColliders() {return _targets;}
    void Update()
    {
        _haveAKey = _enemyBase.GetKey();
        _knockbackCounter = _enemyMove.CurrentKnockbackCounterGetter();
        _state = _enemyState.CurrentStateGetter();
        //_enemyState.StateUpdate();
        if (_stateDelegate!=null)
        {
            _stateDelegate();
        }
        _currentHealth = _enemyBase.GetCurrentHealth();
        if (_targets.Count > 0)
        {
            if (_targets[0].gameObject.activeSelf == false)
            {
                _targets.Remove(_targets[0]);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player pj = other.GetComponent<Player>();
        if (other!=null)
        {
            if (pj)
            {
                 if (!_targets.Contains(other.transform))
                 {
                    _targets.Add(other.transform);
                 }
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_isRangeEnemy)
        {
            _targets.Remove(other.transform);
        }
        else { 
            var _isAttack = _enemyState.IsAttackGetter();
            if (!_isAttack)
            {
                _targets.Remove(other.transform);
            }
        }
    }

    public void InstantiateArrow()
    {
        if (_shootPoint != null && _ArrowPref !=null)
        {
            Debug.Log("Instancio una flecha");
            GameObject arrow = Instantiate(_ArrowPref, _shootPoint.position, _ArrowPref.transform.rotation);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
        }
    }
    public void PartialSoundAttack()
    {
        enemySoundsManager.playOnAttack();
    }
    public void DestroyThisObject()
    {
        _roomEntity.ElimEnemyInList(this.gameObject);
        Destroy(this.gameObject, 2f);
    }

    public bool GetterHaveAKey() { return _haveAKey; }

    public void CoroutineInvulnerable(float damage)
    {
        StartCoroutine(OnInvulnerable(damage));
    }

    IEnumerator OnInvulnerable(float damage)
    {
        var damage2 = damage;
        Debug.Log("Entro al Ienumerator");
        _enemyBase.SetIsImmune(true);
        damage = 0.0f;
        yield return new WaitForSeconds(.5f);

        damage = damage2;
        _enemyBase.SetIsImmune(false);
    }

    public void onDamage(float damage)
    {
        _enemyBase.onDamage(damage);
    }

    public void onAttack(Collision other)
    {
        _enemyBase.onAttack(other);
    }

    public void HealthUp(float add)
    {
        _enemyBase.HealthUp(add);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceBrake);
    }
}