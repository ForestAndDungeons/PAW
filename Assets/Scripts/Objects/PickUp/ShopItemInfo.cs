using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemInfo : MonoBehaviour
{
    PickUp _pickUp;

    private void Start()
    {
        _pickUp = GetComponentInParent<PickUp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_pickUp.isPurchasable)
        {
            var player = other.gameObject.GetComponent<Player>();
            if(player != null && _pickUp.isPurchasable)
                player._infoUI.text = _pickUp.title + "Price: " + System.Convert.ToString(_pickUp.shopPrice);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if(player != null)
            player._infoUI.text = null;
    }
}
