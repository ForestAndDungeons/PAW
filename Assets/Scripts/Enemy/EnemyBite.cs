using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBite : MonoBehaviour
{
    float _attackPower;

    public void OnTriggerEnter(Collider player)
    {
        _attackPower = this.gameObject.GetComponentInParent<Enemy>()._enemyBase.attackPowerGetter();

        if (player != null)
        {
            Debug.Log("Execute Bite damage");
            var _player = player.gameObject.GetComponent<Player>();
            if (_player != null)
            {
                _player._playerBase.onDamage(_attackPower);
            }
        }
    }
}