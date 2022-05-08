using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBite : MonoBehaviour
{
    [SerializeField] int _bitePower;

    public void OnTriggerEnter(Collider player)
    {
        if (player != null)
            Debug.Log("Execute Bite damage");
        player.gameObject.GetComponent<Player>()._playerBase.onDamage(_bitePower);

    }
}