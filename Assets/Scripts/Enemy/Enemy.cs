using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables EnemyBase")]
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;

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
    EnemySoundsManager _enemySoundsManager;
    [HideInInspector] public EnemyBase _enemyBase;
    public Enemy _enemy;

    void Start()
    {
        _enemySoundsManager = new EnemySoundsManager(_enemyAudioSource, _enemyAClip);
        _enemyAnimController = new EnemyAnimatorController(_enemyAnim);
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake,_distanceAttack ,_enemyAnimController);
        _EnemyTriggers = new EnemyTriggers(_enemyMove, _colliders);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor, _enemySoundsManager, _enemy);
        _name = this.gameObject.name;
    }

    public List<Transform> GetColliders() {return _colliders;}
    void Update()
    {
        _currentHealth = _enemyBase.currentHealthGetter();
        _EnemyTriggers.EnemyFixUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            _EnemyTriggers.OnTriggerEnterUpdate(enemyTarget);
        }
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            _EnemyTriggers.OnTriggerStayUpdate(enemyTarget);
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        _EnemyTriggers.OnTriggerExitUpdate(other.transform);
    }

   /* public void EnemyFinishBite()
    {
        _enemyAnim.SetBool("IsBite", false);
    }*/


    public void PartialSoundAttack()
    {
        _enemySoundsManager.playOnAttack();
    }
    public void DestroyThisObject()
    {
        Destroy(this.gameObject, 1f);
    }
}