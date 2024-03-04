using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyAttack : IState
{
    #region Parameters
    EnemieBase _enemieBase;
    EnemySM _enemySM;
    float _attackRadius;
    float _viewAngle;
    EnemyAnimController _enemyAnimController;
    Transform _enemyTransform;
    float _attackInterval;
    float _attackTime;
    #endregion

    public MeleEnemyAttack(EnemieBase enemieBase,EnemySM enemySM,float attackRadius,float viewAngle,EnemyAnimController enemyAnimController,Transform enemyTransform,float attackInterval)
    {
        _enemieBase = enemieBase;
        _enemySM = enemySM;
        _attackRadius = attackRadius;
        _viewAngle = viewAngle;
        _enemyAnimController = enemyAnimController;
        _enemyTransform = enemyTransform;
        _attackInterval = attackInterval;
    }

    public void OnExit()
    {
        
    }

    public void OnStart()
    {
        //_enemyAnimController.SetIsAttacking();
        _attackTime = Time.time;
    }

    public void OnUpdate()
    {
        if (!_enemieBase.isDamaged)
        {
            if (!_enemieBase.InFOV(_enemyTransform,_enemieBase.target,_attackRadius,_viewAngle))
            {
                _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyIdle);
            }
            else
            {
                if (_attackTime <= Time.time)
                {
                    _enemyAnimController.SetIsAttacking();
                    _attackTime = Time.time + _attackInterval;
                }
                _enemyTransform.LookAt(new Vector3(_enemieBase.target.transform.position.x, 1.5f, _enemieBase.target.transform.position.z));
            }

        }
        else
        {
            _enemySM.EnemyChangeStates(EnemyStates.EnemyKnockBack);
        }
    }
}
