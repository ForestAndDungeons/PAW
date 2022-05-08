using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PickUp
{
    [Header("Variables")]
    [SerializeField] float _healing;
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
        playerBase.HealthUp(_healing);
        onPickUp();
    }
}