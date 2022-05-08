using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float _attackPower;

    Player _player;

    private void Start()
    {
        _player = this.gameObject.GetComponentInParent<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        _attackPower = _player._playerBase.attackPowerGetter();

        if (other != null)
            other.gameObject.GetComponent<Enemy>()._enemyBase.onDamage(_attackPower);
        _player.SoundHit();
    }
}