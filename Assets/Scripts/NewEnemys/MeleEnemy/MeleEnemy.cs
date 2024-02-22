using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour, IDamage
{
    [Header("State Machine")]
    EnemySM _enemySM;

    [Header("EnemieBase")]
    [SerializeField] EnemySO _enemySO;
    EnemieBase _enemieBase;
    //[SerializeField] GameObject _currentTarget;

    [Header("Parameters")]
    [SerializeField] float _viewRadius;
    [SerializeField] float _viewAngle;

    void Start()
    {
        _enemieBase = new EnemieBase(_enemySO);
        _enemySM = new EnemySM();
        _enemySM.AddEnemyStates(EnemyStates.MeleEnemyIdle, new MeleEnemyIdle(_enemieBase,this.transform,_viewRadius,_viewAngle));

        _enemySM.EnemyChangeStates(EnemyStates.MeleEnemyIdle);
    }

    private void Update()
    {
        _enemySM.Update();
        
    }

    public void onDamage(float damage)
    {
        Debug.Log($"Damaged {damage}");
    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Vector3 lineA = GetVectorFromAngle(_viewAngle / 2 + transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-_viewAngle / 2 + transform.eulerAngles.y);

        Gizmos.DrawLine(transform.position, transform.position + lineA * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * _viewRadius);
    }
}
