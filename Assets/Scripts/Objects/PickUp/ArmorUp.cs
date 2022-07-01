using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : PickUp
{
    [Header("Variables")]
    [SerializeField] float _armor;
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    private void Start()
    {
        _pickUpSound = new PickUpSound(_audioSource, _audioClip);

    }
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpSound.playOnCollision(_audioSource, _audioClip[0]);
        playerBase.ArmorUp(_armor);
        OnPickUp();
    }
}