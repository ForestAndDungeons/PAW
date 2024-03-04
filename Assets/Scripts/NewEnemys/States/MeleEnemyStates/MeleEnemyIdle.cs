using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyIdle : IState
{
    #region Parameters
    EnemieBase _enemieBase;
    Transform _enemyTransform;
    float _viewRadius;
    float _viewAngle;
    EnemySM _enemySM;
    EnemyAnimController _enemyAnimController;
    float _wait;
    #endregion
    public MeleEnemyIdle(EnemieBase enemieBase,EnemySM enemySM,Transform enemyTransform,float viewRadius,float viewAngle, EnemyAnimController enemyAnimController)
    {
        _enemieBase = enemieBase;
        _enemyTransform = enemyTransform;
        _viewRadius = viewRadius;
        _viewAngle = viewAngle;
        _enemySM = enemySM;
        _enemyAnimController = enemyAnimController;
    }

    public void OnExit()
    {
        
    }

    public void OnStart()
    {
        _enemyAnimController.SetIsWalk(0f);
        _wait = Time.time;
    }

    public void OnUpdate()
    {
        if (!_enemieBase.isDamaged)
        {
                if (_enemieBase.target!=null)
                {
                    if (_enemieBase.InFOV(_enemyTransform, _enemieBase.target, _viewRadius, _viewAngle))
                    {
                        _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyPersuit);
                    }
                    else
                    {
                        if (Time.time - _wait >= 1.5f)
                        {
                            _wait = Time.time;
                            _enemySM.EnemyChangeStates(EnemyStates.EnemyPatrol);
                        }
                    }
                }  
        }
        else { _enemySM.EnemyChangeStates(EnemyStates.EnemyKnockBack); }
    }
}
