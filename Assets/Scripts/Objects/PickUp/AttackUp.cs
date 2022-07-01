using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : PickUp
{
    [Header("Variables")]
    [SerializeField] float _attack;

    public override void Pick(PlayerBase playerBase)
    {
        playerBase.AttackUp(_attack);
        OnPickUp();
    }
}