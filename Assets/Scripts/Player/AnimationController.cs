using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    Animator _myAnimator;
    
    public AnimationController(Animator myAnimator)
    {
        _myAnimator = myAnimator;
    }

    public void onAttack()
    {
        _myAnimator.SetBool("onAttack", true);
    }

    public void onAttack2()
    {
        _myAnimator.SetBool("onAttack2", true);
    }

    public void onAttackEnd()
    {
        _myAnimator.SetBool("onAttack", false);
    }

    public void onAttack2End()
    {
        _myAnimator.SetBool("onAttack2", false);
    }

    public void onSpecial()
    {
        _myAnimator.SetBool("onSpecial", true);
    }

    public void onSpecialEnd()
    {
        _myAnimator.SetBool("onSpecial", false);
    }

    public void onBlockStart()
    {
        _myAnimator.SetBool("onBlock", true);
    }

    public void onBlockEnd()
    {
        _myAnimator.SetBool("onBlock", false);
    }

    public void onHit()
    {
        _myAnimator.SetTrigger("onHit");
    }

    public void onDeath()
    {
        _myAnimator.SetBool("onDeath", true);
    }

    public void InputUpdate(float verticalInput, float horizontalInput)
    {
        _myAnimator.SetFloat("Vertical", verticalInput);
        _myAnimator.SetFloat("Horizontal", horizontalInput);
    }
}