using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] ParticleSystem _particleWalk;
    [SerializeField] ParticleSystem _particleSpecial;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    public void ANIM_Event(string param)
    {
        if (param == "attackStart")
        {
            _player._playerSoundManager.playOnAttack();
            _player.weapon.ActivateColliderAttack(true);
        }

        if (param == "attackEnd")
        {
            _player.weapon.ActivateColliderAttack(false);
        }

        if (param == "attackAnimationEnd")
        {
            _player._animationController.onAttackEnd();
            if (_player.combo <= 1)
            {
                _player.combo = 0;
            }
        }

        if (param == "attack2AnimationEnd")
        {
            _player._animationController.onAttack2End();
        }

        if (param == "attack2")
        {
            if (_player.combo > 1)
            {
                _player._animationController.onAttack2();
                _player.combo = 0;
            } 
        }

        if (param == "specialStart")
        {
            _player._playerSoundManager.playOnSpecial();
            _player.weapon.ActivateColliderSpecial(true);
            _particleSpecial.Play();
        }

        if (param == "specialEnd")
        {
            _player.weapon.ActivateColliderSpecial(false);
        }

        if (param == "specialAnimationEnd")
        {
            _player._animationController.onSpecialEnd();
        }

        if (param == "blockStart")
        {
            _player.weapon.ActivateColliderBlock(true);
            //_player._playerBase.SetIsBlocking(true);
            //_player._playerSoundManager.playOnSpecial();
            //_particleSpecial.Play();
        }

        if (param == "blockEnd")
        {
            if (!Input.GetKey(_player._sKeyCode[2].key))
            {
                _player.weapon.ActivateColliderBlock(false);
                _player._playerBase.SetIsBlocking(false);
                _player._animationController.onBlockEnd();
            }
        }

        if (param == "isWalking")
        {
            _particleWalk.Play();
        }

        if (param == "step")
        {
            //_audioSource.PlayOneShot(_audioClip[0]);
        }

        if (param == "stopMove")
        {
            //_player.CanMoveSetter(false);
            _player.SetEmptyControlDelegate();
            _player.SetEmptyMovementDelegate();
        }

        if (param == "startMove")
        {
            //_player.CanMoveSetter(true);
            _player.SetControlDelegate();
            _player.SetMovementDelegate();
        }
    }
}