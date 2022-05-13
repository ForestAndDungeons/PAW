using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float _attackPower;
    Collider _enemyCollider;

    Enemy _enemy;

    private void Start()
    {
        _enemy = this.gameObject.GetComponentInParent<Enemy>();
        _enemyCollider = this.GetComponent<Collider>();
    }

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

    public void ActivateCollider()
    {
        _enemyCollider.enabled = true;
    }

    public void DeactivateCollider()
    {
        _enemyCollider.enabled = false;
    }
}
