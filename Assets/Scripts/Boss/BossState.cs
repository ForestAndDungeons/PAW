using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState 
{
    StateBoss _currentState;
    //Objetos utilizados
    BossAnimController _bossAnimController;
    List<Transform> _enemySpawnPos;
    AudioClip[] _bossAudioClip;
    AudioSource _bossAudioSource;
    RoomEntity _roomEntity;
    Boss _boss;
    FallFloorSpawner _fallFloorSpawner;
    FireBallSpawner _fireBallSpawner;
    Fireball _fireball;
    InmolateEnemy _inmolateEnemy;
    //Variables
    bool _isFireBall = false;
    bool _rageMode = false;
    public bool isDead = false;
    float _randomSpawnFloor;
    float _randomSpawnEnemies;
    int _randomSpawnRatPos;
    float _currentSpawnFloor;
    float _currentSpawnEnemies;
    //Tiempos
    float _timeBtwIdle;
    float _timeBtwShoot;
    float _timeBtwFallFloor;
    float _timeBtwSpawnEnemy;
    float _startTimeBtwIdle;
    float _startTimeBtwShoot;
    float _startTimeBtwFallFloor;
    float _startTimeBtwSpawnEnemy;
    // Porcentaje de vida
    float _80Porvit;
    float _60Porvit;
    float _40Porvit;
    float _20Porvit;

    public BossState(StateBoss Bossstate, BossAnimController bossAnimController, AudioClip[] bossAudioClip, AudioSource bossAudioSource, Boss boss, float timeBtwShoot, float startTimeBtwShoot, List<Transform> enemySpawnPos,RoomEntity roomEntity,float maxHeath,float startTimeBtwIdle, float timeBtwIdle, float startTimeBtwFallFloor, float timeBtwFallFloor, float startTimeBtwSpawnEnemy, float timeBtwSpawnEnemy, FallFloorSpawner fallFloorSpawner, Fireball fireball,FireBallSpawner fireBallSpawner, InmolateEnemy inmolateEnemy)
    {
        _currentState = Bossstate;
        _bossAnimController = bossAnimController;
        _bossAudioClip = bossAudioClip;
        _bossAudioSource = bossAudioSource;
        _roomEntity = roomEntity;
        _boss = boss;
        _fallFloorSpawner = fallFloorSpawner;
        _enemySpawnPos = enemySpawnPos;
        _fireBallSpawner = fireBallSpawner;
        _fireball = fireball;
        _inmolateEnemy = inmolateEnemy;
        _timeBtwShoot = timeBtwShoot;
        _startTimeBtwShoot = startTimeBtwShoot;
        _timeBtwIdle = timeBtwIdle;
        _startTimeBtwIdle = startTimeBtwIdle;
        _timeBtwFallFloor = timeBtwFallFloor;
        _startTimeBtwFallFloor = startTimeBtwFallFloor;
        _timeBtwSpawnEnemy = timeBtwSpawnEnemy;
        _startTimeBtwSpawnEnemy = startTimeBtwSpawnEnemy;
        _80Porvit = (maxHeath * 80) / 100;
        _60Porvit = (maxHeath * 60) / 100;
        _40Porvit = (maxHeath * 40) / 100;
        _20Porvit = (maxHeath * 20) / 100;
    }

    public void BossStateUpdate()
    {
        //Debug.Log("80% = "+_80Porvit+" 60% = "+_60Porvit+" 40% = "+_40Porvit+" 20% = "+ _20Porvit);
        // el boss Tendria 15 hp -- a los 80% de vida (12) Activa Jump, termina Jump y se activa nuevamente Fireball. -- a los 60% de vida (9) se activa estado SpawnEnemy, termina SpawnEnemy vuelve a fireball.--A los 40% (6) de vida Activa Jump, termina Jump y se activa nuevamente Fireball .-- A los 20%(3)Se activa RageMode, Fireballs mas rapidas y se intercambian entre Jump y Spawn enemie al mismo tiempo
        switch (_currentState)
        {
            case StateBoss.idle:
                // Se encuentra en idle hasta que los personajes entran a la habitacion.
                if (!isDead)
                {
                    //Pasan unos segundos de CoolDown y pasa a Fireball
                    if (_roomEntity.GetPlayerList().Count > 0)
                    {
                        
                        if (_timeBtwIdle <= 0)
                        {
                            _bossAnimController.OnIdle(false);
                            _bossAudioSource.PlayOneShot(_bossAudioClip[0]);
                            _timeBtwIdle = _startTimeBtwIdle;
                            _isFireBall = true;
                            isFireBall();
                        }
                        else
                        {
                            _timeBtwIdle -= Time.deltaTime;
                        }

                    }
                    else
                    {
                       _bossAnimController.OnIdle(true);
                    }


                }
                else
                {
                    isDie();
                }
                break;
            case StateBoss.Jump:
                if (!isDead)
                {
                    //_bossAudioSource.PlayOneShot(_bossAudioClip[2]);
                    if (_currentSpawnFloor < _randomSpawnFloor)
                    {
                        if (_timeBtwFallFloor <= 0)
                        {
                            _bossAnimController.OnJump(true);
                            _fallFloorSpawner.SpawnFloor();
                            _currentSpawnFloor++;
                            _timeBtwFallFloor = _startTimeBtwFallFloor;
                        }
                        else
                        {
                             _timeBtwFallFloor -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        _bossAnimController.OnJump(false);
                        if (!_isFireBall)
                        {
                            _isFireBall = true;
                            _boss._bossBase.onDamage(1);
                        }
                        else
                        {
                            isFireBall();
                        }
                    }
                }
                else
                {
                    isDie();
                }
                break;
            case StateBoss.FireBall:
                // Ataca con Coldown Luego de unos segundos que entra los Pj a la sala
                if (!isDead)
                {
                    //Ataca con firebal con transforms randoms
                    if (_isFireBall)
                    {
                        if (!_rageMode)
                        {
                            Debug.Log(_boss.GetCurrentHealth());
                            if (_timeBtwShoot <= 0)
                            {
                                _bossAnimController.OnFireBall(true);
                                _fireBallSpawner.SpawnFireBall();
                                _timeBtwShoot = _startTimeBtwShoot;
                            }
                            else
                            {
                                _timeBtwShoot -= Time.deltaTime;
                            }
                            if (_boss.GetCurrentHealth() == _80Porvit || _boss.GetCurrentHealth() == _40Porvit)
                            {
                                _bossAnimController.OnFireBall(false);
                                _randomSpawnFloor = Random.Range(6,13);
                                _currentSpawnFloor = 0;
                                _isFireBall = false;
                                isJump();
                            }
                            else if (_boss.GetCurrentHealth() == _60Porvit)
                            {
                                _bossAnimController.OnFireBall(false);
                                _randomSpawnEnemies = Random.Range(4,9);
                                _currentSpawnEnemies = 0;
                                _isFireBall = false;
                                isSpawnEnemy();
                            }
                            /*else if (_boss.GetCurrentHealth() == _20Porvit)
                            {
                                _rageMode = true;
                            }*/
                        }

                    }
                }
                else
                {
                    _bossAnimController.OnFireBall(false);
                    isDie();
                }
                break;
            case StateBoss.SpawnEnemy:
                if (!isDead)
                {
                    if (_currentSpawnEnemies < _randomSpawnEnemies)
                    {
                        if (_timeBtwSpawnEnemy <= 0)
                        {
                            _randomSpawnRatPos = Random.Range(0, 4);
                            _bossAnimController.OnSpawnEnemy(true);
                            _bossAudioSource.PlayOneShot(_bossAudioClip[6]);
                            Debug.Log("_randomSpawnRatPos: " + _randomSpawnRatPos);
                            Debug.Log("x :" + _enemySpawnPos[_randomSpawnRatPos]);
                            _inmolateEnemy.InstantiateInmolateEnemy(_enemySpawnPos[_randomSpawnRatPos]);
                            _currentSpawnEnemies++;
                            _timeBtwSpawnEnemy = _startTimeBtwSpawnEnemy;
                        }
                        else
                        {
                            _timeBtwSpawnEnemy -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        _bossAnimController.OnSpawnEnemy(false);
                        if (!_isFireBall)
                        {
                            _isFireBall = true;
                            _boss._bossBase.onDamage(1);
                        }
                        else
                        {
                            isFireBall();
                        }
                    }
                }
                else
                {
                    isDie();
                }
                break;
            case StateBoss.die:
                _bossAnimController.OnDeath();
                break;
        }
    }

    // Funciones Para pasar de State
    public void isIdle()
    {
        _currentState = StateBoss.idle;
    }
    public void isJump()
    {
        _currentState = StateBoss.Jump;
    }
    public void isFireBall()
    {
        _currentState = StateBoss.FireBall;
    }
    public void isSpawnEnemy()
    {
        _currentState = StateBoss.SpawnEnemy;
    }
    public void isDie()
    {
        _currentState = StateBoss.die;
    }
    public StateBoss CurrentStateGetter()
    {
        return _currentState;
    }

    public bool IsAttackGetter() { return _isFireBall; }
}
