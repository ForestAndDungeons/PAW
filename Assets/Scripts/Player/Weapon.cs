using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float _attackPower;
    Collider _myCollider;

    Player _player;

    private void Start()
    {
        _player = this.gameObject.GetComponentInParent<Player>();
        _myCollider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        _attackPower = _player._playerBase.attackPowerGetter();

        if (other != null)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy._enemyBase.onDamage(_attackPower);
            }

            _player.SoundHit();
        }
    }

    public void activateCollider()
    {
        _myCollider.enabled = true;
    }

    public void deactivateCollider()
    {
        _myCollider.enabled = false;
    }
}