using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : PickUp
{
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.AttackUp;
        OnPickUp(playerBase);
    }
}