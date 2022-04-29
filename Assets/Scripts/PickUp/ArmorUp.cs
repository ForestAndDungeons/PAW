using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : PickUp
{
    [SerializeField] int armor;
    [SerializeField] AudioSource _aSource;
    [SerializeField] AudioClip[] _aClip;
    public override void Pick(PlayerBase playerBase)
    {
        playerBase.ArmorUp(armor);
        Destroy(gameObject);
    }

    public override void PickUpSound(PickUpSound pickUpSound)
    {
        pickUpSound.playOnCollision(_aClip[0]);
    }
}