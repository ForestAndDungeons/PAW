using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateBoss { idle, Jump, FireBall, SpawnEnemy, die};
public class Boss : MonoBehaviour , IDamage
{
    [Header("Boss Lists")]
    [SerializeField] List<Transform> _enemySpawnPos = new List<Transform>();

    [Header("Variables Boss")]
    [SerializeField] bool _isInvulerable;

    [Header("Boss State")]
    [SerializeField] StateBoss _stateBoss;

    [Header("Variables BoosBase")]
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;
    [SerializeField] ParticleSystem _particleSystem;

    [Header("Timers")]
    [SerializeField] float _startTimeBtwIdle;
    [SerializeField] float _startTimeBtwShoot;
    [SerializeField] float _startTimeBtwFallFloor;
    [SerializeField] float _startTimeBtwSpawnEnemy;
    float _timeBtwIdle;
    float _timeBtwShoot;
    float _timeBtwFallFloor;
    float _timeBtwSpawnEnemy;

    [Header("Animator Controller")]
    [SerializeField] Animator _bossAnim;

    [Header("Sounds Manager")]
    [SerializeField] AudioSource _bossAudioSource;
    [SerializeField] AudioClip[] _bossAClip;

    BossAnimController _bossAnimController;
    [HideInInspector] public EnemySoundsManager enemySoundsManager;
    [HideInInspector] public BossBase _bossBase;
    BossState _bossState;
    FallFloorSpawner _fallFloorSpawner;
    FireBallSpawner _fireBallSpawner;
    [Header("Scripts")]
    [SerializeField] Fireball _fireball;
    [SerializeField] InmolateEnemy _inmolateEnemy;

    [Header("RoomEntity")]
    public RoomEntity _roomEntity;


    [SerializeField] Animator _bridge;
    [SerializeField] GameObject _wall;

    void Awake()
    {
        _fallFloorSpawner = GetComponentInChildren<FallFloorSpawner>();
        _fireBallSpawner = GetComponentInChildren<FireBallSpawner>();
        _timeBtwShoot = _startTimeBtwShoot;
        _timeBtwIdle = _startTimeBtwIdle;
        _timeBtwFallFloor = _startTimeBtwFallFloor;
        _timeBtwSpawnEnemy = _startTimeBtwSpawnEnemy;
        //enemySoundsManager = new EnemySoundsManager(_bossAudioSource, _bossAClip);
        _bossAnimController = new BossAnimController(_bossAnim);
        _bossState = new BossState(_stateBoss, _bossAnimController,_bossAClip,_bossAudioSource, this, _timeBtwShoot, _startTimeBtwShoot,_enemySpawnPos,_roomEntity,_maxHealth,_startTimeBtwIdle,_timeBtwIdle,_startTimeBtwFallFloor,_timeBtwFallFloor,_startTimeBtwSpawnEnemy,_timeBtwSpawnEnemy,_fallFloorSpawner, _fireball, _fireBallSpawner,_inmolateEnemy,_bridge,_wall);
        _bossBase = new BossBase(_name, _maxHealth, _attackPower, _armor, this, _bossAudioSource, _bossAClip, _particleSystem, _bossAnimController,_bossState);
        _name = this.gameObject.name;
        

    }
    //GETTERS
    public float GetCurrentHealth() {return _currentHealth;}
    void Update()
    {
        _stateBoss = _bossState.CurrentStateGetter();
        _currentHealth = _bossBase.GetCurrentHealth();
        _bossState.BossStateUpdate();
    }

    //Other Funtions
    public void PartialSoundAttack()
    {
        enemySoundsManager.playOnAttack();
    }
    public void DestroyThisObject()
    {
        if (_roomEntity != null)
        {
            _roomEntity.ElimEnemyInList(this.gameObject);
            Destroy(this.gameObject, 0.3f);
        }
    }

    public void StopHit()
    {
        _bossAudioSource.PlayOneShot(_bossAClip[4]);
        _bossAnimController.OnHit(false);
    }
    public void SoundJump()
    {
        _bossAudioSource.PlayOneShot(_bossAClip[3]);
    }

    public void SoundFireball()
    {
        _bossAudioSource.PlayOneShot(_bossAClip[1]);
    }

    public void onDamage(float damage)
    {
        _bossBase.onDamage(damage);
    }

    public void onAttack(Collision other)
    {
        _bossBase.onAttack(other);
    }

    public void HealthUp(float add)
    {
        _bossBase.HealthUp(add);
    }
}