using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntity : MonoBehaviour
{
    [Header("Door")]
    [SerializeField]DoorScript _doorScript;
    [SerializeField] DoorScript _doorScript1;
    [SerializeField] DoorScript _doorScript2;
    [SerializeField] DoorScript _doorScript3;

    [Header("Listas")]
    [SerializeField] List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] List<GameObject> _playerList = new List<GameObject>();

    [Header("Count")]
    public float EnemyCount;

    void Update()
    {

        if (_playerList.Count > 0)
        {
            _doorScript.CloseDoor();
            _doorScript1.CloseDoor();
            _doorScript2.CloseDoor();
            _doorScript3.CloseDoor();
            if (EnemyCount  >= _enemyList.Count)
            {
                _doorScript.OpenDoor();
                _doorScript1.OpenDoor();
                _doorScript2.OpenDoor();
                _doorScript3.OpenDoor();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Layer 8 is Player
        if (other.gameObject.layer == 8)
        {
            _playerList.Add(other.gameObject);
        }
        // Layer 9 is Enemy
        if (other.gameObject.layer == 9)
        {
            _enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            _playerList.Remove(other.gameObject);
        }
        
        if (other.gameObject.layer == 9)
        {
            _enemyList.Remove(other.gameObject);
        }
        
    }

    public void EnemySum()
    {
        EnemyCount++;
    }

}
