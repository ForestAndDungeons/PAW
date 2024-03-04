using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
   [SerializeField] MeleEnemy _meleEnemy;

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<IDamage>();
        if (entity != null)
        {
            entity.onDamage(_meleEnemy.enemimeBase.attackPower);
        }
    }
}
