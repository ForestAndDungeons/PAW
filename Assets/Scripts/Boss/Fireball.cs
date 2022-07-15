using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] Transform _bossPos; 
    Player _pjCol;
    Boss _boss;
    Weapon _pjAttack;
    [SerializeField] Rigidbody _myRB;
    [SerializeField] float _forceSpeed;

    private void Start()
    {
        _bossPos = FindObjectOfType<Boss>().transform;
        AddForceFireball();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entre en colision con: " + collision.gameObject.name);
        _boss = collision.gameObject.GetComponent<Boss>();
        _pjCol = collision.gameObject.GetComponent<Player>();
        if (_boss!=null)
        {
            _boss._bossBase.onDamage(1);
        }
        if (_pjCol != null)
        {
            _pjCol._playerBase.onDamage(1);
        }
        Destroy(this.gameObject, 0.1f);


    }

    private void OnTriggerEnter(Collider other)
    {
        
        _pjAttack = other.gameObject.GetComponent<Weapon>();
        Debug.Log(_pjAttack.name);
        if (_pjAttack!=null)
        {
            FireballReverse();
        }
    }
    public void InstantiateFireball(Vector3 res,Transform resTransform)
    {
        if (res != null)
        {
            Instantiate(this, res, resTransform.rotation);
        }
        
    }

    public void AddForceFireball()
    {
        _myRB.AddForce(transform.forward * _forceSpeed, ForceMode.Impulse);
        _myRB.AddForce(transform.up * 0.5f, ForceMode.Impulse);
    }


    public void FireballReverse()
    {
        this.transform.LookAt(_bossPos.position);
        _myRB.AddForce(transform.forward * 45f, ForceMode.Impulse);
        _myRB.AddForce(transform.up * 0.5f, ForceMode.Impulse);
    }

}
