using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    public EnemyBase(string name, float maxHealth, float attackPower, float armor)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
    }

    public override void onDamage(float damage)
    {
        if (_currentHealth > 0)
            _currentHealth -= damage - _armor;
    }

    public override void onAttack(Collision other)
    {
        if (other != null)
            other.gameObject.GetComponent<Player>()._playerBase.onDamage(_attackPower);
    }

    public override void HealthUp(float healing)
    {
        //Sistema preparado para futuro feature
    }
}