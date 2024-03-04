using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region States
public enum EnemyStates {
    MeleEnemyPersuit,
    MeleEnemyIdle,
    EnemyKnockBack,
    MeleEnemyAttack,
    EnemyPatrol
}
#endregion
public class EnemieBase 
{
    float _maxLife;
    float _currentLife;
    float _maxSpeed;
    float _knockBackStrength;
    float _knockBackDuration;
    Color _knockBackColor { get; set; }
    Color _originalColor;
    float _blinkInterval;
    float _attackPower;
    public GameObject target;
    LayerMask _wall;
    bool _isDamaged;


    #region Getters // Setters
    public float currentLife { get { return _currentLife; } private set { } }
    public float maxSpeed { get { return _maxSpeed; }  set { _maxSpeed = value; } }
    public float knockbackStrength { get { return _knockBackStrength; } private set { } }
    public float knockBackDuration { get { return _knockBackDuration; } private set { } }
    public Color knockBackColor { get { return _knockBackColor; } private set { } }
    public Color originalColor { get { return _originalColor; } private set { } }
    public float blinlInterval { get { return _blinkInterval; } private set { } }
    public float attackPower { get { return _attackPower; } private set { } }
    public bool isDamaged { get { return _isDamaged; }  set { _isDamaged = value; } }

    #endregion
    public EnemieBase(EnemySO enemySO,KnockBackSO knockBackSO)
    {
        _maxLife = enemySO.maxLife;
        _currentLife = _maxLife;
        _maxSpeed = enemySO.maxSpeed;
        _knockBackStrength = knockBackSO.knockBackStrength;
        _knockBackDuration = knockBackSO.knockBackDuration;
        _knockBackColor = knockBackSO.knockBackColor;
        _originalColor = knockBackSO.originalColor;
        _blinkInterval = knockBackSO.blinkInterval;
        _attackPower = enemySO.attackPower;
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
