using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController
{
    Animator _enemyAnim;
    public EnemyAnimatorController(Animator enemyAnim){
        _enemyAnim = enemyAnim;
    }

    public void OnEnemyBite()
    {
        _enemyAnim.SetBool("IsBite", true);
    }
}
