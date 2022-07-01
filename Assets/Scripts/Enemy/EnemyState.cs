using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    State _currentState;
    EnemyAnimatorController _enemyAnimController;
    EnemyMovement _enemyMovement;
    List<Transform> _targets;
    EnemySoundsManager _enemySoundsManager;
    Enemy _enemy;
    bool _isAttack;
    public bool isDead = false;
    bool _isRangeEnemy;
    float _timeBtwShoot;
    float _startTimeBtwShoot;
    float _distanceFollow;

    public EnemyState(State state, EnemyAnimatorController enemyAnimController,EnemyMovement enemyMovement,List<Transform> targets, EnemySoundsManager enemySoundsManager,Enemy enemy,bool isRangeEnemy,float timeBtwShoot, float startTimeBtwShoot, float distanceFollow)
    {
        _currentState = state;
        _enemyAnimController = enemyAnimController;
        _enemyMovement = enemyMovement;
        _targets = targets;
        _enemySoundsManager = enemySoundsManager;
        _enemy = enemy;
        _isRangeEnemy = isRangeEnemy;
        _timeBtwShoot = timeBtwShoot;
        _startTimeBtwShoot = startTimeBtwShoot;
        _distanceFollow = distanceFollow;
    }

    public void StateStart()
    {
        if (_isRangeEnemy)
        {
            _enemy._stateDelegate = StateEnemyRange;
            
        }
        else
        {
            _enemy._stateDelegate = StateSkeleton;
            
        }
    }

    public void StateEnemyRange()
    {
        Transform _transform = _enemyMovement.MyTransformGetter();
        float _distanceBrake = _enemyMovement.DistanceBrakeGetter();

        switch (_currentState)
        {
            case State.idle:
                if (_targets.Count == 0)
                {
                    _enemyAnimController.OnMovement(0);
                }
                else if(_targets.Count > 0)
                {
                    if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                    {
                        _isAttack = true;
                        isAttack();
                    }
                    else if (Vector3.Distance(_transform.position, _targets[0].position) <= _distanceBrake)
                    {
                        _isAttack = false;
                        isEscape();
                    }

                }
                break;
            case State.escape:
                if (!isDead)
                {
                    _transform.LookAt(_targets[0].position);
                    if (_targets.Count > 0)
                    {
                        _enemyMovement.Escape(_targets[0]);
                        _enemyAnimController.OnMovement(1);
                        if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                        {
                            _isAttack = true;
                            isAttack();
                        }
                        else
                        {
                            isIdle();
                        }
                    }
                    else
                    {
                        isIdle();
                    }

                }
                else
                {
                    isDie();
                }
                break;
            case State.attack:
                if (!isDead)
                {

                    if (_isAttack)
                    {
                        _transform.LookAt(_targets[0].position);
                        if (_timeBtwShoot <= 0)
                        {
                            _enemyAnimController.OnAttack(true);
                            _enemy.InstantiateArrow();
                            _timeBtwShoot = _startTimeBtwShoot;
                        }
                        else
                        {
                            _timeBtwShoot -= Time.deltaTime;
                        }
                        if (_targets.Count == 0)
                        {
                                isIdle();
                        }
                        if (Vector3.Distance(_transform.position, _targets[0].position) <= _distanceBrake)
                        {
                                _isAttack = false;
                                _enemyAnimController.OnAttack(false);
                                isEscape();
                        }
                    }
                }
                else
                {
                    isDie();
                }
                break;
            case State.die:
                _enemyAnimController.OnDeath();
                break;
        }
    }

    public void StateSkeleton()
    {
        Transform _transform = _enemyMovement.MyTransformGetter();
        float _distanceBrake = _enemyMovement.DistanceBrakeGetter();

        switch (_currentState)
        {
            case State.idle:
                if (_targets.Count > 0)
                {
                    if (Vector3.Distance(_transform.position, _targets[0].position) > _distanceFollow)
                    {
                        _enemyAnimController.OnMovement(0);
                    }
                    else
                    {
                        isPersuit();
                    }
                }
                break;
            case State.persuit:
                if (!isDead)
                {
                    if (_targets.Count > 0)
                    {
                        _enemyMovement.FollowToPlayer(_targets[0]);
                        _enemyAnimController.OnMovement(1);
                        if (Vector3.Distance(_transform.position, _targets[0].position) <= _distanceBrake)
                        {
                            _isAttack = true;
                            isAttack();
                        }
                        else
                        {
                            isIdle();
                        }
                    }
                    else
                    {
                        isIdle();
                    }
                }
                else
                {
                    isDie();
                }
                break;
            case State.attack:
                if (!isDead)
                {
                    if (_isAttack)
                    {
                        _enemyAnimController.OnAttack(true);

                        if (_targets.Count == 0)
                        {
                            isIdle();
                        }
                        if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                        {
                            _enemyAnimController.OnAttack(false);
                            _isAttack = false;
                            isPersuit();
                        }
                    }
                }
                else
                {
                    isDie();
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


    // Funciones Para pasar de State
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

    public void isEscape()
    {
        _currentState = State.escape;
    }

    public State CurrentStateGetter()
    {
        return _currentState;
    }

    public bool IsAttackGetter() { return _isAttack; }
}
