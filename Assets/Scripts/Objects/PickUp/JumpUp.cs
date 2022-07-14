using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUp : PickUp
{
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.SetForceJump;
        OnPickUp(playerBase);
    }
}