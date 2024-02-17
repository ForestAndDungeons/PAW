using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { idle, persuit, attack, die, escape };
public class Enemy : MonoBehaviour , IDamage
{
    [Header("Variables Enemy")]
    public GameObject keyPrefab;
    [SerializeField] List<GameObject> _DropleableList = new List<GameObject>();

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
    [SerializeField] float _distanceFollow;
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


    void Awake()
    {
        _timeBtwShoot = _startTimeBtwShoot;
        enemySoundsManager = new EnemySoundsManager(_enemyAudioSource, _enemyAClip);
        _enemyAnimController = new EnemyAnimatorController(_enemyAnim);
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake, _enemyAnimController, _knockbackForce,_knockbackTime,_knockbackCounter);
        _enemyState = new EnemyState(_state,_enemyAnimController,_enemyMove,_targets,enemySoundsManager,this, _isRangeEnemy,_timeBtwShoot, _startTimeBtwShoot, _distanceFollow);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor, _haveAKey, enemySoundsManager, this,_enemyAudioSource,_enemyAClip, _particleSystem,_enemyMove, _targets,_enemyState, _enemyAnimController);
        _name = this.gameObject.name;
        _enemyState.StateStart();
  
    }
    //GETTERS
    public List<Transform> GetColliders() {return _targets;}
    public List<GameObject> GetDropeables() {return _DropleableList;}
    public bool GetterHaveAKey() {return _haveAKey;}
    void Update()
    {
        _haveAKey = _enemyBase.haveKey;
        _knockbackCounter = _enemyMove.CurrentKnockbackCounterGetter();
        _state = _enemyState.CurrentStateGetter();
        if (_stateDelegate!=null)
        {
            _stateDelegate();
        }
        _currentHealth = _enemyBase.currentHealth;
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
        Player pj = other.GetComponent<Player>();
        if (pj)
        {
            _targets.Remove(other.transform);
        }
    }

    //Instantiate

    public void InstantiateArrow()
    {
        if (_shootPoint != null && _ArrowPref !=null)
        {
            var dir = _targets[0].transform.position - _ArrowPref.transform.position;
            GameObject arrow = Instantiate(_ArrowPref, _shootPoint.position,_ArrowPref.transform.rotation);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse); //antes 32f
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
        }
    }

    //Other Funtions
    public void PartialSoundAttack()
    {
        enemySoundsManager.playOnAttack();      
    }
    public void DestroyThisObject()
    {
        if (_roomEntity !=null)
        {
            _roomEntity.ElimEnemyInList(this.gameObject);
            Destroy(this.gameObject, 0.3f);
        }
    }

    public void CoroutineInvulnerable(float damage)
    {
        StartCoroutine(OnInvulnerable(damage));
    }

    IEnumerator OnInvulnerable(float damage)
    {
        yield return new WaitForSeconds(1f);
        _enemyBase.isImmune = true;
        yield return new WaitForSeconds(1f);
        _enemyBase.isImmune = true;

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

    public void StopHit()
    {
        _enemyAnimController.OnHit(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceBrake);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distanceFollow);
    }

}