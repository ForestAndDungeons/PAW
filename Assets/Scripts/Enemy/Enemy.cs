using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { idle, persuit, attack, die };
public class Enemy : MonoBehaviour
{
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
    [SerializeField] float _distanceBrake;
    [SerializeField] float _distanceAttack;
    [SerializeField] List<Transform> _colliders = new List<Transform>();

    [Header("Animator Controller")]
    [SerializeField] Animator _enemyAnim;

    [Header("Sounds Manager")]
    [SerializeField] AudioSource _enemyAudioSource;
    [SerializeField] AudioClip[] _enemyAClip;
    public Transform enemyTarget;

    EnemyMovement _enemyMove;
    EnemyTriggers _EnemyTriggers;
    EnemyAnimatorController _enemyAnimController;
    [HideInInspector] public EnemySoundsManager enemySoundsManager;
    [HideInInspector] public EnemyBase _enemyBase;
    EnemyState _enemyState;
    public EnemyAttack enemyAttack;
    

    void Start()
    {
        enemySoundsManager = new EnemySoundsManager(_enemyAudioSource, _enemyAClip);
        _enemyAnimController = new EnemyAnimatorController(_enemyAnim);
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake,_distanceAttack ,_enemyAnimController);
        _EnemyTriggers = new EnemyTriggers(_enemyMove, _colliders);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor, enemySoundsManager, this,_enemyAudioSource,_enemyAClip, _particleSystem);
        _name = this.gameObject.name;
        _enemyState = new EnemyState(_state,_enemyAnimController,_enemyMove,enemyTarget);
    }

    public List<Transform> GetColliders() {return _colliders;}
    void Update()
    {
        _state = _enemyState.CurrentStateGetter();
        _enemyState.StateUpdate();
        _currentHealth = _enemyBase.currentHealthGetter();
        _EnemyTriggers.EnemyFixUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            _enemyState.isPersuit();
            _EnemyTriggers.OnTriggerEnterUpdate(enemyTarget);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _EnemyTriggers.OnTriggerExitUpdate(other.transform);
        _enemyState.isIdle();
    }

    public void PartialSoundAttack()
    {
        enemySoundsManager.playOnAttack();
    }
    public void DestroyThisObject()
    {
        _enemyAnimController.OnDeath();
        Destroy(this.gameObject, 2f);
    }
}