using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Collider _myCollider;
    [SerializeField] float _timeOfBlocking;

    private void OnTriggerEnter(Collider other)
    {
        _player._playerBase.SetIsBlocking(true);

    }
    public IEnumerator TimeOfBlocking()
    {
        yield return new WaitForSeconds(_timeOfBlocking);
        _player._playerBase.SetIsBlocking(true);
    }
}