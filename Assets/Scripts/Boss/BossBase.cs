using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterBase, IDamage
{
    EnemySoundsManager _enemySoundsManager;
    Boss _boss;
    AudioSource _audioSource;
    AudioClip[] _audioClip;
    ParticleSystem _particleSystem;
    bool _isInvulerable;
    BossAnimController _bossAnimController;
    BossState _bossState;

    public BossBase(string name, float maxHealth, float attackPower, float armor, EnemySoundsManager enemySoundsManager, Boss boss, AudioSource audioSource, AudioClip[] audioClip, ParticleSystem particleSystem, BossAnimController bossAnimController, BossState bossState)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackPower = attackPower;
        _armor = armor;
        _enemySoundsManager = enemySoundsManager;
        _boss = boss;
        _audioSource = audioSource;
        _audioClip = audioClip;
        _particleSystem = particleSystem;
        _bossAnimController = bossAnimController;
        _bossState = bossState;
    }

    public void onDamage(float damage)
    {
        if (_currentHealth > 0)
        {     
            _currentHealth -= damage - _armor;
            //_bossAnimController.OnHit(true);
            //_enemySoundsManager.playOnCollision(_audioSource, _audioClip[0]);
            //_particleSystem.Play();
        }
        else if (_currentHealth <= 0)
        {
            _bossState.isDead = true;
            //_enemySoundsManager.playOnDeath();
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
