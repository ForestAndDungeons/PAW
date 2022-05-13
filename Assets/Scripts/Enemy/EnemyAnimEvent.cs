using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    [SerializeField] Enemy _enemy;

    public void Anim_Event_Attack(string attack)
    {
        
        if (attack == "Attack")
        {
            Debug.Log("Enstro al Anim_Event_Attack, " + attack);
            _enemy.enemySoundsManager.playOnAttack();
            _enemy.enemyAttack.ActivateCollider();
        }
        if (attack == "FinishAttack")
        {
            _enemy.enemyAttack.DeactivateCollider();
        }
    }



}
