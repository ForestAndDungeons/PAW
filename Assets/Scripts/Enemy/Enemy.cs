using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables EnemyBase")]
    [SerializeField] string _name;
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;
    [SerializeField] int _attackPower;
    [SerializeField] int _armor;

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

    EnemyMovement           _enemyMove;
    EnemyTriggers           _EnemyTriggers;
    EnemyAnimatorController _enemyAnimController;
    EnemySoundsManager      _enemySoundsManager;
    [HideInInspector] public EnemyBase _enemyBase;

    void Start()
    {
        _enemySoundsManager = new EnemySoundsManager(_enemyAudioSource,_enemyAClip);
        _enemyAnimController = new EnemyAnimatorController(_enemyAnim);
        _enemyMove = new EnemyMovement(_speed, _rb, transform, _distanceBrake);
        _EnemyTriggers = new EnemyTriggers(_enemyMove,_colliders);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor);
        _name = this.gameObject.name;
    }

    public List<Transform> GetColliders() { return _colliders; }
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

        if (_currentHealth <= 0) {
            Debug.Log("a");
            _enemySoundsManager.playOnDeath();
            Destroy(this.gameObject, 1f);
        }
    }

    public void PartialSoundAttack()
    {
        _enemySoundsManager.playOnAttack();
    }
}
