using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : SoundsManager
{
    public override void playOnCollision(AudioSource aSource,AudioClip _aClip)
    {
        Debug.Log("Ejecuto sonido PickUp");
        aSource.PlayOneShot(_aClip);
    }
}
