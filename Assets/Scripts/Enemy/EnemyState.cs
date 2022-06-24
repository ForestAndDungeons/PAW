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

    public EnemyState(State state, EnemyAnimatorController enemyAnimController,EnemyMovement enemyMovement,List<Transform> targets, EnemySoundsManager enemySoundsManager,Enemy enemy,bool isRangeEnemy,float timeBtwShoot, float startTimeBtwShoot)
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
                    _enemyAnimController.OnIdle();
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
                    Debug.Log("Estoy en escape");
                    if (_targets.Count > 0)
                    {
                        _enemyMovement.Escape(_targets[0]);
                        _enemyAnimController.OnWalking();
                        if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                        {
                            _isAttack = true;
                            isAttack();
                        }
                    }

                }
                else
                {
                    isDie();
                }
                break;
            case State.attack:
                if (_isAttack)
                {
                    _transform.LookAt(_targets[0].position);
                    if (_timeBtwShoot <= 0)
                    {
                        _enemyAnimController.OnAttack();
                        _enemy.InstantiateArrow();
                        _timeBtwShoot = _startTimeBtwShoot;
                        Debug.Log("El enemigo "+_enemy.gameObject.name+" de rango ataca");
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
                            _enemyAnimController.OnAttackEnd();
                            isEscape();
                    }
                }
                break;
            case State.die:
                _enemy.DestroyThisObject();
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
                if (!isDead)
                {
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
                }
                else
                {
                    isDie();
                }
                break;
            case State.attack:
                if (_isAttack)
                {
                    _enemyAnimController.OnAttack();

                    if (_targets.Count == 0)
                    {
                        isIdle();
                    }
                    if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                    {
                        _enemyAnimController.OnAttackEnd();
                        _isAttack = false;
                        isPersuit();
                    }
                }
                break;
            case State.die:
                _enemy.DestroyThisObject();
                _enemyAnimController.OnDeath();
                break;
            default:
                Debug.Log("no entre a ningun state");
                break;
        }
    }
    /*public void StateUpdate()
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
                if (!isDead)
                {
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
                }
                else
                {
                    isDie();
                }
                break;
            case State.attack:
                if (_isAttack)
                {
                    _enemyAnimController.OnAttack();
                    if (_targets.Count == 0)
                    {
                        isIdle();
                    }
                    if (Vector3.Distance(_transform.position, _targets[0].position) >= _distanceBrake)
                    {
                        _enemyAnimController.OnAttackEnd();
                        _isAttack = false;
                        isPersuit();
                    }
                }
                break;
            case State.die:
                _enemy.DestroyThisObject();
                _enemyAnimController.OnDeath();
                break;
            default:
                Debug.Log("no entre a ningun state");
                break;
        }
    }*/



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
