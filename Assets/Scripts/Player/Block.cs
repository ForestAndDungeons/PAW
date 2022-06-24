using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] PlayerBase _playerBase;
    [SerializeField] Collider _myCollider;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ASASASASASA");
        _playerBase.SetIsBlocking(true);
    }
}