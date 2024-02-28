
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase, IDamage
{
    PlayerBaseSO _data;
    AudioSource _audioSource;
    AudioClip[] _audioClip;

    Player _player;
    PlayerSoundManager _playerSoundManager;
    ParticleSystem _particleSystem;
    AnimationController _animationController;

    protected float _keysCollected;
    public float keysCollected { get { return this._keysCollected; } set { this._keysCollected = value; } }

    public PlayerBase(PlayerBaseSO playerBaseSO, string name, float keysCollected, PlayerSoundManager playerSoundManager, AudioClip[] audioClip, AudioSource audioSource, ParticleSystem particleSystem, Player player, AnimationController animationController)
    {
        _data = playerBaseSO;
        _name = name;
        _maxHealth = _data.maxHealth;
        _currentHealth = _maxHealth;
        _attackPower = _data.attackPower;
        _keysCollected = keysCollected;
        _playerSoundManager = playerSoundManager;
        _audioClip = audioClip;
        _audioSource = audioSource;
        _particleSystem = particleSystem;
        _player = player;
        _animationController = animationController;
    }

    public void onDamage(float damage)
    {
        if (!_isImmune)
        {
            if(!_isBlocking)
            {
                if (_currentHealth > 0)
                {
                    if (_armor > 0)
                    {
                        _armor -= damage;

                        if (_armor < 0)
                        {
                            _currentHealth += _armor;
                            _armor = 0;
                        }
                    }
                    else
                    {
                        _currentHealth -= damage;
                    }
                }

                _animationController.onHit();
                _playerSoundManager.playOnCollision(_audioSource, _audioClip[0]);
                _particleSystem.Play();
                _player._uiPlayer.UIUpdate(_maxHealth, _currentHealth, _armor);
                isImmune = true;
                _playerSoundManager.playOnHit();
                _player.StartCoroutine(_player.TimeOfImmune());
            

                if (_currentHealth <= 0)
                {
                    _playerSoundManager.playOnDeath();
                    _animationController.onDeath();
                    _player.DisableThisObject();
                }
            }
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

        _player._uiPlayer.UIUpdate(_maxHealth, _currentHealth, _armor);
    }

    public void ArmorUp(float add)
    {
        _armor += add;
        _player._uiPlayer.UIUpdate(_maxHealth, _currentHealth, _armor);
    }

    public void AddKey(float add)
    {
        _keysCollected += add;
    }

    public void AttackSpeedUp(float add)
    {
        _player.AttackSpeedUp();
    }

    public void SetForceJump(float forceJump)
    {
        _forceJump = forceJump;
        _player._movement.forceJump = _forceJump;
    }

    public void SpeedUp(float add)
    {
        _player._movement.SetSpeedBonus(add);
    }
}