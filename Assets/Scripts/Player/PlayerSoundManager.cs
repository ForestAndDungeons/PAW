using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : SoundsManager
{
    public PlayerSoundManager(AudioSource audioSourse, AudioClip[] audioClip)
    {
        _audioSource = audioSourse;
        _audioClip = audioClip;
    }
    
    public override void playOnCollision(AudioSource audioSource, AudioClip _audioClipCol)
    {
        _audioSource.PlayOneShot(_audioClipCol);
    }

    public override void playOnAttack()
    {
        _audioSource.PlayOneShot(_audioClip[1]);
    }

    public override void playOnHit()
    {
        _audioSource.PlayOneShot(_audioClip[2]);
    }

    public override void playOnDeath()
    {
        _audioSource.PlayOneShot(_audioClip[3]);
    }
}