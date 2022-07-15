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
        Debug.Log("AnimIdle");
        //_bossAnim.SetBool("IsIdle", res);
    }
    public void OnFireBall(bool res)
    {
        Debug.Log("AnimFireBall");
        //_bossAnim.SetBool("IsFireBall", res);
    }

    public void OnJump(bool res)
    {
        Debug.Log("AnimJump");
        //_bossAnim.SetBool("IsJump", res);
    }

    public void OnSpawnEnemy(bool res)
    {
        Debug.Log("AnimSpawnEnemy");
        //_bossAnim.SetBool("isSpawn", res);
    }

    public void OnHit(bool res)
    {
        Debug.Log("AnimHit");
        //_bossAnim.SetBool("IsHit", res);
    }

    public void OnDeath()
    {
        Debug.Log("AnimDeath");
        //_bossAnim.SetBool("IsDeath", true);
    }
}
