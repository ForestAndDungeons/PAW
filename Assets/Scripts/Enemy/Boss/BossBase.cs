using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterBase, IDamage
{
    Boss _boss;
    AudioSource _audioSource;
    AudioClip[] _audioClip;
    ParticleSystem _particleSystem;
    bool _isInvulerable;
    BossAnimController _bossAnimController;
    BossState _bossState;

    public BossBase(string name, float maxHealth, float attackPower, float armor, Boss boss, AudioSource audioSource, AudioClip[] audioClip, ParticleSystem particleSystem, BossAnimController bossAnimController, BossState bossState)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _attackDamage = attackPower;
        _armor = armor;
        _boss = boss;
        _audioSource = audioSource;
        _audioClip = audioClip;
        _particleSystem = particleSystem;
        _bossAnimController = bossAnimController;
        _bossState = bossState;
    }

    public void onDamage(float damage)
    {
        _bossAnimController.OnFireBall(false);
        _bossAnimController.OnJump(false);
        _bossAnimController.OnSpawnEnemy(false);
        if (_currentHealth > 0)
        {     
            _currentHealth -= damage - _armor;
            _bossAnimController.OnHit(true);
            _particleSystem.Play();
        }
        else if (_currentHealth <= 0)
        {
            _bossState.isDead = true;
            _audioSource.PlayOneShot(_audioClip[5]);
        }
    }

    public override void onAttack(Collision other)
    {
        if (other != null)
            other.gameObject.GetComponent<Player>().onDamage(_attackDamage);
    }

    public override void HealthUp(float healing)
    {
        //Sistema preparado para futuro feature
    }

}
