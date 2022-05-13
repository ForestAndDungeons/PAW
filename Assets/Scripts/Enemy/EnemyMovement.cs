using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    float _speed;
    Rigidbody _rb;
    Transform _transform;
    float _distanceBrake;
    float _distanceAttack;
    EnemyAnimatorController _enemyAnimController;

    public EnemyMovement(float _sp, Rigidbody rigidbody, Transform trasform, float _dB,float distanceAttack, EnemyAnimatorController enemyAnimController)
    {
        _speed = _sp;
        _rb = rigidbody;
        _transform = trasform;
        _distanceBrake = _dB;
        _distanceAttack = distanceAttack;
        _enemyAnimController = enemyAnimController;
    }

    public void FollowPlayer(Transform _target)
    {
        if (_target != null)
        {
            if (Vector3.Distance(_transform.position, _target.position) > _distanceBrake)
            {
                Vector3 pos = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
                _rb.MovePosition(pos);
            }
            _transform.LookAt(_target);
        }
    }

    public float DistanceBrakeGetter() { return _distanceBrake; }

    public Transform MyTransformGetter() { return _transform; }

}