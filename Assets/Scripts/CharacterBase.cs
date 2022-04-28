using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    protected string _name;
    protected int _maxHealth;
    protected int _currentHealth;
    protected int _attackPower;
    protected int _armor;

    public int currentHealthGetter()
    {
        return _currentHealth;
    }

    public abstract void onDamage(int damage);

    public abstract void onAttack(Collision other);

    public void onHeal(int health)
    {
        _currentHealth += health;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
