using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapHit : MonoBehaviour
{
    [SerializeField] float _damage;

    private void OnTriggerEnter(Collider other)
    {
        Damage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Damage(other);
    }

    void Damage(Collider other)
    {
        var entity = other.GetComponent<ICharacterBase>();

        if (entity != null)
            entity.onDamage(_damage);
    }
}