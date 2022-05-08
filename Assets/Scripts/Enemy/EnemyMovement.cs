using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    float _speed;
    Rigidbody _rb;
    Transform _transform;
    float _distanceBrake;


    public EnemyMovement(float _sp, Rigidbody rigidbody, Transform trasform, float _dB)
    {
        _speed = _sp;
        _rb = rigidbody;
        _transform = trasform;
        _distanceBrake = _dB;
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

}