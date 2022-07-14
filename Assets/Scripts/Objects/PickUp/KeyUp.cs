using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUp : PickUp
{
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.SetKey;
        OnPickUp(playerBase);
    }
}