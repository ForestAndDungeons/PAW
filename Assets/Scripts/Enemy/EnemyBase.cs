using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    EnemySoundsManager _enemySoundsManager;
    Enemy _enemy;
    AudioSource _audioSource;
    AudioClip[] _audioClip;
    ParticleSystem _particleSystem;

    public EnemyBase(string name, float maxHealth, float attackPower, float armor, EnemySoundsManager enemySoundsManager, Enemy enemy, AudioSource audioSource, AudioClip[] audioClip, ParticleSystem particleSystem)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _enemySoundsManager = enemySoundsManager;
        _enemy = enemy;
        _audioSource = audioSource;
        _audioClip = audioClip;
        _particleSystem = particleSystem;
    }

    public override void onDamage(float damage)
    {
        if (_currentHealth > 0) {
            _currentHealth -= damage - _armor;
            _enemySoundsManager.playOnCollision(_audioSource,_audioClip[0]);
            _particleSystem.Play();
        }
        else if (_currentHealth <= 0)
        {
            _enemySoundsManager.playOnDeath();
            _enemy.DestroyThisObject();
        }
    }

    public override void onAttack(Collision other)
    {
        if (other != null)
            other.gameObject.GetComponent<Player>()._playerBase.onDamage(_attackPower);
    }

    public override void HealthUp(float healing)
    {
        //Sistema preparado para futuro feature
    }
}