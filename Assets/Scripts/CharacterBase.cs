using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;
    [SerializeField] int _attackPower;

    public CharacterBase(string name, int maxHealth, int currentHealth, int attackPower)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = currentHealth;
        _attackPower = attackPower;
    }

    public void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void onDamage()
    {
        _currentHealth -= _attackPower;

        if (_currentHealth <= 0)
            Destroy(this.gameObject);
    }

    public void onHeal(int health)
    {
        _currentHealth += health;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        onDamage();
    }
}
