using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmolateEnemyBase : CharacterBase,IDamage
{
    InmolateEnemy _inmolateEnemy;

    public InmolateEnemyBase(string name, float maxHealth, float attackPower, float armor, InmolateEnemy inmolateEnemy)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _inmolateEnemy = inmolateEnemy;
    }

    public void onDamage(float damage)
    {
            if (_currentHealth > 0)
            {
                _currentHealth -= damage - _armor;
            }
            else if (_currentHealth <= 0)
            {
                _inmolateEnemy.DestroInmolateEnemy();
            }
     }

    public override void onAttack(Collision other)
    {
        if (other != null)
            other.gameObject.GetComponent<Player>().onDamage(_attackPower);
    }

    public override void HealthUp(float healing)
    {
        //Sistema preparado para futuro feature
    }

    public void EnemyWithKey(bool checker)
    {
        _haveKey = checker;
    }
}
