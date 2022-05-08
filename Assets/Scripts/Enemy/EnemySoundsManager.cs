using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundsManager : SoundsManager
{
    public EnemySoundsManager(AudioSource audioSourse, AudioClip[] audioClip)
    {
        _audioSource = audioSourse;
        _audioClip = audioClip;
    }

    public override void playOnCollision(AudioSource audioSource, AudioClip audioClipCol)
    {
        //_aSource.PlayOneShot(_aClip[0]);
    }

    public override void playOnAttack()
    {
        _audioSource.PlayOneShot(_audioClip[1]);
    }

    public override void playOnDeath()
    {
        _audioSource.PlayOneShot(_audioClip[0]);
    }
}