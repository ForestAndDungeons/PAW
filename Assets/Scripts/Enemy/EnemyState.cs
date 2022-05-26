using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    State _currentState;
    EnemyAnimatorController _enemyAnimController;
    EnemyMovement _enemyMovement;
    List<Transform> _targets;
    bool _isAttack;
    public EnemyState(State state, EnemyAnimatorController enemyAnimController,EnemyMovement enemyMovement,List<Transform> targets)
    {
        _currentState = state;
        _enemyAnimController = enemyAnimController;
        _enemyMovement = enemyMovement;
        _targets = targets;
    }

    public void StateUpdate()
    {
        Transform _transform = _enemyMovement.MyTransformGetter();
        float _distanceBrake = _enemyMovement.DistanceBrakeGetter();

        switch (_currentState)
        {
            case State.idle:
                if (_targets.Count == 0)
                {
                    _enemyAnimController.OnIdle();
                }
                else
                {
                    isPersuit();
                }
                break;
            case State.persuit:
                if (_targets.Count > 0)
                {
                    _enemyMovement.FollowToPlayer(_targets[0]);
                    _enemyAnimController.OnWalking();
                    if (Vector3.Distance(_transform.position, _targets[0].position) <= _distanceBrake)
                    {
                        _isAttack = true;
                        isAttack();    
                    }
                }
                break;
            case State.attack:
                if (_isAttack)
                {
                    _enemyAnimController.OnAttack();
                    if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                    {
                        _enemyAnimController.OnAttackEnd();
                        _isAttack = false;
                        isPersuit();
                    }
                }
                break;
            case State.die:
                _enemyAnimController.OnDeath();
                break;
            default:
                Debug.Log("no entre a ningun state");
                break;
        }
    }

    public void isIdle()
    {
        _currentState = State.idle;
    }
    public void isPersuit()
    {
        _currentState = State.persuit;
    }
    public void isAttack()
    {
        _currentState = State.attack;
    }
    public void isDie()
    {
        _currentState = State.die;
    }

    public State CurrentStateGetter()
    {
        return _currentState;
    }

}
