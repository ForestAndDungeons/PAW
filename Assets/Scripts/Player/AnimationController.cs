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

    public void InputUpdate(float verticalInput, float horizontalInput)
    {
        _myAnimator.SetFloat("Vertical", verticalInput);
        _myAnimator.SetFloat("Horizontal", horizontalInput);
    }
}
