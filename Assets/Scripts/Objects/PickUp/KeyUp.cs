using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUp : PickUp
{
    [Header("Variables")]
    [SerializeField] bool _key;

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
        _pickUpSound.playOnCollision(_audioSource, _audioClip[0]);
        playerBase.KeyUp(_key);
        OnPickUp();
    }
}
