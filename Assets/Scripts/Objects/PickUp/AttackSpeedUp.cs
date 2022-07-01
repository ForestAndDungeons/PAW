using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : PickUp
{
    public override void Pick(PlayerBase playerBase)
    {
        playerBase.AttackSpeedUp();
        OnPickUp();
    }
}