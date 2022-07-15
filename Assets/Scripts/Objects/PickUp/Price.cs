using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Price : MonoBehaviour
{
    public PickUp _pickUp;
    public List<Player> _players = new List<Player>();

    private void Start()
    {
        _pickUp = GetComponentInParent<PickUp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if(_pickUp.GetIsPurchasable())
            player._infoUI.text = /*_pickUp.GetTitle() +*/ System.Convert.ToString(_pickUp.GetPrice()) + " Coins";

        //Assign automatic players
        /*if (_pickUp._isPurchasable)
        {
            if(_players.Count == 0)
                _players.Add(other.gameObject.GetComponent<Player>());
            
            foreach (Player p in _players)
            {
                p._infoUI.text = "Price: " + System.Convert.ToString(_pickUp._price);

                if (p != other.gameObject.GetComponent<Player>())
                    _players.Add(other.gameObject.GetComponent<Player>());
            }
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        player._infoUI.text = null;
    }
}
