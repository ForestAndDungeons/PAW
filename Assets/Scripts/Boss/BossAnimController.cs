using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimController 
{
    Animator _bossAnim;
    public BossAnimController(Animator bossAnim)
    {
        _bossAnim = bossAnim;
    }

    public void OnIdle(bool res)
    {
        _bossAnim.SetBool("IsIdle", res);
    }
    public void OnFireBall(bool res)
    {
        _bossAnim.SetBool("IsFireBall", res);
    }

    public void OnJump(bool res)
    {
        _bossAnim.SetBool("IsJump", res);
    }

    public void OnSpawnEnemy(bool res)
    {
        _bossAnim.SetBool("isSpawn", res);
    }

    public void OnHit(bool res)
    {
        _bossAnim.SetBool("IsHit", res);
    }

    public void OnDeath()
    {
        _bossAnim.SetBool("IsDeath", true);
    }
}
