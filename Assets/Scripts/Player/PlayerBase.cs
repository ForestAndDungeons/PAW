
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    AudioSource _audioSource;
    AudioClip[] _audioClip;

    Player _player;
    PlayerSoundManager _playerSoundManager;
    ParticleSystem _particleSystem;
    AnimationController _animationController;

    public PlayerBase(string name, float maxHealth, float attackPower, float armor, PlayerSoundManager playerSoundManager, AudioClip[] audioClip, AudioSource audioSource, ParticleSystem particleSystem, Player player, AnimationController animationController)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _playerSoundManager = playerSoundManager;
        _audioClip = audioClip;
        _audioSource = audioSource;
        _particleSystem = particleSystem;
        _player = player;
        _animationController = animationController;
    }

    public override void onDamage(float damage)
    {
        if (_currentHealth > 0)
        {   
            if (_armor > 0)
            {
                _armor -= damage;

                if(_armor < 0)
                {
                    _currentHealth += _armor;
                    _armor = 0;
                }
            }
            else
            {
                _currentHealth -= damage;
            }

            _animationController.onHit();
            _playerSoundManager.playOnCollision(_audioSource,_audioClip[0]);
            _particleSystem.Play();
            _player._uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth);
        }

        if (_currentHealth <= 0)
        {
            _playerSoundManager.playOnDeath();
            _animationController.onDeath();
            _player.DisableThisObject();
        }
    }

    public override void onAttack(Collision other){}

    public override void HealthUp(float add)
    {
        _currentHealth += add;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        _player._uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth);
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