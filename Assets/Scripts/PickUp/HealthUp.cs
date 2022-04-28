using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PickUp
{
    [SerializeField] int healing;

    public override void Pick(PlayerBase playerBase)
    {
        playerBase.HealthUp(healing);
        Destroy(gameObject);
    }
}