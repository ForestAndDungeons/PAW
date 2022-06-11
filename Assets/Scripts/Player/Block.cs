using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] PlayerBase _playerBase;

    private void OnTriggerEnter(Collider other)
    {
        _playerBase.IsBlockingSetter(true);
    }
}
