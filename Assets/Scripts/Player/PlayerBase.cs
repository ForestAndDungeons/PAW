using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    PlayerSoundManager _playerSoundMan;
    AudioSource _aSource;
    AudioClip[] _aClip;
    public PlayerBase(string name, int maxHealth, int attackPower, int armor, PlayerSoundManager pSM, AudioClip[] aClip, AudioSource aSource)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _playerSoundMan = pSM;
        _aClip = aClip;
        _aSource = aSource;
    }

    public override void onDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage - _armor;
            _playerSoundMan.playOnCollision(_aSource,_aClip[0]);
        }
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
