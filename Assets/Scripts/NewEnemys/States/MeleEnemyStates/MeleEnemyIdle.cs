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
    #endregion
    public MeleEnemyIdle(EnemieBase enemieBase,Transform enemyTransform,float viewRadius,float viewAngle)
    {
        _enemieBase = enemieBase;
        _enemyTransform = enemyTransform;
        _viewRadius = viewRadius;
        _viewAngle = viewAngle;
    }

    public void OnExit()
    {
        
    }

    public void OnStart()
    {
        Debug.Log("Estoy en MeleIdle");
    }

    public void OnUpdate()
    {
        if (Vector3.Distance(_enemyTransform.position,_enemieBase.target.transform.position)<= _viewRadius)
        {
            Debug.Log("veo al player 1 sin fov");
        }
    }
}
