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
    public override void playOnCollision()
    {

    }

    public override void playOnAttack()
    {
        //Ejecuto sonido Ataque
    }




}
