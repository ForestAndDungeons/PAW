using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase
{
    protected string _name;

    protected float _maxHealth;
    public float maxHealth { get { return this._maxHealth; } set { this._maxHealth = value; } }

    protected float _currentHealth;
    public float currentHealth { get { return this._currentHealth; } set { this._currentHealth = value; } }

    protected float _attackPower;
    public float attackPower { get { return this._attackPower; } set { this._attackPower = value; } }

    protected float _armor;
    public float armor { get { return this._armor; } set { this._armor = value; } }

    protected float _money;
    public float money { get { return this._money; } set { this._money = value; } }

    protected bool _haveKey;
    public bool haveKey { get { return this._haveKey; } set { this._haveKey = value; } }

    protected bool _isBlocking;
    public bool isBlocking { get { return this._isBlocking; } set { this._isBlocking = value; } }

    protected bool _isImmune;
    public bool isImmune { get { return this._isImmune; } set { this._isImmune = value; } }

    protected float _immuneTime;
    protected float _forceJump;

    public abstract void onAttack(Collision other);
    public abstract void HealthUp(float healing);

    public void SetKey(float haveAKey)
    {
        _haveKey = System.Convert.ToBoolean(haveAKey);
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