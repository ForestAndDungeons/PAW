using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : PickUp
{
    [SerializeField] int armor;

    public override void Pick(PlayerBase playerBase)
    {
        playerBase.ArmorUp(armor);
        Destroy(gameObject);
    }
}