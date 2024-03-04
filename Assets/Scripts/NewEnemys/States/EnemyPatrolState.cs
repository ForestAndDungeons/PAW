using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    EnemySM _enemySM;
    float _viewRadius;
    float _viewAngle;
    List<GameObject> _allwaypoints;
    Transform _enemyTransform;
    Rigidbody _enemyRB;
    int _currWaypoint;
    EnemieBase _enemieBase;
    EnemyAnimController _enemyAnimController;

    public EnemyPatrolState(EnemieBase enemieBase, EnemySM enemySM, float viewRadius,float viewAngle, List<GameObject> allwaypoints, Transform enemyTransform,Rigidbody enemyRB, int currWaypoint, EnemyAnimController enemyAnimController)
    {
        _enemieBase = enemieBase;
        _enemySM = enemySM;
        _viewRadius = viewRadius;
        _viewAngle = viewAngle;
        _allwaypoints = allwaypoints;
        _enemyTransform = enemyTransform;
        _enemyRB = enemyRB;
        _currWaypoint = currWaypoint;
        _enemyAnimController = enemyAnimController;
    }

    public void OnStart()
    {
        _enemyAnimController.SetIsWalk(1f);
    }

    public void OnExit()
    {
        _enemyAnimController.SetIsWalk(0f);
    }

    public void OnUpdate()
    {
        if (_enemieBase.isDamaged)
        {
            _enemySM.EnemyChangeStates(EnemyStates.EnemyKnockBack);
           
        }
        else
        {
            if (_enemieBase.target != null)
            {
                if (_enemieBase.InFOV(_enemyTransform, _enemieBase.target, _viewRadius, _viewAngle))
                {
                    _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyPersuit);
                }
                else Patrol();
            }
        }

    }

    void Patrol()
    {
        
        var currWaypoint = _allwaypoints[_currWaypoint];
        if (_enemieBase.InFOV(_enemyTransform,currWaypoint,_viewRadius,360f))
        {
            Vector3 enemyDir = currWaypoint.transform.position - _enemyTransform.position;
            // _enemyTransform.position += enemyDir.normalized * (_enemieBase.maxSpeed/0.5f) * Time.deltaTime;
            Vector3 pos = Vector3.MoveTowards(_enemyTransform.position, currWaypoint.transform.position, _enemieBase.maxSpeed * Time.deltaTime);
            _enemyRB.MovePosition(pos);
            _enemyTransform.LookAt(currWaypoint.gameObject.transform.position);

            if (enemyDir.magnitude <= 0.5f)
            {
                _currWaypoint++;
                var removedWaypoint = currWaypoint.GetComponent<WaypointPatrol>();
                _allwaypoints.Remove(currWaypoint);
                removedWaypoint.OnDestroy();
            

                if (_currWaypoint > _allwaypoints.Count - 1)
                {
                    _currWaypoint = 0;
                }
            }
        }
        else
        {
            Debug.Log("Tengo una pared adelande del waypoint");
            var removedWaypoint = currWaypoint.GetComponent<WaypointPatrol>();
            _allwaypoints.Remove(currWaypoint);
            removedWaypoint.OnDestroy();
        }
    }
}
