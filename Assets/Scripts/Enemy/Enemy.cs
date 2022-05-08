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
    [SerializeField] List<Transform> _colliders = new List<Transform>();

    [Header("Animator Controller")]
    [SerializeField] Animator _enemyAnim;

    [Header("Sounds Manager")]
    [SerializeField] AudioSource _enemyAudioSource;
    [SerializeField] AudioClip[] _enemyAClip;

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
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake);
        _EnemyTriggers = new EnemyTriggers(_enemyMove, _colliders);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor, _enemySoundsManager, _enemy);
        _name = this.gameObject.name;
    }

    public List<Transform> GetColliders() {return _colliders;}

    private void OnTriggerStay(Collider other)
    {
        _EnemyTriggers.OnTriggerStayUpdate(other.transform, transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _EnemyTriggers.OnTriggerExitUpdate(other.transform);
    }

    public void EnemyFinishBite()
    {
        _enemyAnim.SetBool("IsBite", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Funciona pero al tener que cambiar el metodo de ataque se tiene que modificar
        _enemyAnimController.OnEnemyBite();
    }

    void Update()
    {
        _currentHealth = _enemyBase.currentHealthGetter();
    }

    public void PartialSoundAttack()
    {
        _enemySoundsManager.playOnAttack();
    }
    public void DestroyThisObject()
    {
        Destroy(this.gameObject, 1f);
    }
}