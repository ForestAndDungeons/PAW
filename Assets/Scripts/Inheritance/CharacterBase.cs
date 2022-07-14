using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase
{
    protected string _name;
    protected float _maxHealth;
    protected float _currentHealth;
    protected float _attackPower;
    protected float _armor;
    protected float _money;
    protected bool _haveAKey;
    protected bool _isBlocking;
    protected bool _isImmune;
    protected float _immuneTime;
    protected float _forceJump;

    public abstract void onAttack(Collision other);
    public abstract void HealthUp(float healing);

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetAttackPower()
    {
        return _attackPower;
    }

    public float GetArmor()
    {
        return _armor;
    }

    public float GetMoney()
    {
        return _money;
    }

    public void SetMoney(float money)
    {
        _money = money;
    }

    public bool GetKey()
    {
        return _haveAKey;
    }

    public void SetKey(float haveAKey)
    {
        _haveAKey = System.Convert.ToBoolean(haveAKey);
    }

    public void SetIsBlocking(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }

    public void SetIsImmune(bool isImmune)
    {
        _isImmune = isImmune;
    }

    public void AttackUp(float add)
    {
        _attackPower += add;
    }
    public void MoneyUp(float add)
    {
        _money += add;
    }
}