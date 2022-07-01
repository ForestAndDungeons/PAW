using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Lista")]
    [SerializeField] List<GameObject> _listObjects;

    [Header("Variables")]
    [SerializeField] float _randomFloat;
    [SerializeField] Transform _spawnObject;
    [SerializeField] Collider _collider;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClip;

    [Header("Animator")]
    [SerializeField] Animator _animator;

    private void Awake()
    {
        _randomFloat = Random.Range(0, _listObjects.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenChest();
    }
    public void OpenChest()
    {
        _collider.enabled = false;
        _animator.SetBool("Open",true);
        _audioSource.PlayOneShot(_audioClip[1]);
        if (_listObjects.Count > 0)
        {
            for (int i = 0; i < _listObjects.Count; i++)
            {
                int index = _listObjects.IndexOf(_listObjects[i]);
                if (_randomFloat == index)
                {
                    Instantiate(_listObjects[i], _spawnObject.position , Quaternion.identity);

                }
            }
        }
    }

    public void Event_Sound_Idle()
    {
        _audioSource.PlayOneShot(_audioClip[0]);
    }
}
