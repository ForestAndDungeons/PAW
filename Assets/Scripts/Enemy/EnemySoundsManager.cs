using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundsManager : SoundsManager
{
    public EnemySoundsManager(AudioSource audSourse, AudioClip[] audClip)
    {
        _aSource = audSourse;
        _aClip = audClip;
    }

    public override void playOnCollision(AudioSource aSource,AudioClip aClipCol)
    {
       //_aSource.PlayOneShot(_aClip[0]);
    }


    public override void playOnAttack()
    {
        //_aSource.PlayOneShot(_aClip[1]);
    }

    public override void playOnDead()
    {
        _aSource.PlayOneShot(_aClip[0]);
    }


}
