using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    float _speed;
    Rigidbody _rb;
    Transform _transform;
    float _knockbackForce;
    float _distanceBrake;
    float _distanceAttack;
    float _knockbackTime;
    float _knockbackCounter;
    EnemyAnimatorController _enemyAnimController;

    public EnemyMovement(float _sp, Rigidbody rigidbody, Transform trasform, float _dB,float distanceAttack, EnemyAnimatorController enemyAnimController, float knockbackForce, float knockbackTime, float knockbackCounter)
    {
        _speed = _sp;
        _rb = rigidbody;
        _transform = trasform;
        _knockbackForce = knockbackForce;
        _distanceBrake = _dB;
        _distanceAttack = distanceAttack;
        _enemyAnimController = enemyAnimController;
        _knockbackTime = knockbackTime;
        _knockbackCounter = knockbackCounter;
    }

    public void FollowToPlayer(Transform _target)
    {

        if (_target != null)
        {
            if (Vector3.Distance(_transform.position, _target.position) > _distanceBrake)
            {
                Vector3 pos = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
                _rb.MovePosition(pos);
            }
            _transform.LookAt(new Vector3(_target.position.x,0.2f,_target.position.z));
        }
    }

    public void OnKnockback(Transform target)
    {
        _knockbackCounter = _knockbackTime;
        Vector3 hitDirection = new Vector3 ((target.position.x - _transform.position.x),0f,(target.position.z - _transform.position.z));
        hitDirection = hitDirection.normalized;
        _rb.MovePosition(_transform.position - (hitDirection * _knockbackForce));  
    }

    public float DistanceBrakeGetter() { return _distanceBrake; }

    public Transform MyTransformGetter() { return _transform; }

    public float CurrentKnockbackCounterGetter()
    {
        return _knockbackCounter;
    }
}