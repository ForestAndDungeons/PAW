using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] List<GameObject> _listObjects;
    Collider _collider;
    Animator _animator;
    ParticleSystem _particleSystem;
    float _randomFloat;
    //[SerializeField] Transform _spawnObject;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _animator = GetComponentInChildren<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _randomFloat = Random.Range(0, _listObjects.Count);
    }

    void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
            OpenChest();
    }

    void OpenChest()
    {
        _collider.enabled = false;
        _particleSystem.Play();
        _animator.SetBool("Open",true);

        if (_listObjects.Count > 0)
        {
            for (int i = 0; i < _listObjects.Count; i++)
            {
                int index = _listObjects.IndexOf(_listObjects[i]);
                if (_randomFloat == index)
                {
                    //Instantiate(_listObjects[i], _spawnObject.position , Quaternion.identity);
                    Instantiate(_listObjects[i], transform.position, Quaternion.identity);
                }
            }
        }
    }
}
