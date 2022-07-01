using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : PickUp
{
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;
    PickUpSound _pickUpSound;

    private void Start()
    {
        _pickUpSound = new PickUpSound(_audioSource, _audioClip);
    }

    public override void Pick(PlayerBase playerBase)
    {
        playerBase.AttackSpeedUp();
        OnPickUp();
    }
}