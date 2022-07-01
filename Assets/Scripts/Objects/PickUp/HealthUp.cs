using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PickUp
{
    [Header("Variables")]
    [SerializeField] float _healing;

    public override void Pick(PlayerBase playerBase)
    {
        playerBase.HealthUp(_healing);
        OnPickUp();
    }
}