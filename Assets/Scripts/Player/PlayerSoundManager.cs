using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : SoundsManager
{
    public PlayerSoundManager(AudioSource audSourse, AudioClip[] audClip)
    {
        _aSource = audSourse;
        _aClip = audClip;
    }
    
    public override void playOnCollision(AudioSource aSource,AudioClip _aClipCol)
    {
        _aSource.PlayOneShot(_aClipCol);
    }


    public override void playOnAttack()
    {
        _aSource.PlayOneShot(_aClip[1]);
    }

    public override void playOnDead()
    {
        //_aSource.PlayOneShot(_aClip[3]);
    }
}
