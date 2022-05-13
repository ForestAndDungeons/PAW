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

    public void OnAttack()
    {
        _enemyAnim.SetBool("IsAttack", true);
    }

    public void OnIdle()
    {
        _enemyAnim.SetFloat("IsWalk", 0);
    }

    public void OnWalking()
    {
        _enemyAnim.SetFloat("IsWalk", 1);
    }
    public void OnAttackEnd()
    {
        _enemyAnim.SetBool("IsAttack", false);
    }

    public void OnHit()
    {
        _enemyAnim.SetBool("IsHit", true);
    }
    public void OnHitEnd()
    {
        _enemyAnim.SetBool("IsHit", false);
    }

    public void OnDeath()
    {
        _enemyAnim.SetBool("IsDeath", true);
    }

}