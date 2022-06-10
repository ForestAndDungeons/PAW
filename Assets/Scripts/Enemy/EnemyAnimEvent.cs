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
            Debug.Log("Entro al Anim_Event_Attack, " + attack);
            _enemy.enemySoundsManager.playOnAttack();
            _enemy.enemyAttack.ActivateCollider();
        }
        if (attack == "FinishAttack")
        {
            _enemy.enemyAttack.DeactivateCollider();
        }
    }

    public void Anim_event_InstantiateKey()
    {
        if (_enemy.HasAKey)
        {
            if (_enemy._keyPrefab !=null)
            {
                GameObject key = Instantiate(_enemy._keyPrefab, (transform.position + new Vector3(0, 0.5f, 0)), _enemy._keyPrefab.transform.rotation);
            }
        }
    }


}
