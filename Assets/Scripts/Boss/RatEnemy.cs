using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour, IDamage
{
    [HideInInspector] public RatBase _ratBase;
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
        _ratBase = new RatBase(_name, _maxHealth, _attackPower, _armor, this);
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
    public void DestroRat()
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

    public void InstantiateRat(Transform enemyPosToSpawn)
    {
        Debug.Log(enemyPosToSpawn);
        Instantiate(this, enemyPosToSpawn.position, this.transform.rotation);
    }

    public void onDamage(float damage)
    {
        _ratBase.onDamage(damage);
    }

    public void FollowPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(this.transform.position, _playerList[0].position, _speed * Time.deltaTime);
        _rb.MovePosition(pos);

    }
    /*[SerializeField]List<Transform> _playerList;
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    [SerializeField] Rigidbody _rb;
    private void OnCollisionEnter(Collision collision)
    {
        Player _pj = collision.gameObject.GetComponent<Player>();
        if (_pj!=null)
        {
            _pj._playerBase.onDamage(_damage);
            Destroy(this.gameObject);
            Debug.Log("Colisione con el Player");
        }
    }

    private void Update()
    {

        if (_playerList.Count >= 1)
        {
            transform.LookAt(_playerList[0]);
            FollowPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other !=null)
        {
            Transform pj = other.gameObject.GetComponent<Player>().transform;
            if (pj!=null)
            {
                _playerList.Add(pj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other !=null)
        {
            Transform pj = other.gameObject.GetComponent<Player>().transform;
            if (pj != null)
            {
                _playerList.Remove(pj);
            }
        }
    }
    public void InstantiateRat(Transform enemyPosToSpawn)
    {
        Debug.Log(enemyPosToSpawn);
        Instantiate(this, enemyPosToSpawn.position, this.transform.rotation);
    }

    public void FollowPlayer()
    {
           Vector3 pos = Vector3.MoveTowards(this.transform.position, _playerList[0].position, _speed * Time.deltaTime);
           _rb.MovePosition(pos);

    }
    */
}
