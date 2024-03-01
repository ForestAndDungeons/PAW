using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float _damage;
    [SerializeField]Rigidbody _myRb;
    Player _pj;
    private void Start()
    {
        
        Debug.Log("Animación");
        _myRb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _pj = collision.gameObject.GetComponent<Player>();
        if (_pj != null)
        {
            _pj.onDamage(1);
            Destroy(this.gameObject, 0.1f);
        }
        Destroy(this.gameObject,0.1f);
    }
}
