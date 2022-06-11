using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapHit : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] float _timeBeforeHit;

    private void OnTriggerEnter(Collider other)
    {
        Damage(_damage, other);
    }

    private void OnTriggerStay(Collider other)
    {
        Damage(_damage, other);
    }

    void Damage(float damage, Collider other)
    {
        var player = other.GetComponent<Player>();
        var enemy = other.GetComponent<Enemy>();

        if (player != null)
            player._playerBase.onDamage(_damage);
        else if (enemy != null)
            enemy._enemyBase.onDamage(_damage);
    }
}
