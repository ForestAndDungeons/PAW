using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { idle, persuit, attack, die };
public class Enemy : MonoBehaviour
{
    [Header("Variables Enemy")]
    [SerializeField] bool _haveAKey;
    public GameObject _keyPrefab;
    

    [Header("Enemy State")]
    [SerializeField]State _state;

    [Header("Variables EnemyBase")]
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;
    [SerializeField] ParticleSystem _particleSystem;

    [Header("Variables Move")]
    [SerializeField] float _speed;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _knockbackForce;
    [SerializeField] float _knockbackTime;
                     float _knockbackCounter;
    [SerializeField] float _distanceBrake;
    [SerializeField] float _distanceAttack;
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

    

    void Start()
    {
        enemySoundsManager = new EnemySoundsManager(_enemyAudioSource, _enemyAClip);
        _enemyAnimController = new EnemyAnimatorController(_enemyAnim);
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake,_distanceAttack ,_enemyAnimController, _knockbackForce,_knockbackTime,_knockbackCounter);
        _enemyState = new EnemyState(_state,_enemyAnimController,_enemyMove,_targets,enemySoundsManager,this);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor, _haveAKey, enemySoundsManager, this,_enemyAudioSource,_enemyAClip, _particleSystem,_enemyMove, _targets,_enemyState);
        _name = this.gameObject.name;
    }

    public List<Transform> GetColliders() {return _targets;}
    void Update()
    {
        _haveAKey = _enemyBase.keyGetter();
        _knockbackCounter = _enemyMove.CurrentKnockbackCounterGetter();
        _state = _enemyState.CurrentStateGetter();
         _enemyState.StateUpdate();
        _currentHealth = _enemyBase.currentHealthGetter();
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
        var _isAttack = _enemyState.IsAttackGetter();
        if (!_isAttack)
        {
            _targets.Remove(other.transform);
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

    public bool GetterHaveAKey()
    {
        return _haveAKey;
    }
}