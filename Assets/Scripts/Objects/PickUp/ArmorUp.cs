using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUp : PickUp
{
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.ArmorUp;
        OnPickUp(playerBase);
    }
}