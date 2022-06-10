using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntity : MonoBehaviour
{
    [Header("EnemyWithKey")]
    [SerializeField] int _randomEnemy;

    [Header("Doors")]
    [SerializeField] List<DoorScript> _doorList;


    [Header("Listas")]
    [SerializeField] public List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] public List<GameObject> _playerList = new List<GameObject>();

   /* public delegate void EventRoom();
    public event EventRoom eventRoom;*/
    private void Start()
    {
        StartCoroutine(WaitForFillList());
    }
    void Update()
    {
       /* if (eventRoom != null)
        {
            eventRoom();
        }*/
         if (_playerList.Count > 0)
        {
            for (int i = 0; i < _doorList.Count; i++)
            {
                _doorList[i].CloseDoor();
            }
            
            if (_enemyList.Count == 0)
            {
                for (int i = 0; i < _doorList.Count; i++)
                {
                    if (!_doorList[i].isSecretDoor)
                    {
                        _doorList[i].OpenDoor();
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Player pj = other.gameObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (pj)
        {
            StartCoroutine(WaitForAddPlayer(other.gameObject));
        }

        if (enemy)
        {
            _enemyList.Add(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player pj = other.gameObject.GetComponent<Player>();
        if (pj)
        {
            _playerList.Remove(other.gameObject);
        }   
        
    }

    public void ElimEnemyInList(GameObject enemy)
    {
        Debug.Log("Se elimino de la lista el enemigo: " + enemy);
        _enemyList.Remove(enemy);
    }

    public void EnemyWithKey(int enemyRandom)
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            int index = _enemyList.IndexOf(_enemyList[i]);
            if (enemyRandom == index)
            {
                Debug.Log("El enemigo: "+_enemyList[i].name+" es el que tiene la llave");
                Enemy enemyWithKey = _enemyList[i].GetComponent<Enemy>();
                enemyWithKey._enemyBase.EnemyWithKey(true);
                
            }
        }
    }

    IEnumerator WaitForFillList()
    {
        yield return new WaitForSeconds(.5f);
        _randomEnemy = Random.Range(0, _enemyList.Count);
        EnemyWithKey(_randomEnemy);
    }

    IEnumerator WaitForAddPlayer(GameObject playerGO)
    {
        yield return new WaitForSeconds(1f);
        //playerGO es Player GameObject
        _playerList.Add(playerGO);
    }
}
