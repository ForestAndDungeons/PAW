using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundsManager
{
     protected AudioSource _aSource;
     protected AudioClip[] _aClip;

    public abstract void playOnCollision(AudioSource _aSource,AudioClip _aClip);

    public virtual void playOnDeath() { 
    
    }

    public virtual void playOnAttack()
    {

    }

    public virtual void playOnDead()
    {

    }


}
