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

    EnemyMovement _EnemyMove;
    EnemyTriggers _EnemyTriggers;
    EnemyAttack _enemyAttack;

    void Start()
    {
        _EnemyMove = new EnemyMovement(_speed, _rb, transform,_distanceBrake);
        _EnemyTriggers = new EnemyTriggers(_EnemyMove,_colliders);
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

    void Update()
    {
        
    }
}
