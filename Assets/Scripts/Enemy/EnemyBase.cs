using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    public EnemyBase(string name, int maxHealth, int attackPower, int armor)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
    }

    public override void onDamage(int damage)
    {
        _currentHealth -= damage - _armor;

        if (_currentHealth <= 0)
            Destroy(this.gameObject);
    }

    public override void onAttack(Collision other)
    {
        if (other != null)
        other.gameObject.GetComponent<Player>()._playerBase.onDamage(_attackPower);
    }

    public override void HealthUp(int healing)
    {
        
    }
}
