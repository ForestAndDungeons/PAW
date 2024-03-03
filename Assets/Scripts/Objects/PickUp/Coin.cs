using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    
    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.AddCoins;
        OnPickUp(playerBase);
    }
}