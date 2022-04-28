using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack
{
    Collider _attackCollider;
    CharacterBase _chrBase;

    public  EnemyAttack(Collider _attkCollider)
    {
        _attackCollider = _attkCollider;
    }
    
    public void AttackPlayer()
    {
        //_chrBase.onDamage();
    }

}
