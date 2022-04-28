using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int _attackPower;

    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
            other.gameObject.GetComponent<Enemy>()._enemyBase.onDamage(_attackPower);
    }
}