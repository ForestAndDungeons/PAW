using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : PickUp
{
    [Header("Variables")]
    [SerializeField] float _armor;

    public override void Pick(PlayerBase playerBase)
    {
        playerBase.ArmorUp(_armor);
        OnPickUp();
    }
}