using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBase : CharacterBase,IDamage
{
    RatEnemy _ratEnemy;

    public RatBase(string name, float maxHealth, float attackPower, float armor, RatEnemy ratEnemy)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _ratEnemy = ratEnemy;
    }

    public void onDamage(float damage)
    {
            if (_currentHealth > 0)
            {
                _currentHealth -= damage - _armor;
            }
            else if (_currentHealth <= 0)
            {
                _ratEnemy.DestroRat();
            }
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

    public void EnemyWithKey(bool checker)
    {
        _haveAKey = checker;
    }
}
