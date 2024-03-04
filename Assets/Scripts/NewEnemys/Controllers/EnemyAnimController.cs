using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController
{
    #region Parameters
    Animator _animator;
    public Animator myAnimator { get { return _animator; } }
    #endregion
    public EnemyAnimController(Animator animator)
    {
        _animator = animator;
    }


    public void SetIsAttacking()
    {
        _animator.SetTrigger("isAttack");

    }

    public void SetIsWalk(float value)
    {
        _animator.SetFloat("IsWalk", value);
    }

    public void SetIsHit()
    {
        _animator.SetTrigger("isHit");
    }

    public void SetIsDeath()
    {
        _animator.SetTrigger("isDeath");
    }
}
