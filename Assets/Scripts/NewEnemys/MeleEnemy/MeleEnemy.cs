using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleEnemy : MonoBehaviour, IDamage
{
    [Header("State Machine")]
    EnemySM _enemySM;

    [Header("EnemieBase")]
    [SerializeField] EnemySO _enemySO;
    [SerializeField] KnockBackSO _knockBackSO;
    EnemieBase _enemieBase;
    public EnemieBase enemimeBase { get { return _enemieBase; } private set { } }

    [Header("Parameters")]
    [SerializeField] float _viewRadius;
    [SerializeField] float _viewAngle;
    [SerializeField] float _attackRadius;
    [SerializeField] Animator _animator;
    Rigidbody _myRB;
    [SerializeField] float _attackInterval;

    [Header("Patrol Parameters")]
    [SerializeField] Vector3 _SpawnerWayCenter;
    [SerializeField] Vector3 _SpawnerWaySize;
    [SerializeField] GameObject _waypointPref;
    [SerializeField] List<GameObject> _allWaypoints = new List<GameObject>();
    int _randomCantWaypoints;
    int _currWaypoint;

    [SerializeField] Collider _myCollider;

    [Header("Controllers")]
    EnemyAnimController _enemyAnimController;
    SkinnedMeshRenderer _skinMeshRender;


    private void Awake()
    {
        _enemyAnimController = new EnemyAnimController(_animator);
        _myRB = GetComponent<Rigidbody>();
        _myCollider = GetComponent<Collider>();
    }
    void Start()
    {
        _enemieBase = new EnemieBase(_enemySO,_knockBackSO);
        _enemySM = new EnemySM();

        
        _skinMeshRender = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        //_skinMeshRender.material.color = Color.blue;

        _enemySM.AddEnemyStates(EnemyStates.MeleEnemyIdle, new MeleEnemyIdle(_enemieBase,_enemySM,this.transform,_viewRadius,_viewAngle,_enemyAnimController));
        _enemySM.AddEnemyStates(EnemyStates.EnemyPatrol, new EnemyPatrolState(_enemieBase, _enemySM, _viewRadius,_viewAngle, _allWaypoints, this.transform,_myRB, _currWaypoint, _enemyAnimController));
        _enemySM.AddEnemyStates(EnemyStates.MeleEnemyPersuit, new MeleEnemyPersuit(_enemieBase,_enemySM,this.transform,_myRB,_attackRadius,_viewAngle, _viewRadius,_enemyAnimController));
        _enemySM.AddEnemyStates(EnemyStates.MeleEnemyAttack, new MeleEnemyAttack(_enemieBase,_enemySM,_attackRadius,_viewAngle,_enemyAnimController,this.transform,_attackInterval));
        _enemySM.AddEnemyStates(EnemyStates.EnemyKnockBack, new EnemyKnockBack(_enemieBase,_enemySM,_myRB,this.transform,_skinMeshRender,_enemyAnimController));

        _enemySM.EnemyChangeStates(EnemyStates.EnemyPatrol);
        _randomCantWaypoints = Random.Range(2, 9);
       
        SpawnWaypoint();
    }

    private void Update()
    {
        _enemySM.Update();
        CheckNearTarget();

        if (_allWaypoints.Count <= _randomCantWaypoints)
        {
            SpawnWaypoint();
        }

        if (_enemieBase.currentLife <= 0)
        {
            OnDeath();
        }
        
    }

    public void SpawnWaypoint()
    {
        Vector3 pos = (this.transform.position + _SpawnerWayCenter) + new Vector3(Random.Range(-_SpawnerWaySize.x / 2, _SpawnerWaySize.x / 2), Random.Range(-_SpawnerWaySize.y / 2, _SpawnerWaySize.y / 2), Random.Range(-_SpawnerWaySize.z / 2, _SpawnerWaySize.z / 2));

        _allWaypoints.Add(Instantiate(_waypointPref, pos, Quaternion.identity));
    }
    void CheckNearTarget()
    {
        if (GameManager.Instance._players.Count >= 1)
        {
            if (!GameManager.Instance.isSinglePlayer)
            {
                var distanceP1 = Vector3.Distance(this.transform.position, GameManager.Instance._players[0].transform.position);
                var distanceP2 = Vector3.Distance(this.transform.position, GameManager.Instance._players[1].transform.position);
                if (distanceP1 < distanceP2)
                {
                    _enemieBase.target = GameManager.Instance._players[0].gameObject;
                }
                else
                {
                    _enemieBase.target = GameManager.Instance._players[1].gameObject;
                }
            }
        }else _enemieBase.target = GameManager.Instance._players[0].gameObject;
    }
    public void onDamage(float damage)
    {
        
        _enemieBase.OnDamage(damage);
        _enemieBase.isDamaged = true;
    }

    void OnDeath()
    {
        _myCollider.enabled = false;
        _enemyAnimController.SetIsDeath();
        Destroy(this.gameObject, 2f);
    }
 
    public Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);


        Vector3 lineA = GetVectorFromAngle(_viewAngle / 2 + transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-_viewAngle / 2 + transform.eulerAngles.y);

        Gizmos.DrawLine(transform.position, transform.position + lineA * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * _viewRadius);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(this.transform.position + _SpawnerWayCenter, _SpawnerWaySize);
    }
}
