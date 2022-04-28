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
        if (_currentHealth > 0)
            _currentHealth -= damage - _armor;
        else if (_currentHealth <= 0)
            this.gameObject.SetActive(false);
        //No funciona el else, why?
    }

    public override void onAttack(Collision other)
    {

    }

    public override void HealthUp(int add)
    {
        _currentHealth += add;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
    public void ArmorUp(int add)
    {
        _armor += add;
    }
}
