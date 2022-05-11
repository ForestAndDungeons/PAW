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

    public abstract void onDamage(float damage);

    public abstract void onAttack(Collision other);

    public abstract void HealthUp(float healing);
}
