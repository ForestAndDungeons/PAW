using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundsManager
{
    protected AudioSource _audioSource;
    protected AudioClip[] _audioClip;

    public virtual void playOnCollision(AudioSource _audioSource, AudioClip _audioClip){}

    public virtual void playOnAttack(){}

    public virtual void playOnHit(){}

    public virtual void playOnDeath(){}
}