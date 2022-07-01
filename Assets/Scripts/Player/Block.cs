using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Collider _myCollider;

    private void OnTriggerEnter(Collider other)
    {
        _player._playerBase.SetIsBlocking(true);
    }
}