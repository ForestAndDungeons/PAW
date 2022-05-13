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
        _audioSource.PlayOneShot(_audioClip[0]);
    }

    //Viento del arma
    public override void playOnAttack()
    {
        _audioSource.PlayOneShot(_audioClip[1]);
    }

    public override void playOnHit() {
    //Cuando acertar el golpe
        _audioSource.PlayOneShot(_audioClip[2]);
    }

    // cuando moris
    public override void playOnDeath()
    {
        _audioSource.PlayOneShot(_audioClip[3]);
    }





}