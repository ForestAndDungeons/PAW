using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PickUp
{
    [SerializeField] int healing;
    [SerializeField] AudioSource _aSource;
    [SerializeField] AudioClip[]   _aClip;
    public override void Pick(PlayerBase playerBase)
    {
        playerBase.HealthUp(healing);
        Destroy(gameObject);
    }

    public override void PickUpSound(PickUpSound pickUpSound)
    {
        pickUpSound.playOnCollision(_aClip[0]);
    }
}