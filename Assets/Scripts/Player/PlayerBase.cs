using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    PlayerSoundManager _playerSoundMan;
    AudioSource _aSource;
    AudioClip[] _aClip;
    Player _player;
    public PlayerBase(string name, float maxHealth, float attackPower, float armor, PlayerSoundManager pSM, AudioClip[] aClip, AudioSource aSource)
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

    public override void onDamage(float damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage - _armor;
            _playerSoundMan.playOnCollision(_aSource,_aClip[0]);
        }
        else if (_currentHealth <= 0)
        {
            //_playerSoundsManager.playOnDeath();
            _player.DisableThisObject();
        }
    }

    //public override void onAttackCollision(Collision other){}
    //public override void onAttackTrigger(Collider other){}

    public override void onAttack(Collision other)
    {
        
    }

    public override void HealthUp(float add)
    {
        _currentHealth += add;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
    public void ArmorUp(float add)
    {
        _armor += add;
    }

    public void AttackUp(float add)
    {
        _attackPower += add;
    }
}
