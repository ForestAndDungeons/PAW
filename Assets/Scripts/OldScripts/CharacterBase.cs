using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    string _name;
    int _maxHealth;
    int _currentHealth;

    public CharacterBase(string n, int max, int current)
    {
        _name = n;
        _maxHealth = max;
        _currentHealth = current;
    }
}
