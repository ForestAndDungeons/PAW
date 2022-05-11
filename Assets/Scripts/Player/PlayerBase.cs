
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    PlayerSoundManager _playerSoundMan;
    AudioSource _audioSource;
    AudioClip[] _audioClip;
    Player _player;
    ParticleSystem _particleSystem;

    public PlayerBase(string name, float maxHealth, float attackPower, float armor, PlayerSoundManager playerSoundManager, AudioClip[] audioClip, AudioSource audioSource, ParticleSystem particleSystem)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _playerSoundMan = playerSoundManager;
        _audioClip = audioClip;
        _audioSource = audioSource;
        _particleSystem = particleSystem;
    }

    public override void onDamage(float damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage - _armor;
            _playerSoundMan.playOnCollision(_audioSource,_audioClip[0]);
            _particleSystem.Play();

        }
        else if (_currentHealth <= 0)
        {
            //_playerSoundsManager.playOnDeath();
            _player.DisableThisObject();
        }
    }
    public override void onAttack(Collision other){}

    //public override void onAttackCollision(Collision other){}
    //public override void onAttackTrigger(Collider other){}

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
