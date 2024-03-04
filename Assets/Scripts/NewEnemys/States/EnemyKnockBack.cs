using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : IState
{
    #region Parameters
    EnemieBase _enemieBase;
    EnemySM _enemySM;
    Rigidbody _enemyRB;
    Transform _enemyTransform;
    SkinnedMeshRenderer _skinMeshRenderer;
    float _startTimeKnockBack;
    float _lastBlinkTime;
    int _blinkCounter;
    EnemyAnimController _enemyAnimController;
    #endregion
    public EnemyKnockBack(EnemieBase enemieBase, EnemySM enemySM, Rigidbody enemyRB, Transform enemyTransform, SkinnedMeshRenderer skinMeshRenderer, EnemyAnimController enemyAnimController)
    {
        _enemieBase = enemieBase;
        _enemySM = enemySM;
        _enemyRB = enemyRB;
        _enemyTransform = enemyTransform;
        _skinMeshRenderer = skinMeshRenderer;
        _enemyAnimController = enemyAnimController;
    }

    public void OnExit()
    {
        _skinMeshRenderer.material.color = _enemieBase.originalColor;
    }

    public void OnStart()
    {
        _startTimeKnockBack = Time.time;
        _enemyAnimController.SetIsHit();
    }

    public void OnUpdate()
    {
        if (_enemieBase.isDamaged)
        {
            if (Time.time - _lastBlinkTime >= _enemieBase.blinlInterval)
            {
                _skinMeshRenderer.material.color = (_skinMeshRenderer.material.color == _enemieBase.originalColor) ? _enemieBase.knockBackColor : _enemieBase.originalColor;
                _lastBlinkTime = Time.time;
                _blinkCounter++;
            }
            float _actualTime = Time.time - _startTimeKnockBack;
            float _porCompleted = _actualTime / _enemieBase.knockBackDuration;

            if (_porCompleted >=1f)
            {
                _enemieBase.isDamaged = false;
            }
            else
            {
                Vector3 dir = new Vector3((_enemyTransform.position.x - _enemieBase.target.transform.position.x) ,0f, (_enemyTransform.position.z - _enemieBase.target.transform.position.z));
                dir = dir.normalized;
                _enemyRB.AddForce(dir * _enemieBase.knockbackStrength, ForceMode.Impulse);
            }

        }
        else
        {
            _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyIdle);
        }
    }
}
