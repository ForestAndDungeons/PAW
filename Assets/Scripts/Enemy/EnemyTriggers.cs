using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers
{
    EnemyMovement _movement;
    List<Transform> _col;

    public EnemyTriggers(EnemyMovement _enemyMove, List<Transform> _coll)
    {

        _movement = _enemyMove;
        _col = _coll;
    }

    public void OnTriggerStayUpdate(Transform _targets)
    {
        if (!_col.Contains(_targets.transform))
        {
            _col.Add(_targets.transform);
        }

        _movement.FollowPlayer(_col[0]);
    }

    public void OnTriggerExitUpdate(Transform _target)
    {
        _col.Remove(_target.transform);
    }
}