using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController
{
    Animator _enemyAnim;
    public EnemyAnimatorController(Animator enemyAnim)
    {
        _enemyAnim = enemyAnim;
    }

    public void OnAttack(bool res)
    {
        _enemyAnim.SetBool("IsAttack", res);
    }

    public void OnMovement(float res)
    {
        _enemyAnim.SetFloat("IsWalk", res);
    }

    public void OnHit(bool res)
    {
        _enemyAnim.SetBool("IsHit", res);
    }

    public void OnDeath()
    {
        _enemyAnim.SetBool("IsDeath", true);
    }

}