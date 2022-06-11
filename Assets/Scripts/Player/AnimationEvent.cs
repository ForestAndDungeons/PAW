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
            _player.weapon.activateColliderAttack();
        }

        if (param == "attackEnd")
        {
            _player.weapon.deactivateColliderAttack();
        }

        if (param == "attackAnimationEnd")
        {
            _player._animationController.onAttackEnd();
        }

        if (param == "specialStart")
        {
            _player._playerSoundManager.playOnSpecial();
            _player.weapon.activateColliderSpecial();
            _particleSpecial.Play();
        }

        if (param == "specialEnd")
        {
            _player.weapon.deactivateColliderSpecial();
        }

        if (param == "specialAnimationEnd")
        {
            _player._animationController.onSpecialEnd();
        }

        if (param == "blockStart")
        {
            //_player._playerSoundManager.playOnSpecial();
            _player.weapon.activateColliderBlock();
            //_particleSpecial.Play();
        }

        if (param == "blockEnd")
        {
            _player.weapon.deactivateColliderBlock();
            _player._animationController.onBlockEnd();
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
            _player.CanMoveSetter(false);
        }

        if (param == "startMove")
        {
            _player.CanMoveSetter(true);
        }
    }
}
