using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers
{
    EnemyMovement _movement;
    List<Transform> _col;

    public EnemyTriggers(EnemyMovement _enemyMove,List<Transform> _coll) { 
       
        _movement = _enemyMove;
        _col = _coll;
    }

   public void OnTriggerStayUpdate(Transform _targets, Transform myPosition)
    {
        if (!_col.Contains(_targets.transform))
        {
            _col.Add(_targets.transform);
        }

        _movement.FollowPlayer(_col[0]);
        
       /* if (_targets[0]!=null) {
            float dist1 = Vector3.Distance(_targets[0].position, myPosition.position);
            Debug.Log("Dist1 " + dist1);
        }
        if (_targets[1] != null) { 
            float dist2 = Vector3.Distance(_targets[1].position, myPosition.position); 
            Debug.Log("Dist2 " + dist2);
        }

        if (dist1 < dist2) _movement.FollowPlayer(_targets[0]);
        else _movement.FollowPlayer(_targets[1]);*/

    }

   public void OnTriggerExitUpdate(Transform _target)
    {
        _col.Remove(_target.transform);
    }
}
