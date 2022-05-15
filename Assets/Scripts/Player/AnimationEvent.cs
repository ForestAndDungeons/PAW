using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] ParticleSystem _particleWalk;

    public void ANIM_Event(string param)
    {
        if (param == "attackStart")
        {
            _player._playerSoundManager.playOnAttack();
            _player.weapon.activateCollider();

        }

        if (param == "attackEnd")
        {
            _player.weapon.deactivateCollider();
        }

        if (param == "attackAnimationEnd")
        {
            _player._animationController.onAttackEnd();
        }

        if (param == "isWalking")
        {
            _particleWalk.Play();
        }
    }
}
