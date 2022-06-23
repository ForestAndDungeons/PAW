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
    protected bool _haveAKey;
    protected bool _isBlocking;
    protected bool _isImmune;
    protected float _immuneTime;

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

    public bool GetKey()
    {
        return _haveAKey;
    }

    public void SetKey(bool haveAKey)
    {
        _haveAKey = haveAKey;
    }

    public void SetIsBlocking(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }

    public void SetIsImmune(bool isImmune)
    {
        _isImmune = isImmune;
    }
}