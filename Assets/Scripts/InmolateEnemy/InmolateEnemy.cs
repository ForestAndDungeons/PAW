using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmolateEnemy : MonoBehaviour, IDamage
{
    [HideInInspector] public InmolateEnemyBase _inmolateEnemyBase;
    [SerializeField] string _name;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _attackPower;
    [SerializeField] float _armor;
    [SerializeField] List<Transform> _playerList;
    [SerializeField] float _speed;
    [SerializeField] Rigidbody _rb;

    void Awake()
    {
        _inmolateEnemyBase = new InmolateEnemyBase(_name, _maxHealth, _attackPower, _armor, this);
        _name = this.gameObject.name;

    }

    private void Update()
    {
        if (_playerList.Count >= 1)
        {
            FollowPlayer();
            transform.LookAt(_playerList[0]);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Player _pj = collision.gameObject.GetComponent<Player>();
        if (_pj != null)
        {
            _pj._playerBase.onDamage(_attackPower);
            Destroy(this.gameObject);
            Debug.Log("Colisione con el Player");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Transform pj = other.gameObject.GetComponent<Player>().transform;
            if (pj != null)
            {
                _playerList.Add(pj);
            }
        }
    }
    public void DestroInmolateEnemy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            Transform pj = other.gameObject.GetComponent<Player>().transform;
            if (pj != null)
            {
                _playerList.Remove(pj);
            }
        }
    }

    public void InstantiateInmolateEnemy(Transform enemyPosToSpawn)
    {
        Debug.Log(enemyPosToSpawn);
        Instantiate(this, enemyPosToSpawn.position, this.transform.rotation);
    }

    public void onDamage(float damage)
    {
        _inmolateEnemyBase.onDamage(damage);
    }

    public void FollowPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(this.transform.position, _playerList[0].position, _speed * Time.deltaTime);
        _rb.MovePosition(pos);

    }
}
