using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3 (Random.Range(-2f, 2f), Random.Range(4f, 6f), Random.Range(-2f, 2f)), ForceMode.Impulse);
    }

    public override void Pick(PlayerBase playerBase)
    {
        _pickUpDelegate = playerBase.AddCoins;
        OnPickUp(playerBase);
    }
}