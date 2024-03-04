using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyPersuit : IState
{
    #region Parameters
    EnemieBase _enemieBase;
    EnemySM _enemySM;
    Transform _enemyTransform;
    Rigidbody _myRB;
    float _attackRadius;
    float _viewAngle;
    float _viewRadius;
    EnemyAnimController _enemyAnimController;
    float _defaultSpeed;
    #endregion

    public MeleEnemyPersuit(EnemieBase enemieBase,EnemySM enemySM,Transform enemyTransform,Rigidbody myRB,float attackRadius,float viewAngle,float viewRadius, EnemyAnimController enemyAnimController)
    {
        _enemieBase = enemieBase;
        _enemySM = enemySM;
        _enemyTransform = enemyTransform;
        _myRB = myRB;
        _attackRadius = attackRadius;
        _viewAngle = viewAngle;
        _viewRadius = viewRadius;
        _enemyAnimController = enemyAnimController;
    }

    public void OnExit()
    {
        
    }

    public void OnStart()
    {
        _enemyAnimController.SetIsWalk(1f);
    }

    public void OnUpdate()
    {
        if (!_enemieBase.isDamaged)
        {
            if (_enemieBase.InFOV(_enemyTransform,_enemieBase.target,_viewRadius,_viewAngle))
            {
                if (_enemieBase.InFOV(_enemyTransform, _enemieBase.target, _attackRadius, _viewAngle))
                {
                    _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyAttack);
                    
                }
                else
                {
                    
                    PersuitTarget();
                }
            }
            else
            {
                _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyIdle);
            }
        }
        else
        {
            _enemySM.EnemyChangeStates(EnemyStates.EnemyKnockBack);
        }
    }

    void PersuitTarget()
    {
        //_enemyAnimController.SetIsWalk(1f);
        Vector3 pos = Vector3.MoveTowards(_enemyTransform.position, _enemieBase.target.transform.position, _enemieBase.maxSpeed  * Time.deltaTime);
        _myRB.MovePosition(pos);
        _enemyTransform.LookAt(new Vector3(_enemieBase.target.transform.position.x, 1.5f, _enemieBase.target.transform.position.z));
    }


    
}
