using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables Move")]
    [SerializeField] float _speed;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _distanceBrake;
    [SerializeField] List<Transform> _colliders = new List<Transform>();

    [Header("Variables Attack")]
    /* [SerializeField] Collider _followCollider;
     [SerializeField] Collider _AttkCollider;*/

    //Variables para _enemyBase
    [SerializeField] string _name;
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;
    [SerializeField] int _attackPower;
    [SerializeField] int _armor;

    EnemyMovement _EnemyMove;
    EnemyTriggers _EnemyTriggers;
    EnemyAttack _enemyAttack;
    [HideInInspector] public EnemyBase _enemyBase;

    void Start()
    {
        _EnemyMove = new EnemyMovement(_speed, _rb, transform,_distanceBrake);
        _EnemyTriggers = new EnemyTriggers(_EnemyMove,_colliders);
        _enemyBase = new EnemyBase(_name, _maxHealth, _attackPower, _armor);
       // _enemyAttack = new EnemyAttack(_AttkCollider);
    }

    public List<Transform> GetColliders() { return _colliders; }
    private void OnTriggerStay(Collider other)
    {
        _EnemyTriggers.OnTriggerStayUpdate(other.transform, transform);
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (_AttkCollider)
        {
            Debug.Log("Entre al trigger de _AttkCollider ");
        }
        else if (_followCollider)
        {
            Debug.Log("Entre al trigger de _followCollider");
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        _EnemyTriggers.OnTriggerExitUpdate(other.transform);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other != null)
            _enemyBase.onAttack(other);
    }

    void Update()
    {
        _currentHealth = _enemyBase.currentHealthGetter();

        if (_currentHealth <= 0)
            this.gameObject.SetActive(false);
    }
}
