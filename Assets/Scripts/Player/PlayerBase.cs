using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    public PlayerBase(string name, int maxHealth, int attackPower, int armor)
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
            this.gameObject.SetActive(false);
    }

    public override void onAttack(Collision other)
    {

    }
}
