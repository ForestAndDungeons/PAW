using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapHit : MonoBehaviour
{
    [SerializeField] float _damage;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        var enemy = other.GetComponent<Enemy>();

        if (player != null)
            player._playerBase.onDamage(_damage);
        else if (enemy != null)
            enemy._enemyBase.onDamage(_damage);
    }
}
