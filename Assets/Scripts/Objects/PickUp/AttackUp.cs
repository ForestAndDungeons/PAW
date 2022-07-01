using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : PickUp
{
    [Header("Variables")]
    [SerializeField] ItemUI _itemUI;
    [SerializeField] string _title;
    [SerializeField] string _description;
    [SerializeField] float _attack;
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    private void Start()
    {
        _pickUpSound = new PickUpSound(_audioSource, _audioClip);
        _itemUI = FindObjectOfType<ItemUI>();
    }
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpSound.playOnCollision(_audioSource, _audioClip[0]);
        _itemUI.ActiveInterface(_title, _description);
        playerBase.AttackUp(_attack);
        OnPickUp();
    }

}