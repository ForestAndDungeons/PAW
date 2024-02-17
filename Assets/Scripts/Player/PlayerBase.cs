using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacterBase
{
    PlayerBaseSO _data;
    AudioSource _audioSource;
    AudioClip[] _audioClip;
    Player _player;
    PlayerSoundManager _playerSoundManager;
    ParticleSystem _particleSystem;
    AnimationController _animationController;

    public PlayerBase(PlayerBaseSO playerBaseSO, string name, bool haveKey, Player player)
    {
        _data = playerBaseSO;
        _maxHealth = _data.maxHealth;
        _currentHealth = _maxHealth;
        _attackPower = _data.attackPower;
        _name = name;
        _haveKey = haveKey;
        _player = player;
    }

    public override void onAttack(Collision other){}

    public override void HealthUp(float add)
    {
        _currentHealth += add;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        _player._uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth, _armor);
    }

    public void ArmorUp(float add)
    {
        _armor += add;
        _player._uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth, _armor);
    }

    public void AttackSpeedUp(float add)
    {
        _player.AttackSpeedUp();
    }

    public void SetForceJump(float forceJump)
    {
        _forceJump = forceJump;
        _player._movement.SetForceJump(_forceJump);
    }

    public void SpeedUp(float add)
    {
        _player._movement.SetSpeedBonus(add);
    }
}