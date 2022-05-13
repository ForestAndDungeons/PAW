using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers
{
    EnemyMovement _movement;
    List<Transform> _col;
    bool _isRange;
    Transform _target;

    public EnemyTriggers(EnemyMovement _enemyMove, List<Transform> _coll)
    {
        _movement = _enemyMove;
        _col = _coll;
    }


    public void OnTriggerEnterUpdate(Transform _targets)
    {
        /* if (!_col.Contains(_targets.transform))
         {
             _col.Add(_targets.transform);} */
        if (_targets != null)
         
        {
            _target = _targets;
            _isRange = true;
        }

    }

    public void EnemyFixUpdate()
    { 
        if (_isRange)
        {
           //_movement.FollowPlayer(_target);
          // _movement.AttackPlayer(_target);
        }
    }

    public void OnTriggerExitUpdate(Transform _target)
    {
        _col.Remove(_target.transform);
        _isRange = false;
    }
    /*
     * --Codigo de prueba faltan cosas.
     * public void OnTriggerStayUpdate(Transform _targets)
    {
        if (!_col.Contains(_targets.transform))
        {
            _col.Add(_targets.transform);
        }

        if (!_col.Contains(null))
        {
            _isRange = true;
        }
        else
        {
            _isRange = false;
        }

    }*/
}