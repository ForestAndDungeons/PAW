using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] AudioSource _breakSound;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject _whole;
    [SerializeField] GameObject _broken;
    [SerializeField] bool _isChest;
    [SerializeField] Chest _chest;
    

    public void Break()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        _whole.SetActive(false);
        _broken.SetActive(true);

        _particleSystem.Play();
        _breakSound.Play();

        if (_chest!=null)
        {
            if (_isChest)
            {
                _chest.OpenChest(this.gameObject.transform);
            }
        }
    }
}
