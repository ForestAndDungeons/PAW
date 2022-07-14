using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : PickUp
{
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.AttackSpeedUp;
        OnPickUp(playerBase);
    }
}