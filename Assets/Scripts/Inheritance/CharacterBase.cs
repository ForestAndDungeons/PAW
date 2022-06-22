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

    public float maxHealthGetter()
    {
        return _maxHealth;
    }

    public float currentHealthGetter()
    {
        return _currentHealth;
    }

    public float attackPowerGetter()
    {
        return _attackPower;
    }

    public float armorGetter()
    {
        return _armor;
    }

    public bool KeyGetter()
    {
        return _haveAKey;
    }

    public void KeySetter(bool haveAKey)
    {
        _haveAKey = haveAKey;
    }

    public bool IsBlockingGetter()
    {
        return _isBlocking;
    }

    public void IsBlockingSetter(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }

    public bool IsImmuneGetter()
    {
        return _isImmune;
    }

    public void IsImmuneSetter(bool isImmune)
    {
        _isImmune = isImmune;
    }
}
