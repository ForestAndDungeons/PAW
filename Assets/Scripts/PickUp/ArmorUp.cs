using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : PickUp
{
    [Header("Valor Armor")]
    [SerializeField] int armor;
    [Header("Audio")]
    [SerializeField] AudioSource _aSource;
    [SerializeField] AudioClip[] _aClip;

    private void Start()
    {
        pickSoundManager = new PickUpSound();
    }
    public override void Pick(PlayerBase playerBase)
    {
        pickSoundManager.playOnCollision(_aSource, _aClip[0]);
        playerBase.ArmorUp(armor);
        Destroy(gameObject, 1f);
    }

}