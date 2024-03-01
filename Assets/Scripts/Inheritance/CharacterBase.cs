using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase
{
    protected string _name;

    protected float _maxHealth;
    public float maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    protected float _currentHealth;
    public float currentHealth
    { 
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    protected bool _isDead;

    protected float _attackDamage;
    public float attackPower
    { 
        get { return _attackDamage; } 
        set { _attackDamage = value; }
    }

    protected float _attackSpeed;
    public float attackSpeed
    { 
        get { return attackSpeed; }
        set { attackSpeed = value; } 
    }

    protected float _movementSpeed;
    public float movementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    protected float _armor;
    public float armor
    {
        get { return _armor; }
        set { _armor = value; }
    }

    protected float _coins;
    public float coins
    {
        get { return _coins; }
        set { _coins = value; }
    }

    protected bool _isBlocking;
    public bool isBlocking
    {
        get { return _isBlocking; }
        set { _isBlocking = value; }
    }

    protected bool _isImmune;
    public bool isImmune
    {
        get { return _isImmune; }
        set { _isImmune = value; }
    }

    protected float _immuneTime;
    float _lastTimeHitted;

    protected float _jumpForce;
    public float jumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    public abstract void onAttack(Collision other);
    public abstract void HealthUp(float healing);

    /*public void SetKey(float haveAKey)
    {
        _keysCollected = System.Convert.ToBoolean(haveAKey);
    }*/

    public void AddAttackDamage(float add)
    {
        _attackDamage += add;
    }

    public void AddCoins(float add)
    {
        _coins += add;
    }

    public virtual void onDamage(float damage)
    {
        if (Time.realtimeSinceStartup - _lastTimeHitted >= _immuneTime)
        {
            //Debug.Log("time Hitted" + Time.realtimeSinceStartup);
            //Debug.Log("Last time hitted" + _lastTimeHitted);
            _lastTimeHitted = Time.realtimeSinceStartup;
            _isImmune = false;
        }

        if (!_isImmune)
        {
            if (!_isBlocking)
            {
                if (_currentHealth > 0)
                {
                    if (_armor > 0)
                    {
                        _armor -= damage;

                        if (_armor < 0)
                        {
                            _currentHealth += _armor;
                            _armor = 0;
                        }
                    }
                    else
                    {
                        _currentHealth -= damage;
                    }
                    
                }
                else
                {
                    _isDead = true;
                }
            }
            isImmune = true;
        }
    }

    /*public void AddAttackSpeed(float add, Animator animator)
    {
        animator.SetFloat("attackSpeed", _baseAttackSpeed + add);
    }*/
}