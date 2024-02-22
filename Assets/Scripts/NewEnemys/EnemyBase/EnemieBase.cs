using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates {
    MeleEnemyPersuit,
    MeleEnemyIdle
}
public class EnemieBase 
{
    float _maxLife;
    float _currentLife;
    float _maxSpeed;
    float _knockBackSpeed;
    float _knockBackDuration;
    Color _knockBackColor;
    float _attackPower;
    LayerMask _wall;
    public GameObject target;
    

    public EnemieBase(EnemySO enemySO)
    {
        _maxLife = enemySO.maxLife;
        _currentLife = _maxLife;
        _maxSpeed = enemySO.maxSpeed;
        _knockBackSpeed = enemySO.knockBackSpeed;
        _knockBackDuration = enemySO.knockBackDuration;
        _knockBackColor = enemySO.knockBackColor;
        _attackPower = enemySO.attackPower;
        target = GameManager.Instance._player1Prefab;
        _wall = enemySO.walls;
    }

    
    public void OnDamage(float damage)
    {
        if (_currentLife > 0) _currentLife -= damage;
        else _currentLife = 0;
    }

    public bool InFOV(Transform mypos, GameObject obj, float viewRadius, float viewAngle)
    {
        if (Vector3.Distance(mypos.position, obj.transform.position) > viewRadius) { return false; }

        if (Vector3.Angle(mypos.forward, obj.transform.position - mypos.position) > (viewAngle / 2)) { return false; }

        return InLOS(mypos.position, obj.transform.position); 
    }

    public bool InLOS(Vector3 mypos, Vector3 objPos)
    {
        var dir = objPos - mypos;
        return !Physics.Raycast(mypos, dir, dir.magnitude, _wall);
    }
}
