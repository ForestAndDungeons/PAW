using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PickUp
{
    [Header("Variable Heal")]
    [SerializeField] int healing;
    [Header("Audio")]
    [SerializeField] AudioSource _aSource;
    [SerializeField] AudioClip[] _aClip;

    private void Start()
    {
        pickSoundManager = new PickUpSound();
    }

    public override void Pick(PlayerBase playerBase)
    {
        Debug.Log("pickeo pickup");
        pickSoundManager.playOnCollision(_aSource, _aClip[0]);
        playerBase.HealthUp(healing);
        Destroy(gameObject,1f);
    }

}