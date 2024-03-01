
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    PlayerBaseSO _data;
    Player _player;

    //
    AudioSource _audioSource;
    AudioClip[] _audioClip;
    PlayerSoundManager _playerSoundManager;
    ParticleSystem _particleSystem;
    AnimationController _animationController;

    protected float _keysCollected;
    public float keysCollected
    {
        get { return this._keysCollected; }
        set { this._keysCollected = value; }
    }

    public PlayerBase(PlayerBaseSO playerBaseSO, string name, float keysCollected, PlayerSoundManager playerSoundManager, AudioClip[] audioClip, AudioSource audioSource, ParticleSystem particleSystem, Player player, AnimationController animationController)
    {
        _data = playerBaseSO;
        _maxHealth = _data.maxHealth;
        _currentHealth = _maxHealth;
        _attackDamage = _data.attackDamage;
        _attackSpeed = _data.attackSpeed;
        _movementSpeed = _data.movementSpeed;
        _immuneTime = _data.immuneTime;

        _player = player;
        _name = name;
        _keysCollected = keysCollected;
        _playerSoundManager = playerSoundManager;
        _audioClip = audioClip;
        _audioSource = audioSource;
        _particleSystem = particleSystem;
        _animationController = animationController;
    }

    /*public void onDamage(float damage)
    {
        //
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
    }*/

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

    public void AddAttackSpeed(float add)
    {
        _player.myAnimator.SetFloat("attackSpeed", _attackSpeed + add);
    }

    public void SetForceJump(float forceJump)
    {
        _jumpForce = forceJump;
        _player._movement.jumpForce = _jumpForce;
    }

    public void SpeedUp(float add)
    {
        _player._movement.AddSpeed(add);
    }
}