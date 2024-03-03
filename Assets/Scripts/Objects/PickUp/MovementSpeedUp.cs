using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedUp : PickUp
{

    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.SpeedUp;
        OnPickUp(playerBase);
    }
}