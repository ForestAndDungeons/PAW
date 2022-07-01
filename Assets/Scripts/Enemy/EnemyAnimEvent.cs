using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    [SerializeField] Enemy _enemy;

    public void Anim_Event_Attack(string attack)
    {
        if (_enemy.enemyAttack != null)
        {
            if (attack == "Attack")
            {
                _enemy.enemySoundsManager.playOnAttack();
                _enemy.enemyAttack.ActivateCollider();
            }
            if (attack == "FinishAttack")
            {
                _enemy.enemyAttack.DeactivateCollider();
            }
        }
    }

    public void Anim_event_EnemyDeath()
    {
        _enemy.DestroyThisObject();
    }
    public void Anim_event_InstantiateKey()
    {
        if (_enemy != null)
        {
            if (_enemy.GetterHaveAKey())
            {
                if (_enemy.keyPrefab !=null)
                {
                    Instantiate(_enemy.keyPrefab, (transform.position + new Vector3(0, 0.7f, 0)), _enemy.keyPrefab.transform.rotation);
                }
            }
            else
            {
                var drop = PorcentajeDrop();
                if (drop!= null)
                {
                    Instantiate(drop, (transform.position + new Vector3(0, 0.7f, 0)), drop.transform.rotation);
                }

            }
        }

    }

    public GameObject PorcentajeDrop()
    {
        float randomPorcentaje = Random.Range(0,101);
        if (randomPorcentaje >=0 && randomPorcentaje <=15) //15%
        {
            Debug.Log("si es 0 o menor a 10 entra en el 15% y dropea potion. Salio : " + randomPorcentaje);
            return _enemy.GetDropeables()[0];
        }
        else if (randomPorcentaje >15 && randomPorcentaje <= 35 ) //%20
        {
            Debug.Log("si es mayor a 10 o menor o igual a 50 entra en el 20% y dropea escudo. Salio : " + randomPorcentaje);
            return _enemy.GetDropeables()[1];
        }
        else  //60%
        {
            Debug.Log("si es mayor a 51 entra en el 40% que no dropea nada. Salio : " +randomPorcentaje);
            return null;
        }
        
    }

}
