using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundsManager
{
     protected AudioSource _aSource;
     protected AudioClip[] _aClip;
    public abstract void playOnCollision();

    public virtual void playOnDeath() { 
    
    }

    public virtual void playOnAttack()
    {

    }


}
